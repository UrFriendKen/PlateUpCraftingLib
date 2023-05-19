using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class PurchasePartAfterDuration : GenericSystemBase, IModSystem
    {
        EntityQuery Vendors;
        protected override void Initialise()
        {
            base.Initialise();
            Vendors = GetEntityQuery(new QueryHelper()
                .All(typeof(CPartsVendor), typeof(CTakesDuration)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = Vendors.ToEntityArray(Allocator.Temp);
            using NativeArray<CPartsVendor> appliances = Vendors.ToComponentDataArray<CPartsVendor>(Allocator.Temp);
            using NativeArray<CTakesDuration> positions = Vendors.ToComponentDataArray<CTakesDuration>(Allocator.Temp);
        }
    }
}
