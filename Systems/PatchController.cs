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
        
        public static bool StaticHas<T>(Entity entity) where T : struct, IComponentData
        {
            return _instance?.Has<T>(entity) ?? false;
        }
    }
}
