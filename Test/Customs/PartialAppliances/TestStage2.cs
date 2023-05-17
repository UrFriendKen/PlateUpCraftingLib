using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.PartialAppliances
{
    public class TestStage2 : CustomPartialAppliance
    {
        public override string UniqueNameID => "testStage2";

        public override GameObject Prefab => TestCubeManager.GetPrefab<TestStage2>(
            scale: 1f, material: MaterialUtils.GetExistingMaterial("Metal - Copper"));
        public override bool IsPurchasable => false;
        public override bool IsPurchasableAsUpgrade => false;

        public override List<PartialAppliance.ApplianceRecipe> Recipes => new List<PartialAppliance.ApplianceRecipe>()
        {
            new PartialAppliance.ApplianceRecipe()
            {
                Parts = new Dictionary<AppliancePart, int>()
                {
                    { GDOUtils.GetCastedGDO<AppliancePart, RedPart>(), 1 },
                    { GDOUtils.GetCastedGDO<AppliancePart, YellowPart>(), 1 }
                },
                Result = GDOUtils.GetExistingGDO(ApplianceReferences.HobSafe) as Appliance
            }
        };
    }
}
