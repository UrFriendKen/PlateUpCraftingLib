using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    [UpdateInGroup(typeof(DurationLocks))]
    public class DurationRequiresNotLocked : GameSystemBase, IModSystem
    {
        EntityQuery Durations;
        protected override void Initialise()
        {
            base.Initialise();
            Durations = GetEntityQuery(new QueryHelper()
                .All(typeof(CPartsVendor), typeof(CTakesDuration), typeof(CLockedVendor)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = Durations.ToEntityArray(Allocator.Temp);
            using NativeArray<CTakesDuration> durations = Durations.ToComponentDataArray<CTakesDuration>(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CTakesDuration duration = durations[i];
                duration.IsLocked = true;
                Set(entity, duration);
            }
        }
    }
}
