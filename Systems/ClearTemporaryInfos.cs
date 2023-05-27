using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ClearTemporaryInfos : GenericSystemBase, IModSystem
    {
        EntityQuery Infos;

        protected override void Initialise()
        {
            base.Initialise();
            Infos = GetEntityQuery(new QueryHelper()
                .All(typeof(CTemporaryApplianceInfo))
                .Any(typeof(CShowPartialApplianceInfo), typeof(CShowAppliancePartInfo)));
        }

        protected override void OnUpdate()
        {
            float dt = Time.DeltaTime;

            using NativeArray<Entity> entities = Infos.ToEntityArray(Allocator.Temp);
            using NativeArray<CTemporaryApplianceInfo> lifetimes = Infos.ToComponentDataArray<CTemporaryApplianceInfo>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CTemporaryApplianceInfo lifetime = lifetimes[i];

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
                    if (Has<CShowPartialApplianceInfo>(entity))
                        EntityManager.RemoveComponent<CShowPartialApplianceInfo>(entity);
                    if (Has<CShowAppliancePartInfo>(entity))
                        EntityManager.RemoveComponent<CShowAppliancePartInfo>(entity);
                }
                else
                {
                    Set(entity, lifetime);
                }
            }
        }
    }
}
