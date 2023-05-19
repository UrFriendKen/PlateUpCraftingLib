using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;

namespace CraftingLibParts
{
    public abstract class StandardPart : CustomAppliancePart { }

    public abstract class BasicPart : StandardPart { }

    public static class BasicParts
    {
        public static AppliancePart CraftingLibIron => GDOUtils.GetCastedGDO<AppliancePart, Iron>();
        public class Iron : BasicPart
        {
            public override string UniqueNameID => "basicPartIron";
            public override bool IsPurchasable => true;
            public override int PurchaseCost => 5;
            public override GameObject Prefab => TestCubeManager.GetPrefab<Iron>(
                scaleX: 0.5f, scaleY: 0.35f, scaleZ: 0.2f, collider: false);

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Iron",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart CraftingLibGold => GDOUtils.GetCastedGDO<AppliancePart, Gold>();
        public class Gold : BasicPart
        {
            public override string UniqueNameID => "basicPartGold";
            public override bool IsPurchasable => true;
            public override int PurchaseCost => 10;
            public override GameObject Prefab => TestCubeManager.GetPrefab<Gold>(scaleX: 0.5f, scaleY: 0.35f, scaleZ: 0.2f, MaterialUtils.GetExistingMaterial("Plastic - Yellow"), collider: false);

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Gold",
                    Description = "Shiny metal"
                })
            };
        }
    }
}
