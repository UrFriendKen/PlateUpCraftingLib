using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(DestructionGroup))]
    public class DestroyDepletedPartsStore : GenericSystemBase, IModSystem
    {
        EntityQuery Stores;

        protected override void Initialise()
        {
            base.Initialise();
            Stores = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliancePartStore), typeof(CDestroyWhenDepleted)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Stores.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartStore> stores = Stores.ToComponentDataArray<CAppliancePartStore>(Allocator.Temp);

            for (int i = entities.Length - 1; i > -1; i--)
            {
                Entity entity = entities[i];
                CAppliancePartStore store = stores[i];

                if (!store.IsInfinite && !store.IsInUse)
                    EntityManager.DestroyEntity(entity);
            }
        }
    }
}
