using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ValidateCraftStationSatisfied : GameSystemBase, IModSystem
    {
        EntityQuery CraftStations;
        protected override void Initialise()
        {
            base.Initialise();
            CraftStations = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CAppliancePartCraftStation)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = CraftStations.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartCraftStation> craftStations = CraftStations.ToComponentDataArray<CAppliancePartCraftStation>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartCraftStation craftStation = craftStations[i];

                if (!craftStation.HasResult(ctx, entity, out int resultID))
                {
                    if (ctx.Has<CRecipeSatisfied>(entity))
                        ctx.Remove<CRecipeSatisfied>(entity);
                    continue;
                }

                ctx.Set(entity, new CRecipeSatisfied()
                {
                    Result = resultID
                });
            }
        }
    }
}
