using CraftingLib;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedProvider : CustomAppliance
    {
        public override string UniqueNameID => "redProvider";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedProvider>(
            scale: 0.8f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CAppliancePartStore()
            {
                PartID = GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject.ID,
                Total = 0,
                Remaining = 0
            }
        };
    }
}
