using Kitchen;
using Unity.Collections;
using Unity.Entities;

namespace KitchenCraftingLibTest
{
    public class SetOnFirePermanent : DaySystem
    {
        EntityQuery PermanentFires;
        protected override void Initialise()
        {
            base.Initialise();
            PermanentFires = GetEntityQuery(new QueryHelper()
                .All(typeof(Customs.Components.CPermanentlyOnFire))
                .None(typeof(CIsOnFire)));
        }

        protected override void OnUpdate()
        {
            using NativeArray<Entity> entities = PermanentFires.ToEntityArray(Allocator.Temp);
            foreach (Entity e in entities)
            Set(e, new CIsOnFire());
        }
    }
}
