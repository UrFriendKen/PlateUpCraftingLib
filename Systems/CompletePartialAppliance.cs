using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class CompletePartialAppliance : RestaurantSystem, IModSystem
    {
        EntityQuery PartialAppliances;
        protected override void Initialise()
        {
            base.Initialise();
            PartialAppliances = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliance), typeof(CPartialAppliance), typeof(CPosition), typeof(CIsCompletable)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = PartialAppliances.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliance> appliances = PartialAppliances.ToComponentDataArray<CAppliance>(Allocator.Temp);
            using NativeArray<CPosition> positions = PartialAppliances.ToComponentDataArray<CPosition>(Allocator.Temp);
            using NativeArray<CIsCompletable> completables = PartialAppliances.ToComponentDataArray<CIsCompletable>(Allocator.Temp);

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliance appliance = appliances[i];
                CPosition position = positions[i];
                CIsCompletable completable = completables[i];

                if (Require(entity, out CTakesDuration duration))
                {
                    if (!duration.Active || !(duration.Remaining <= 0f))
                    {
                        continue;
                    }
                }

                if (appliance.Layer != completable.Layer && GetOccupant(position, completable.Layer) != default)
                    continue;

                Entity newEntity = ctx.CreateEntity();
                ctx.Set(newEntity, new CCreateAppliance()
                {
                    ID = completable.ID,
                    ForceLayer = completable.Layer
                });
                ctx.Set(newEntity, new CPosition(position));

                ctx.Destroy(entity);
                SetOccupant(position, default, appliance.Layer);
                SetOccupant(position, newEntity, completable.Layer);
            }
        }
    }
}
