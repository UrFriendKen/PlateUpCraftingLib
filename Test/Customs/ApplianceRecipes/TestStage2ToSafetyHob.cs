using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace KitchenCraftingLibTest.Customs.ApplianceRecipes
{
    public class TestStage2ToSafetyHob : CustomApplianceRecipe
    {
        public override string UniqueNameID => "testStage2ToSafetyHob";
        public override Dictionary<AppliancePart, int> Parts => new Dictionary<AppliancePart, int>()
        {
            { GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject as AppliancePart, 2 },
            { GDOUtils.GetCustomGameDataObject<YellowPart>().GameDataObject as AppliancePart, 1 }
        };
        public override Appliance Result => GDOUtils.GetExistingGDO(ApplianceReferences.HobSafe) as Appliance;
    }
}
