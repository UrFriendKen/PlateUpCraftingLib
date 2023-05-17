using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenCraftingLibTest.Customs.ApplianceRecipes
{
    public class TestStage1ToNeon : CustomApplianceRecipe
    {
        public override string UniqueNameID => "testStage1ToNeon";
        public override Dictionary<AppliancePart, int> Parts => new Dictionary<AppliancePart, int>()
        {
            { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 4 }
        };
        public override Appliance Result => GDOUtils.GetExistingGDO(ApplianceReferences.AffordableNeonSign1) as Appliance;
    }
}
