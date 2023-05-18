using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using TestCubes;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class YellowPart : CustomAppliancePart
    {
        public override string UniqueNameID => "yellowPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<YellowPart>(
            scaleX: 0.3f, scaleY: 0.3f, scaleZ: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Yellow"));

        public override List<IAppliancePartProperty> Properties => new List<IAppliancePartProperty>()
        {
            new CNonDisposablePart()
        };

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
