using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(DurationLocks), OrderFirst = true)]
    public class EnsureVendorSelectedInRange : GenericSystemBase, IModSystem
    {
        EntityQuery Vendors;
        protected override void Initialise()
        {
            base.Initialise();
            Vendors = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliancePartVendor), typeof(CVendorOption)));
        }
        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Vendors.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartVendor> vendors = Vendors.ToComponentDataArray<CAppliancePartVendor>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartVendor vendor = vendors[i];

                if (!RequireBuffer(entity, out DynamicBuffer<CVendorOption> options) || options.Length < 1)
                {
                    vendor.SelectedIndex = -1;
                    Set<CVendorLocked>(entity);
                    continue;
                }
                EntityManager.RemoveComponent<CVendorLocked>(entity);
                if (vendor.SelectedIndex > options.Length - 1 || vendor.SelectedIndex < 0)
                {
                    vendor.SelectedIndex = 0;
                    Set(entity, vendor);
                }
            }
        }
    }
}
