using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class YellowPart : CustomAppliancePart
    {
        public override string UniqueNameID => "yellowPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<YellowPart>(
            scale: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Yellow"));

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
