using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class YellowPart : CustomAppliancePart
    {
        public override string UniqueNameID => "yellowPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<YellowPart>(
            scaleX: 0.3f, scaleY: 0.3f, scaleZ: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Yellow"));

        public override bool IsWithdrawable => true;

        public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>()
        {
            (Locale.English, new BasicInfo()
            {
                Name = "Yellow Part",
                Description = "What did you think it was?"
            })
        };

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
