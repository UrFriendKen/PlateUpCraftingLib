using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenCraftingLibTest.Customs.ApplianceRecipes;
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

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Test Stage 1",
                Description = "This is a Partial Appliance",
                Sections = new List<Appliance.Section>()
                {
                    new Appliance.Section()
                    {
                        Title = "To Test Stage 2",
                        Description = "Insert 1 Red Part and 1 Yellow Part"
                    },
                    new Appliance.Section()
                    {
                        Title = "To Neon Sign",
                        Description = "Insert 4 Red Parts"
                    }
                }
            })
        };

        public override List<ApplianceRecipe> Recipes => new List<ApplianceRecipe>()
        {
            GDOUtils.GetCustomGameDataObject<TestStage1ToNeon>().GameDataObject as ApplianceRecipe,
            GDOUtils.GetCustomGameDataObject<TestStage1ToTestStage2>().GameDataObject as ApplianceRecipe
        };
    }
}
