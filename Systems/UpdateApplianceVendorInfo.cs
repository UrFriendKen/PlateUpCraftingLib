using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class UpdateApplianceVendorInfo : GenericSystemBase, IModSystem
    {
        EntityQuery Infos;
        protected override void Initialise()
        {
            base.Initialise();
            Infos = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliancePartVendor), typeof(CVendorOption), typeof(CShowAppliancePartVendorInfo))
                .None(typeof(CVendorLocked)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Infos.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartVendor> vendors = Infos.ToComponentDataArray<CAppliancePartVendor>(Allocator.Temp);
            using NativeArray<CShowAppliancePartVendorInfo> infos = Infos.ToComponentDataArray<CShowAppliancePartVendorInfo>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartVendor vendor = vendors[i];
                CShowAppliancePartVendorInfo info = infos[i];

                if (!RequireBuffer(entity, out DynamicBuffer<CVendorOption> optionsBuffer))
                    continue;
                if (vendor.SelectedIndex < 0 || vendor.SelectedIndex > optionsBuffer.Length - 1)
                    continue;
                CVendorOption option = optionsBuffer[vendor.SelectedIndex];
                info.PartID = option.ID;
                info.Price = option.PurchaseCost;
                Set(entity, info);
            }
        }
    }
}
