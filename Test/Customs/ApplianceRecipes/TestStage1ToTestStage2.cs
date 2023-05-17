using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenCraftingLibTest.Customs.PartialAppliances;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenCraftingLibTest.Customs.ApplianceRecipes
{
    public class TestStage1ToTestStage2 : CustomApplianceRecipe
    {
        public override string UniqueNameID => "testStage1TotestStage2";
        public override Dictionary<AppliancePart, int> Parts => new Dictionary<AppliancePart, int>()
        {
            { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 1 },
            { GDOUtils.GetCustomGameDataObject<YellowPart>().GameDataObject as AppliancePart, 1 },
        };
        public override Appliance Result => GDOUtils.GetCustomGameDataObject<TestStage2>().GameDataObject as Appliance;
    }
}
