using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static CraftingLibParts.FixedPrefabType;

namespace CraftingLibParts
{
    public abstract class StandardPart : CustomAppliancePart
    {
        public override sealed GameObject Prefab => PrefabRegistry.GetPrefab(GetType(), material: PrefabMaterial);
        public virtual Material PrefabMaterial { get; protected set; } = MaterialUtils.GetExistingMaterial("Metal");
    }

    public abstract class BasicPart : StandardPart
    {
        public sealed override bool IsPurchasable => true;
    }

    public abstract class IntermediatePart : StandardPart
    {
        public sealed override bool IsPurchasable => false;
    }

    public static class BasicParts
    {
        #region Nuggets
        public abstract class BasicNugget : BasicPart, INugget
        {
        }

        public static AppliancePart AluminumNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAluminumNugget>();
        public class CraftingLibAluminumNugget : BasicNugget
        {
            public override string UniqueNameID => "aluminumNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Aluminum Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart TitaniumNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTitaniumNugget>();
        public class CraftingLibTitaniumNugget : BasicNugget
        {
            public override string UniqueNameID => "titaniumNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Titanium Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart ChromiumNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibChromiumNugget>();
        public class CraftingLibChromiumNugget : BasicNugget
        {
            public override string UniqueNameID => "chromiumNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Chromium Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart IronNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibIronNugget>();
        public class CraftingLibIronNugget : BasicNugget
        {
            public override string UniqueNameID => "ironNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal Dark");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Iron Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart CobaltNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibCobaltNugget>();
        public class CraftingLibCobaltNugget : BasicNugget
        {
            public override string UniqueNameID => "cobaltNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal- Shiny Blue");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Cobalt Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart CopperNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibCopperNugget>();
        public class CraftingLibCopperNugget : BasicNugget
        {
            public override string UniqueNameID => "copperNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal - Copper");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Copper Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart ZincNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibZincNugget>();
        public class CraftingLibZincNugget : BasicNugget
        {
            public override string UniqueNameID => "zincNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Zinc Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart SilverNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSilverNugget>();
        public class CraftingLibSilverNugget : BasicNugget
        {
            public override string UniqueNameID => "silverNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal- Shiny");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Silver Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart TinNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTinNugget>();
        public class CraftingLibTinNugget : BasicNugget
        {
            public override string UniqueNameID => "tinNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Tin Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart GoldNugget => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGoldNugget>();
        public class CraftingLibGoldNugget : BasicNugget
        {
            public override string UniqueNameID => "goldNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Shiny Gold");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Gold Nugget",
                    Description = "Ferrous metal"
                })
            };
        }

