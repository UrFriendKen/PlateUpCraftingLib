using KitchenMods;
using Unity.Entities;

namespace KitchenCraftingLibTest.Customs
{
    public static class Components
    {
        public struct CPermanentlyOnFire : IComponentData, IModComponent { }
        public struct CRemovePermanentlyOnFire : IComponentData, IModComponent { }
    }
}
