using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
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
            public override GameObject Prefab => TestCubeManager.GetPrefab<Iron>(
                scaleX: 0.5f, scaleY: 0.35f, scaleZ: 0.2f, collider: false);
        }

        public static AppliancePart CraftingLibGold => GDOUtils.GetCastedGDO<AppliancePart, Gold>();
        public class Gold : BasicPart
        {
            public override string UniqueNameID => "basicPartGold";
            public override GameObject Prefab => TestCubeManager.GetPrefab<Gold>(scaleX: 0.5f, scaleY: 0.35f, scaleZ: 0.2f, MaterialUtils.GetExistingMaterial("Plastic - Yellow"), collider: false);
        }
    }
}
