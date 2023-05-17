using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(DurationLocks))]
    public class LockDurationsDuringDay : DaySystem, IModSystem
    {
        EntityQuery Durations;
        protected override void Initialise()
        {
            base.Initialise();
            Durations = GetEntityQuery(typeof(CTakesDuration), typeof(CLockDurationDay));
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
