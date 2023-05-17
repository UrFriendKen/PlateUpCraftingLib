﻿using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateInGroup(typeof(DurationLocks))]
    public class DurationRequiresPartialCompletable : GameSystemBase, IModSystem
    {
        EntityQuery Durations;
        protected override void Initialise()
        {
            base.Initialise();
            Durations = GetEntityQuery(new QueryHelper()
                .All(typeof(CPartialAppliance), typeof(CTakesDuration))
                .None(typeof(CIsCompletable)));
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
