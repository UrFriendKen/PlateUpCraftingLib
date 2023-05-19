using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;
using Kitchen;
using CraftingLib;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class BasicPartsVendingMachine : CustomAppliance
    {
        public override string UniqueNameID => "basicPartsVendingMachine";

        public override GameObject Prefab => TestCubeManager.GetPrefab<BasicPartsVendingMachine>(
            scaleX: 0.85f, scaleZ: 0.5f, material: MaterialUtils.GetExistingMaterial("Plastic - Dark Green"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CPartsVendor(),
            new CImmovable(),
            new CFixedRotation(),
            new CDestroyApplianceAtDay()
            {
                HideBin = true
            }
        };

        public override bool IsPurchasable => false;

        public override bool IsPurchasableAsUpgrade => false;

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Parts Vending Machine",
                Description = "Buy Appliance Parts Here"
            })
        };
    }
}
