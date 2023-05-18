using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.Views
{
    public class AppliancePartView : UpdatableObjectView<AppliancePartView.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper()
                    .All(typeof(CLinkedView), typeof(CAppliancePart)));
            }

            protected override void OnUpdate()
            {
                using NativeArray<CLinkedView> views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using NativeArray<CAppliancePart> parts = Views.ToComponentDataArray<CAppliancePart>(Allocator.Temp);

                for (int i = 0; i < views.Length; i++)
                {
                    CLinkedView view = views[i];
                    CAppliancePart part = parts[i];

                    SendUpdate(view, new ViewData()
                    {
                        AppliancePartID = part.ID
                    });
                }
            }
        }

        [MessagePackObject(false)]
        public class ViewData : IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int AppliancePartID { get; set; }

            public bool IsChangedFrom(ViewData check)
            {
                return AppliancePartID != check.AppliancePartID;
            }
        }

        public override void Initialise()
        {
            base.Initialise();
            base.gameObject.SetActive(false);
        }

        private GameObject PrefabContainer;

        protected override void UpdateData(ViewData view_data)
        {
            if (!GameData.Main.TryGet<AppliancePart>(view_data.AppliancePartID, out var appliancePart))
            {
                return;
            }
            if (PrefabContainer != null)
            {
                Destroy(PrefabContainer);
            }
            PrefabContainer = Instantiate(GameData.Main.GetPrefab(view_data.AppliancePartID), base.transform, worldPositionStays: true);
            PrefabContainer.transform.localPosition = Vector3.zero;
            PrefabContainer.transform.localRotation = Quaternion.identity;
            PrefabContainer.transform.localScale = Vector3.one;
        }
    }
}
