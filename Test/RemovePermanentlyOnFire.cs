using Kitchen;
using Unity.Collections;
using Unity.Entities;
using static KitchenCraftingLibTest.Customs.Components;

namespace KitchenCraftingLibTest
{
    public class RemovePermanentlyOnFire : GenericSystemBase
    {
        EntityQuery PermanentFires;
        protected override void Initialise()
        {
            base.Initialise();
            PermanentFires = GetEntityQuery(new QueryHelper()
                .All(typeof(CRemovePermanentlyOnFire)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = PermanentFires.ToEntityArray(Allocator.Temp);
            foreach (Entity e in entities)
            {
                if (Has<CPermanentlyOnFire>(e))
                    EntityManager.RemoveComponent<CPermanentlyOnFire>(e);
                EntityManager.RemoveComponent<CRemovePermanentlyOnFire>(e);
            }
        }
    }
}
