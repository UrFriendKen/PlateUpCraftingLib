using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using static Controllers.InputLock;

namespace CraftingLib.Views
{
    public class VendorDisplayPartSubview : UpdatableObjectView<VendorDisplayPartSubview.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper()
                    .All(typeof(CLinkedView), typeof(CAppliancePartVendor), typeof(CVendorOption)));
            }

            protected override void OnUpdate()
            {
                using NativeArray<Entity> entities = Views.ToEntityArray(Allocator.Temp);
                using NativeArray<CLinkedView> views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using NativeArray<CAppliancePartVendor> vendors = Views.ToComponentDataArray<CAppliancePartVendor>(Allocator.Temp);


                for (int i = 0; i < views.Length; i++)
                {
                    Entity entity = entities[i];
                    CLinkedView view = views[i];
                    CAppliancePartVendor vendor = vendors[i];

                    int partID = 0;
                    bool isLocked = Has<CVendorLocked>(entity);
                    if (RequireBuffer(entity, out DynamicBuffer<CVendorOption> optionsBuffer) &&
                        vendor.SelectedIndex > -1 && vendor.SelectedIndex < optionsBuffer.Length)
                    {
                        CVendorOption selectedOption = optionsBuffer[vendor.SelectedIndex];
                        partID = selectedOption.ID;
                    }

                    SendUpdate(view, new ViewData()
                    {
                        AppliancePartID = partID,
                        IsLocked = isLocked
                    });
                }
            }
        }

        [MessagePackObject(false)]
        public class ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int AppliancePartID { get; set; }
            [Key(2)] public bool IsLocked { get; set; }

            public bool IsChangedFrom(ViewData check)
            {
                return AppliancePartID != check.AppliancePartID ||
                    IsLocked != check.IsLocked;
            }

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<VendorDisplayPartSubview>();
        }

        public override void Initialise()
        {
            base.Initialise();
            base.gameObject.SetActive(false);
        }

        public GameObject Lock;
        public GameObject Holder;
        private GameObject PrefabContainer;

        protected override void UpdateData(ViewData view_data)
        {

            Lock?.SetActive(view_data.IsLocked);
            if (!GameData.Main.TryGet<AppliancePart>(view_data.AppliancePartID, out var appliancePart))
            {
                Holder?.SetActive(false);
                return;
            }
            if (PrefabContainer != null)
            {
                Destroy(PrefabContainer);
            }
            PrefabContainer = Instantiate(GameData.Main.GetPrefab(view_data.AppliancePartID));
            if (Holder != null)
            {
                PrefabContainer.transform.SetParent(Holder.transform);
                Holder.SetActive(true);
            }
            PrefabContainer.transform.localPosition = Vector3.zero;
            PrefabContainer.transform.localRotation = Quaternion.identity;
            PrefabContainer.transform.localScale = Vector3.one;
        }
    }
}
