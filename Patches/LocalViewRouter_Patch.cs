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
        static GameObject _appliancePartPrefab = null;

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
                    object obj = m_GetPrefab.Invoke(__instance, new object[] { ViewType.ApplianceInfo });
                    if (obj != null)
                    {
                        _partialApplianceInfoPrefab = GameObject.Instantiate(obj as GameObject);
                        _partialApplianceInfoPrefab.name = "Partial Appliance Indicator";
                        _partialApplianceInfoPrefab.transform.SetParent(_hiderContainer.transform);

                        ApplianceInfoView applianceInfoView = _partialApplianceInfoPrefab.GetComponent<ApplianceInfoView>();
                        Component.DestroyImmediate(applianceInfoView);

                        PartialApplianceInfoView partialApplianceInfoView = _partialApplianceInfoPrefab.AddComponent<PartialApplianceInfoView>();

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
                }
                __result = _partialApplianceInfoPrefab;
                return false;
            }
            return true;
        }
    }
}
