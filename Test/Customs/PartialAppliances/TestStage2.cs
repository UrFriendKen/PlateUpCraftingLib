using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceRecipes;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using TestCubes;

namespace KitchenCraftingLibTest.Customs.PartialAppliances
{
    public class TestStage2 : CustomPartialAppliance
    {
        public override string UniqueNameID => "testStage2";

        public override GameObject Prefab => TestCubeManager.GetPrefab<TestStage2>(
            scaleY: 0.8f, material: MaterialUtils.GetExistingMaterial("Metal - Copper"));
        public override bool IsPurchasable => false;
        public override bool IsPurchasableAsUpgrade => false;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CSpecialPartialAppliance()
            {
                SpecialDeposit = true
            }
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Test Stage 2",
                Description = "This is a Partial Appliance",
                Sections = new List<Appliance.Section>()
                {
                    new Appliance.Section()
                    {
                        Title = "To Safety Hob",
                        Description = "Insert 2 Red Parts and 1 Yellow Part"
                    }
                },
                Tags = new List<string>() { "Completes Instantly" }
            })
        };

        public override List<ApplianceRecipe> Recipes => new List<ApplianceRecipe>()
        {
            GDOUtils.GetCustomGameDataObject<TestStage2ToSafetyHob>().GameDataObject as ApplianceRecipe
        };
    }
}