        public static AppliancePart Carbon => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibCarbon>();
        public class CraftingLibCarbon : BasicNugget
        {
            public override string UniqueNameID => "carbonNugget";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Carbon",
                    Description = "Ferrous metal"
                })
            };
        }
        #endregion

        #region Crystals
        public abstract class BasicCrystal : BasicPart, ICrystal
        {
        }

        public static AppliancePart QuartzCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibQuartzCrystal>();
        public class CraftingLibQuartzCrystal : BasicCrystal
        {
            public override string UniqueNameID => "quartzCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Quartz",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AmethystCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAmethystCrystal>();
        public class CraftingLibAmethystCrystal : BasicCrystal
        {
            public override string UniqueNameID => "amethystCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Amethyst",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart DiamondCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibDiamondCrystal>();
        public class CraftingLibDiamondCrystal : BasicCrystal
        {
            public override string UniqueNameID => "diamondCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Diamond",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart EmeraldCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibEmeraldCrystal>();
        public class CraftingLibEmeraldCrystal : BasicCrystal
        {
            public override string UniqueNameID => "emeraldCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Emerald",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart SapphireCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSapphireCrystal>();
        public class CraftingLibSapphireCrystal : BasicCrystal
        {
            public override string UniqueNameID => "sapphireCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Sapphire",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart TopazCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTopazCrystal>();
        public class CraftingLibTopazCrystal : BasicCrystal
        {
            public override string UniqueNameID => "topazCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Topaz",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart GarnetCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGarnetCrystal>();
        public class CraftingLibGarnetCrystal : BasicCrystal
        {
            public override string UniqueNameID => "garnetCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Garnet",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AquamarineCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAquamarineCrystal>();
        public class CraftingLibAquamarineCrystal : BasicCrystal
        {
            public override string UniqueNameID => "aquamarineCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Aquamarine",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OpalCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOpalCrystal>();
        public class CraftingLibOpalCrystal : BasicCrystal
        {
            public override string UniqueNameID => "opalCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Opal",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PeridotCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPeridotCrystal>();
        public class CraftingLibPeridotCrystal : BasicCrystal
        {
            public override string UniqueNameID => "peridotCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Peridot",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PyriteCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPyriteCrystal>();
        public class CraftingLibPyriteCrystal : BasicCrystal
        {
            public override string UniqueNameID => "pyriteCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Pyrite",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart BerylCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibBerylCrystal>();
        public class CraftingLibBerylCrystal : BasicCrystal
        {
            public override string UniqueNameID => "berylCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Beryl",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OnyxCrystal => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOnyxCrystal>();
        public class CraftingLibOnyxCrystal : BasicCrystal
        {
            public override string UniqueNameID => "onyxCrystal";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Rough Onyx",
                    Description = "Mineral"
                })
            };
        }
        #endregion

        public static AppliancePart Wood => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibWood>();
        public class CraftingLibWood : BasicPart, ILog
        {
            public override string UniqueNameID => "basicPartWood";
            public override int PurchaseCost => 0;

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Wood - Log");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Wood",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart Sand => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSand>();
        public class CraftingLibSand : BasicPart, IBlock
        {
            public override string UniqueNameID => "basicPartSand";
            public override int PurchaseCost => 0;

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Sand",
                    Description = "Mineral"
                })
            };
        }
    }

    public static class IntermediateParts
    {
        #region Ingots
        public abstract class IntermediateIngot : IntermediatePart, IIngot
        {
        }

        public static AppliancePart AluminumIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAluminumIngot>();
        public class CraftingLibAluminumIngot : IntermediateIngot
        {
            public override string UniqueNameID => "aluminumIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Aluminum Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart TitaniumIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTitaniumIngot>();
        public class CraftingLibTitaniumIngot : IntermediateIngot
        {
            public override string UniqueNameID => "titaniumIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Titanium Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart ChromiumIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibChromiumIngot>();
        public class CraftingLibChromiumIngot : IntermediateIngot
        {
            public override string UniqueNameID => "chromiumIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Chromium Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart IronIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibIronIngot>();
        public class CraftingLibIronIngot : IntermediateIngot
        {
            public override string UniqueNameID => "ironIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal Dark");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Iron Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart CobaltIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibCobaltIngot>();
        public class CraftingLibCobaltIngot : IntermediateIngot
        {
            public override string UniqueNameID => "cobaltIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal- Shiny Blue");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Cobalt Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart CopperIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibCopperIngot>();
        public class CraftingLibCopperIngot : IntermediateIngot
        {
            public override string UniqueNameID => "copperIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal - Copper");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Copper Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart ZincIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibZincIngot>();
        public class CraftingLibZincIngot : IntermediateIngot
        {
            public override string UniqueNameID => "zincIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Dark Yellow");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Zinc Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart SilverIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSilverIngot>();
        public class CraftingLibSilverIngot : IntermediateIngot
        {
            public override string UniqueNameID => "silverIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal- Shiny");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Silver Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart TinIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTinIngot>();
        public class CraftingLibTinIngot : IntermediateIngot
        {
            public override string UniqueNameID => "tinIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Tin Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart GoldIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGoldIngot>();
        public class CraftingLibGoldIngot : IntermediateIngot
        {
            public override string UniqueNameID => "goldIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Plastic - Shiny Gold");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Gold Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart BrassIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibBrassIngot>();
        public class CraftingLibBrassIngot : IntermediateIngot
        {
            public override string UniqueNameID => "brassIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal - Brass");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Brass Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart BronzeIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibBronzeIngot>();
        public class CraftingLibBronzeIngot : IntermediateIngot
        {
            public override string UniqueNameID => "bronzeIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Bronze Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart CarbonSteelIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibHighCarbonSteelIngot>();
        public class CraftingLibHighCarbonSteelIngot : IntermediateIngot
        {
            public override string UniqueNameID => "highCarbonSteelIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "High Carbon Steel Ingot",
                    Description = "Ingot"
                })
            };
        }

        public static AppliancePart StainlessSteelIngot => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibStainlessSteelIngot>();
        public class CraftingLibStainlessSteelIngot : IntermediateIngot
        {
            public override string UniqueNameID => "stainlessSteelIngot";

            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Stainless Steel Ingot",
                    Description = "Ingot"
                })
            };
        }
        #endregion

        #region Blocks
        public abstract class IntermediateBlock : IntermediatePart, IBlock
        {
        }

        public static AppliancePart GlassBlock => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGlassBlock>();
        public class CraftingLibGlassBlock : IntermediateBlock
        {
            public override string UniqueNameID => "intermediatePartGlassBlock";
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Door GlassBlock");
            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Glass Block",
                    Description = "Clear as day"
                })
            };
        }
        #endregion

        #region Plates
        public abstract class IntermediatePlate : IntermediatePart, IPlate
        {
        }
        #endregion

        #region Rods
        public abstract class IntermediateRod : IntermediatePart, IRod
        {
        }
        #endregion

        #region Shards
        public abstract class IntermediateShard : IntermediatePart, IShard
        {
        }

        public static AppliancePart QuartzShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibQuartzShard>();
        public class CraftingLibQuartzShard : IntermediateShard
        {
            public override string UniqueNameID => "quartzShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Quartz Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AmethystShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAmethystShard>();
        public class CraftingLibAmethystShard : IntermediateShard
        {
            public override string UniqueNameID => "amethystShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Amethyst Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart DiamondShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibDiamondShard>();
        public class CraftingLibDiamondShard : IntermediateShard
        {
            public override string UniqueNameID => "diamondShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Diamond Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart EmeraldShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibEmeraldShard>();
        public class CraftingLibEmeraldShard : IntermediateShard
        {
            public override string UniqueNameID => "emeraldShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Emerald Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart SapphireShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSapphireShard>();
        public class CraftingLibSapphireShard : IntermediateShard
        {
            public override string UniqueNameID => "sapphireShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Sapphire Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart TopazShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTopazShard>();
        public class CraftingLibTopazShard : IntermediateShard
        {
            public override string UniqueNameID => "topazShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Topaz Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart GarnetShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGarnetShard>();
        public class CraftingLibGarnetShard : IntermediateShard
        {
            public override string UniqueNameID => "garnetShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Garnet Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AquamarineShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAquamarineShard>();
        public class CraftingLibAquamarineShard : IntermediateShard
        {
            public override string UniqueNameID => "aquamarineShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Aquamarine Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OpalShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOpalShard>();
        public class CraftingLibOpalShard : IntermediateShard
        {
            public override string UniqueNameID => "opalShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Opal Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PeridotShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPeridotShard>();
        public class CraftingLibPeridotShard : IntermediateShard
        {
            public override string UniqueNameID => "peridotShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Peridot Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PyriteShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPyriteShard>();
        public class CraftingLibPyriteShard : IntermediateShard
        {
            public override string UniqueNameID => "pyriteShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Pyrite Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart BerylShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibBerylShard>();
        public class CraftingLibBerylShard : IntermediateShard
        {
            public override string UniqueNameID => "berylShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Beryl Shard",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OnyxShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOnyxShard>();
        public class CraftingLibOnyxShard : IntermediateShard
        {
            public override string UniqueNameID => "onyxShard";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Onyx Shard",
                    Description = "Mineral"
                })
            };
        }
        #endregion


        #region Faceted Crystals
        public abstract class IntermediateFaceted : IntermediatePart, IIngot
        {
        }

        public static AppliancePart QuartzFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibQuartzFaceted>();
        public class CraftingLibQuartzFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "quartzFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Quartz",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AmethystFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAmethystFaceted>();
        public class CraftingLibAmethystFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "amethystFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Amethyst",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart DiamondFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibDiamondFaceted>();
        public class CraftingLibDiamondFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "diamondFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Diamond",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart EmeraldFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibEmeraldFaceted>();
        public class CraftingLibEmeraldFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "emeraldFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Emerald",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart SapphireFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibSapphireFaceted>();
        public class CraftingLibSapphireFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "sapphireFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Sapphire",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart TopazFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibTopazFaceted>();
        public class CraftingLibTopazFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "topazFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Topaz",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart GarnetFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGarnetFaceted>();
        public class CraftingLibGarnetFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "garnetFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Garnet",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart AquamarineFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibAquamarineFaceted>();
        public class CraftingLibAquamarineFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "aquamarineFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Aquamarine",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OpalFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOpalFaceted>();
        public class CraftingLibOpalFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "opalFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Opal",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PeridotFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPeridotFaceted>();
        public class CraftingLibPeridotFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "peridotFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Peridot",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart PyriteFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibPyriteFaceted>();
        public class CraftingLibPyriteFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "pyriteFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Pyrite",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart BerylFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibBerylFaceted>();
        public class CraftingLibBerylFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "berylFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Beryl",
                    Description = "Mineral"
                })
            };
        }

        public static AppliancePart OnyxFaceted => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibOnyxFaceted>();
        public class CraftingLibOnyxFaceted : IntermediateFaceted
        {
            public override string UniqueNameID => "onyxFaceted";
            public override int PurchaseCost => 0;
            public override Material PrefabMaterial => MaterialUtils.GetExistingMaterial("Metal");

            public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>
            {
                (Locale.English, new BasicInfo()
                {
                    Name = "Faceted Onyx",
                    Description = "Mineral"
                })
            };
        }
        #endregion
    }
}
