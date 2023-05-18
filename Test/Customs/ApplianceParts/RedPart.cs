using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedPart : CustomAppliancePart
    {
        public override string UniqueNameID => "redPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedPart>(
            scaleX: 0.3f, scaleY: 0.3f, scaleZ: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>()
        {
            (Locale.English, new BasicInfo()
            {
                Name = "Red Part",
                Description = "A red part. Duh"
            })
        };

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
