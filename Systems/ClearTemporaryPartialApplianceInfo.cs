using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ClearTemporaryPartialApplianceInfo : GenericSystemBase, IModSystem
    {
        EntityQuery PartialApplianceInfos;

        protected override void Initialise()
        {
            base.Initialise();
            PartialApplianceInfos = GetEntityQuery(new QueryHelper()
                .All(typeof(CTemporaryApplianceInfo), typeof(CShowPartialApplianceInfo)));
        }

        protected override void OnUpdate()
        {
            float dt = Time.DeltaTime;

            using NativeArray<Entity> entities = PartialApplianceInfos.ToEntityArray(Allocator.Temp);
            using NativeArray<CTemporaryApplianceInfo> lifetimes = PartialApplianceInfos.ToComponentDataArray<CTemporaryApplianceInfo>(Allocator.Temp);
            using NativeArray<CShowPartialApplianceInfo> infos = PartialApplianceInfos.ToComponentDataArray<CShowPartialApplianceInfo>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CTemporaryApplianceInfo lifetime = lifetimes[i];
                CShowPartialApplianceInfo info = infos[i];

                if (Has<CBeingLookedAt>(entity))
                {
                    lifetime.RemainingLifetime = 0.2f;
                    Set(entity, lifetime);
                    continue;
                }
                lifetime.RemainingLifetime -= dt;
                if (lifetime.RemainingLifetime < 0f)
                {
                    EntityManager.RemoveComponent<CTemporaryApplianceInfo>(entity);
                    EntityManager.RemoveComponent<CShowPartialApplianceInfo>(entity);
                }
                else
                {
                    Set(entity, lifetime);
                }
            }
        }
    }
}
