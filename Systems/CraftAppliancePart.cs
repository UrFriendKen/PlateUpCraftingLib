using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class CraftAppliancePart : RestaurantSystem, IModSystem
    {
        EntityQuery CraftStations;
        protected override void Initialise()
        {
            base.Initialise();
            CraftStations = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliancePartCraftStation), typeof(CRecipeSatisfied), typeof(CUsedPart), typeof(CTakesDuration)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = CraftStations.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartCraftStation> craftStations = CraftStations.ToComponentDataArray<CAppliancePartCraftStation>(Allocator.Temp);
            using NativeArray<CRecipeSatisfied> recipeResults = CraftStations.ToComponentDataArray<CRecipeSatisfied>(Allocator.Temp);
            using NativeArray<CTakesDuration> durations = CraftStations.ToComponentDataArray<CTakesDuration>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartCraftStation craftStation = craftStations[i];
                CRecipeSatisfied recipeResult = recipeResults[i];
                CTakesDuration duration = durations[i];

                if (!duration.Active || !(duration.Remaining <= 0f))
                {
                    continue;
                }
                if (!ctx.RequireBuffer(entity, out DynamicBuffer<CUsedPart> buffer))
                {
                    buffer = ctx.AddBuffer<CUsedPart>(entity);
                }
                buffer.Clear();
                buffer.Add(new CUsedPart()
                {
                    ID = recipeResult.Result
                });

                if (!craftStation.MultipleCraftsInDay)
                {
                    ctx.Add<CCraftPerformed>(entity);
                }
            }
        }
    }
}
