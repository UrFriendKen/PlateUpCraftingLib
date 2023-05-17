using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenCraftingLibTest.Customs.ApplianceRecipes;
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

        public override List<ApplianceRecipe> Recipes => new List<ApplianceRecipe>()
        {
            GDOUtils.GetCustomGameDataObject<TestStage2ToSafetyHob>().GameDataObject as ApplianceRecipe
        };
    }
}
