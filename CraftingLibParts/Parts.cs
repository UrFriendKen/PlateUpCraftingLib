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

        #region Shards
        public abstract class BasicShard : BasicPart, IShard
        {
        }

        public static AppliancePart QuartzShard => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibQuartzShard>();
        public class CraftingLibQuartzShard : BasicShard
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
        public class CraftingLibAmethystShard : BasicShard
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
        public class CraftingLibDiamondShard : BasicShard
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
        public class CraftingLibEmeraldShard : BasicShard
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
        public class CraftingLibSapphireShard : BasicShard
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
        public class CraftingLibTopazShard : BasicShard
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
        public class CraftingLibGarnetShard : BasicShard
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
        public class CraftingLibAquamarineShard : BasicShard
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
        public class CraftingLibOpalShard : BasicShard
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
        public class CraftingLibPeridotShard : BasicShard
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
        public class CraftingLibPyriteShard : BasicShard
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
        public class CraftingLibBerylShard : BasicShard
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
        public class CraftingLibOnyxShard : BasicShard
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

        public static AppliancePart GlassBlock => GDOUtils.GetCastedGDO<AppliancePart, CraftingLibGlassBlock>();
        public class CraftingLibGlassBlock : IntermediatePart, IBlock
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
    }
}
