using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class ManageVendorLocks : GenericSystemBase, IModSystem
    {
        EntityQuery Vendors;
        protected override void Initialise()
        {
            base.Initialise();
            Vendors = GetEntityQuery(typeof(CPartsVendor));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Vendors.ToEntityArray(Allocator.Temp);
            using NativeArray<CPartsVendor> vendors = Vendors.ToComponentDataArray<CPartsVendor>(Allocator.Temp);

            SMoney player_money = GetOrDefault<SMoney>();

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CPartsVendor vendor = vendors[i];

                bool shouldLock = vendor.PartID == 0 || !GameData.Main.TryGet(vendor.PartID, out AppliancePart _) || vendor.Cost > player_money;
                if (!shouldLock && Has<CLockedVendor>())
                {
                    EntityManager.RemoveComponent<CLockedVendor>(entity);
                    continue;
                }
                if (shouldLock && !Has<CLockedVendor>())
                {
                    EntityManager.AddComponent<CLockedVendor>(entity);
                    continue;
                }
            }
        }
    }
}
