using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedPart : CustomAppliancePart
    {
        public override string UniqueNameID => "redPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedPart>(
            scale: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
