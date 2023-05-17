using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.PartialAppliances
{
    public class TestStage1 : CustomPartialAppliance
    {
        public override string UniqueNameID => "testStage1";

        public override GameObject Prefab => TestCubeManager.GetPrefab<TestStage1>(
            scale: 1f, material: MaterialUtils.GetExistingMaterial("Metal Dark"));
        public override bool IsPurchasable => false;
        public override bool IsPurchasableAsUpgrade => false;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CDisplayDuration()
            {
                Process = ProcessReferences.Purchase,
                ShowWhenEmpty = true,
                IsBad = false
            },
            new CTakesDuration()
            {
                Mode = InteractionMode.Appliances,
                Manual = true,
                Total = 3f
            },
            new CLockDurationDay()
        };

        public override List<PartialAppliance.ApplianceRecipe> Recipes => new List<PartialAppliance.ApplianceRecipe>()
        {
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 2 }
                },
                RequireExactMatch = true,
                Result = GDOUtils.GetCastedGDO<PartialAppliance, TestStage2>()
            },
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 10 }
                },
                Result = GDOUtils.GetExistingGDO(ApplianceReferences.AffordableNeonSign1) as Appliance
            },
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 5 }
                },
                Result = GDOUtils.GetExistingGDO(ApplianceReferences.AffordableNeonSign2) as Appliance
            },
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 7 }
                },
                RequireExactMatch = true,
                Result = GDOUtils.GetExistingGDO(ApplianceReferences.AffordableRoofLight) as Appliance
            },
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 1 }
                },
                Result = GDOUtils.GetExistingGDO(ApplianceReferences.AffordableStockArt) as Appliance
            },
        };
    }
}
