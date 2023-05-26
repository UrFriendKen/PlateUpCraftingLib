using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;
using static CraftingLibParts.FixedPrefabType;

namespace CraftingLibParts
{
    public abstract class StandardPart : CustomAppliancePart { }

    public abstract class BasicPart : StandardPart
    {
        public override sealed GameObject Prefab => PrefabRegistry.GetPrefab(GetType(), material: PrefabMaterial);
        public virtual Material PrefabMaterial { get; protected set; } = MaterialUtils.GetExistingMaterial("Metal");
        public sealed override bool IsPurchasable => true;
    }

    public abstract class IntermediatePart : StandardPart
    {
        public sealed override bool IsPurchasable => false;
    }

    public static class BasicParts
    {
        #region Nuggets
        public static AppliancePart CraftingLibIron => GDOUtils.GetCastedGDO<AppliancePart, Iron>();
        public class Iron : BasicPart, ICrystal
        {
            public override string UniqueNameID => "basicPartIron";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Crystal",
                    Description = "Ferrous metal"
                })
            };
        }
        public static AppliancePart CraftingLibIron2 => GDOUtils.GetCastedGDO<AppliancePart, Iron2>();
        public class Iron2 : BasicPart, IFaceted
        {
            public override string UniqueNameID => "basicPartIron2";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Gem",
                    Description = "Ferrous metal"
                })
            };
        }
        public static AppliancePart CraftingLibIron3 => GDOUtils.GetCastedGDO<AppliancePart, Iron3>();
        public class Iron3 : BasicPart, IShard
        {
            public override string UniqueNameID => "basicPartIron3";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Shard",
                    Description = "Ferrous metal"
                })
            };
        }
        public static AppliancePart CraftingLibIron4 => GDOUtils.GetCastedGDO<AppliancePart, Iron4>();
        public class Iron4 : BasicPart, ILog
        {
            public override string UniqueNameID => "basicPartIron4";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Log",
                    Description = "Ferrous metal"
                })
            };
        }
        public static AppliancePart CraftingLibIron5 => GDOUtils.GetCastedGDO<AppliancePart, Iron5>();
        public class Iron5 : BasicPart, IStick
        {
            public override string UniqueNameID => "basicPartIron5";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Stick",
                    Description = "Ferrous metal"
                })
            };
        }
        public static AppliancePart CraftingLibIron6 => GDOUtils.GetCastedGDO<AppliancePart, Iron6>();
        public class Iron6 : BasicPart, IBlock
        {
            public override string UniqueNameID => "basicPartIron6";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Block",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart CraftingLibAluminum => GDOUtils.GetCastedGDO<AppliancePart, Iron>();
        public class Aluminum : BasicPart
        {
            public override string UniqueNameID => "basicPartAluminum";
            public override int PurchaseCost => 5;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Aluminum",
                    Description = "Lightweight metal"
                })
            };
        }

        public static AppliancePart CraftingLibGold => GDOUtils.GetCastedGDO<AppliancePart, Gold>();
        public class Gold : BasicPart, IShard
        {
            public override string UniqueNameID => "basicPartGold";
            public override int PurchaseCost => 20;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Shiny Gold");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Gold",
                    Description = "Shiny metal"
                })
            };
        }
        #endregion

        #region Uncut Gem
        public abstract class BaseUncutGem<T> : BasicPart
        {
            public virtual Material PrefabMaterial { get; protected set; } = MaterialUtils.GetExistingMaterial("Metal");
        }
        #endregion

        public static AppliancePart CraftingLibWood => GDOUtils.GetCastedGDO<AppliancePart, Wood>();
        public class Wood : BasicPart, ILog
        {
            public override string UniqueNameID => "basicPartWood";
            public override int PurchaseCost => 5;

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Wood - Log");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Wood",
                    Description = "#SayNoToDeforestation"
                })
            };
        }

        public static AppliancePart CraftingLibSand => GDOUtils.GetCastedGDO<AppliancePart, Sand>();
        public class Sand : BasicPart, IFaceted
        {
            public override string UniqueNameID => "basicPartSand";
            public override int PurchaseCost => 3;

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Sand",
                    Description = "Definitely not beach sand"
                })
            };
        }
    }

    public static class IntermediateParts
    {
        #region Bars
        public abstract class BaseBar<T> : IntermediatePart where T : BaseBar<T>
        {
            public override sealed GameObject Prefab => TestCubeManager.GetPrefab<T>(scaleX: 0.5f, scaleY: 0.35f, scaleZ: 0.2f, PrefabMaterial, collider: false);
            public virtual Material PrefabMaterial { get; protected set; } = MaterialUtils.GetExistingMaterial("Metal");
        }
        #endregion

        public static AppliancePart CraftingLibGlass => GDOUtils.GetCastedGDO<AppliancePart, Glass>();
        public class Glass : IntermediatePart
        {
            public override string UniqueNameID => "intermediatePartGlass";
            public override GameObject Prefab => TestCubeManager.GetPrefab<Glass>(scaleX: 0.2f, scaleY: 0.2f, scaleZ: 0.2f, MaterialUtils.GetExistingMaterial("Door Glass"), collider: false);

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Glass",
                    Description = "Clear as day"
                })
            };
        }
    }
}
