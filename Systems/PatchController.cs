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

        public static bool StaticTryGetEntityQuery(QueryHelper queryHelper, out EntityQuery entityQuery)
        {
            EntityQuery? tempQuery = _instance?.GetEntityQuery(queryHelper);
            if (!tempQuery.HasValue)
            {
                entityQuery = default;
                return false;
            }
            entityQuery = tempQuery.Value;
            return true;
        }
    }
}
