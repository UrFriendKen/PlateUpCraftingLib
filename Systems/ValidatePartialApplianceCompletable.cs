using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ValidatePartialApplianceCompletable : GameSystemBase, IModSystem
    {
        EntityQuery PartialAppliances;
        protected override void Initialise()
        {
            base.Initialise();
            PartialAppliances = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CPartialAppliance), typeof(CPosition)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = PartialAppliances.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliance> appliances = PartialAppliances.ToComponentDataArray<CAppliance>(Allocator.Temp);
            using NativeArray<CPosition> positions = PartialAppliances.ToComponentDataArray<CPosition>(Allocator.Temp);
            using NativeArray<CPartialAppliance> partialAppliances = PartialAppliances.ToComponentDataArray<CPartialAppliance>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliance appliance = appliances[i];
                CPosition position = positions[i];
                CPartialAppliance partialAppliance = partialAppliances[i];

                if (!partialAppliance.IsComplete(ctx, entity, out Appliance result))
                {
                    if (ctx.Has<CIsCompletable>(entity))
                        ctx.Remove<CIsCompletable>(entity);
                    continue;
                }

                ctx.Set(entity, new CIsCompletable()
                {
                    ID = result.ID,
                    Layer = result.Layer
                });
            }
        }
    }
}
