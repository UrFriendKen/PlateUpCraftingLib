using KitchenMods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace KitchenCraftingLibTest.Customs
{
    public static class Components
    {
        public struct CPermanentlyOnFire : IComponentData, IModComponent { }
        public struct CRemovePermanentlyOnFire : IComponentData, IModComponent { }
    }
}
