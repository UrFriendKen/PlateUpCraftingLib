using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(DurationLocks), OrderFirst = true)]
    public class UpdateVendorLock : GenericSystemBase, IModSystem
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

            SMoney player_money = GetOrDefault<SMoney>();

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartVendor vendor = vendors[i];

                bool shouldLock = false;
                CVendorLocked.LockReason lockReason = default;

                if (!RequireBuffer(entity, out DynamicBuffer<CVendorOption> options) || options.Length < 1)
                {
                    vendor.SelectedIndex = -1;
                    shouldLock = true;
                    lockReason = CVendorLocked.LockReason.InvalidID;
                }
                else if (vendor.SelectedIndex > options.Length - 1 || vendor.SelectedIndex < 0)
                {
                    vendor.SelectedIndex = 0;
                }
                if (player_money < options[vendor.SelectedIndex].PurchaseCost)
                {
                    shouldLock = true;
                    lockReason = CVendorLocked.LockReason.NotEnoughMoney;
                }

                if (shouldLock)
                {
                    Set(entity, new CVendorLocked()
                    {
                        Reason = lockReason
                    });
                }
                else if (Has<CVendorLocked>(entity))
                {
                    EntityManager.RemoveComponent<CVendorLocked>(entity);
                }
            }
        }
    }
}
