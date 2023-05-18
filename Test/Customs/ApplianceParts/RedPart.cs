using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedPart : CustomAppliancePart
    {
        public override string UniqueNameID => "redPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedPart>(
            scale: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override List<IAppliancePartProperty> Properties => new List<IAppliancePartProperty>()
        {
            new CNonDisposablePart()
        };

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
