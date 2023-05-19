using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLibParts.Customs
{
    public struct CPartsVendor : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        public int PartID;
        public int Cost;
    }

    public struct CLockedVendor : IComponentData, IModComponent { }
    public struct CIsInitialized : IComponentData, IModComponent { }
}
