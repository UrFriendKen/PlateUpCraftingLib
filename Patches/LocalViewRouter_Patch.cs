using CraftingLib.Views;
using HarmonyLib;
using Kitchen;
using System;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace CraftingLib.Patches
{
    [HarmonyPatch]
    static class LocalViewRouter_Patch
    {
        static MethodInfo m_GetPrefab = typeof(LocalViewRouter).GetMethod("GetPrefab", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ViewType) }, null);

        static GameObject _hiderContainer = null;
        static GameObject _partialApplianceInfoPrefab = null;
        static GameObject _appliancePartInfoPrefab = null;
        static GameObject _appliancePartPrefab = null;

        static GameObject _applianceIndicatorCopy;

        static GameObject GetApplianceIndicatorCopy(LocalViewRouter instance, bool stripView = true, bool original = false)
        {
            if (_applianceIndicatorCopy == null)
            {
                object obj = m_GetPrefab.Invoke(instance, new object[] { ViewType.ApplianceInfo });
                if (obj != null)
                {
                    _applianceIndicatorCopy = GameObject.Instantiate(obj as GameObject);
                    _applianceIndicatorCopy.name = "Appliance Indicator Copy";
                    _applianceIndicatorCopy.transform.SetParent(_hiderContainer.transform);
                }
                if (stripView)
                {
                    ApplianceInfoView applianceInfoView = _applianceIndicatorCopy.GetComponent<ApplianceInfoView>();
                    Component.DestroyImmediate(applianceInfoView);
                }
            }
            return original? _applianceIndicatorCopy : GameObject.Instantiate(_applianceIndicatorCopy);
        }

        static void AttachInfoView<T, V>(GameObject gameObject)  where T : InfoView<V> where V : IViewData
        {
            T partialApplianceInfoView = gameObject.AddComponent<T>();

            Transform transform = partialApplianceInfoView.transform;
            Transform container = transform.Find("Container");
            Transform templates = transform.Find("Templates");
            Transform background = container?.Find("Background");
            Transform body = container?.Find("Body");
            Transform price = body?.Find("Price");

            partialApplianceInfoView.Title = container?.Find("Name")?.GetComponent<TextMeshPro>();
            partialApplianceInfoView.Title?.SetText("Partial Appliance");

            partialApplianceInfoView.Description = body.Find("Description")?.Find("Description Text")?.GetComponent<TextMeshPro>();
            partialApplianceInfoView.Description?.SetText("For all your crafting needs!");

            partialApplianceInfoView.Sections = body?.Find("Sections")?.gameObject;
            partialApplianceInfoView.PriceTag = price?.gameObject;
            partialApplianceInfoView.Price = price?.Find("Value")?.GetComponent<TextMeshPro>();

            partialApplianceInfoView.Backing = background?.gameObject;

            partialApplianceInfoView.TemplateTag = templates?.Find("Process").gameObject;
            partialApplianceInfoView.TemplateInfo = templates?.Find("Info").gameObject;
            partialApplianceInfoView.Animator = container?.GetComponent<Animator>();
        }

        [HarmonyPatch(typeof(LocalViewRouter), "GetPrefab")]
        [HarmonyPrefix]
        static bool GetPrefab_Prefix(ref LocalViewRouter __instance, ref GameObject __result, ViewType view_type)
        {
            if (_hiderContainer == null)
            {
                _hiderContainer = new GameObject("View Prefabs Hider");
                _hiderContainer.SetActive(false);
            }

            if (view_type == Main.AppliancePartViewType)
            {
                if (_appliancePartPrefab == null)
                {
                    _appliancePartPrefab = new GameObject("Appliance Part");
                    _appliancePartPrefab.transform.SetParent(_hiderContainer.transform);

                    AppliancePartView appliancePartView = _appliancePartPrefab.AddComponent<AppliancePartView>();
                }
                __result = _appliancePartPrefab;
                return false;
            }

            if (view_type == Main.PartialApplianceInfoViewType)
            {
                if (_partialApplianceInfoPrefab == null)
                {
                    _partialApplianceInfoPrefab = GetApplianceIndicatorCopy(__instance);
                    _partialApplianceInfoPrefab.name = "Partial Appliance Indicator";
                    _partialApplianceInfoPrefab.transform.SetParent(_hiderContainer.transform);


                    AttachInfoView<PartialApplianceInfoView, PartialApplianceInfoView.ViewData>(_partialApplianceInfoPrefab);
                }
                __result = _partialApplianceInfoPrefab;
                return false;
            }

            if (view_type == Main.AppliancePartInfoViewType)
            {
                if (_appliancePartInfoPrefab == null)
                {
                    _appliancePartInfoPrefab = GetApplianceIndicatorCopy(__instance);
                    _appliancePartInfoPrefab.name = "Appliance Part Indicator";
                    _appliancePartInfoPrefab.transform.SetParent(_hiderContainer.transform);


                    AttachInfoView<AppliancePartInfoView, AppliancePartInfoView.ViewData>(_appliancePartInfoPrefab);
                }
                __result = _appliancePartInfoPrefab;
                return false;
            }

            return true;
        }
    }
}
