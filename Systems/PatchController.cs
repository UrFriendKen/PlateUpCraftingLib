using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class PatchController : GenericSystemBase, IModSystem
    {
        static PatchController _instance;
        protected override void OnUpdate()
        {
            _instance = this;
        }

        public static bool HasCPartialAppliance(Entity entity)
        {
            return _instance?.Has<CPartialAppliance>(entity) ?? false;
        }
    }
}
