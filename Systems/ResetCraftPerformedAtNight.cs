using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ResetCraftPerformedAtNight : NightSystem, IModSystem
    {
        EntityQuery CraftPerformed;
        protected override void Initialise()
        {
            base.Initialise();
            CraftPerformed = GetEntityQuery(typeof(CCraftPerformed));
        }

        protected override void OnUpdate()
        {
            EntityManager.RemoveComponent<CCraftPerformed>(CraftPerformed);
        }
    }
}
