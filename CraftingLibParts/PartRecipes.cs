using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
using System.Collections.Generic;
using static CraftingLibParts.BasicParts;
using static CraftingLibParts.FixedPrefabType;
using static CraftingLibParts.IntermediateParts;

namespace CraftingLibParts
{
    public abstract class StandardRecipe : CustomAppliancePartRecipe
    {
        public override sealed bool AllowCraftingDesk => true;
    }

    public static class SmelterRecipes
    {
        public abstract class BaseSmelterRecipe : CustomAppliancePartRecipe
        {
            //public override HashSet<Appliance> PossibleAppliances => // Smelter Appliance, TBC
            public override sealed bool AllowCraftingDesk => true; // Set to false when smelter appliance implemented
        }
        #region Nugget To Ingot
        public abstract class BaseNuggetToIngot<TNugget, TIngot> : BaseSmelterRecipe
            where TNugget : CustomAppliancePart, INugget
            where TIngot : CustomAppliancePart, IIngot
        {
            public virtual int NuggetQuantity { get; protected set; } = 2;
            public override sealed Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
            {
                { GDOUtils.GetCastedGDO<AppliancePart, TNugget>(), NuggetQuantity }
            };
            public override sealed AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, TIngot>();
        }
        public class AluminumNuggetToIngot : BaseNuggetToIngot<CraftingLibAluminumNugget, CraftingLibAluminumIngot>
        {
            public override string UniqueNameID => "aluminumNuggetToIngot";
        }
        public class TitaniumNuggetToIngot : BaseNuggetToIngot<CraftingLibTitaniumNugget, CraftingLibTitaniumIngot>
        {
            public override string UniqueNameID => "titaniumNuggetToIngot";
        }
        public class ChromiumNuggetToIngot : BaseNuggetToIngot<CraftingLibChromiumNugget, CraftingLibChromiumIngot>
        {
            public override string UniqueNameID => "chromiumNuggetToIngot";
        }
        public class IronNuggetToIngot : BaseNuggetToIngot<CraftingLibIronNugget, CraftingLibIronIngot>
        {
            public override string UniqueNameID => "ironNuggetToIngot";
        }
        public class CobaltNuggetToIngot : BaseNuggetToIngot<CraftingLibCobaltNugget, CraftingLibCobaltIngot>
        {
            public override string UniqueNameID => "cobaltNuggetToIngot";
        }
        public class CopperNuggetToIngot : BaseNuggetToIngot<CraftingLibCopperNugget, CraftingLibCopperIngot>
        {
            public override string UniqueNameID => "copperNuggetToIngot";
        }
        public class ZincNuggetToIngot : BaseNuggetToIngot<CraftingLibZincNugget, CraftingLibZincIngot>
        {
            public override string UniqueNameID => "zincNuggetToIngot";
        }
        public class SilverNuggetToIngot : BaseNuggetToIngot<CraftingLibSilverNugget, CraftingLibSilverIngot>
        {
            public override string UniqueNameID => "silverNuggetToIngot";
        }
        public class GoldNuggetToIngot : BaseNuggetToIngot<CraftingLibGoldNugget, CraftingLibGoldIngot>
        {
            public override string UniqueNameID => "goldNuggetToIngot";
        }
        #endregion

        public class SandToGlass : BaseSmelterRecipe
        {
            public override string UniqueNameID => "sandToGlass";
            public override Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>()
            {
                { Sand, 3 }
            };
            public override AppliancePart Result => GlassBlock;
        }

        #region Nugget To Ingot Alloying
        public abstract class TwoPartAlloying<TPart1, TPart2, TResult> : BaseSmelterRecipe
            where TPart1 : CustomAppliancePart
            where TPart2 : CustomAppliancePart
            where TResult : CustomAppliancePart
        {
            public virtual int Part1Quantity { get; protected set; } = 1;
            public virtual int Part2Quantity { get; protected set; } = 1;
            public override sealed Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
            {
                { GDOUtils.GetCastedGDO<AppliancePart, TPart1>(), Part1Quantity },
                { GDOUtils.GetCastedGDO<AppliancePart, TPart2>(), Part2Quantity }
            };
            public override sealed AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, TResult>();
        }
        public abstract class ThreePartAlloying<TPart1, TPart2, TPart3, TResult> : BaseSmelterRecipe
            where TPart1 : CustomAppliancePart
            where TPart2 : CustomAppliancePart
            where TPart3 : CustomAppliancePart
            where TResult : CustomAppliancePart
        {
            public virtual int Part1Quantity { get; protected set; } = 1;
            public virtual int Part2Quantity { get; protected set; } = 1;
            public virtual int Part3Quantity { get; protected set; } = 1;
            public override sealed Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
            {
                { GDOUtils.GetCastedGDO<AppliancePart, TPart1>(), Part1Quantity },
                { GDOUtils.GetCastedGDO<AppliancePart, TPart2>(), Part2Quantity },
                { GDOUtils.GetCastedGDO<AppliancePart, TPart3>(), Part3Quantity }
            };
            public override sealed AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, TResult>();
        }

        public class BrassAlloying : TwoPartAlloying<CraftingLibCopperNugget, CraftingLibZincNugget, CraftingLibBrassIngot>
        {
            public override string UniqueNameID => "brassAlloying";
            public override int Part1Quantity => 2;
        }
        public class BronzeAlloying : TwoPartAlloying<CraftingLibCopperNugget, CraftingLibTinNugget, CraftingLibBronzeIngot>
        {
            public override string UniqueNameID => "bronzeAlloying";
            public override int Part1Quantity => 2;
        }
        public class HighCarbonSteelAlloying : TwoPartAlloying<CraftingLibIronNugget, CraftingLibCarbon, CraftingLibHighCarbonSteelIngot>
        {
            public override string UniqueNameID => "highCarbonSteelAlloying";
            public override int Part2Quantity => 2;
        }
        public class StainlessSteelAlloying : ThreePartAlloying<CraftingLibIronNugget, CraftingLibCarbon, CraftingLibChromiumNugget, CraftingLibStainlessSteelIngot>
        {
            public override string UniqueNameID => "stainlessSteelAlloying";
        }
        #endregion
    }

    public static class CrusherRecipes
    {
        public abstract class BaseCrusherRecipe : CustomAppliancePartRecipe
        {
            //public override HashSet<Appliance> PossibleAppliances => // Crusher Appliance, TBC
            public override sealed bool AllowCraftingDesk => true; // Set to false when crusher appliance implemented
        }

        #region Crystal to Shard
        public abstract class BaseCrystalToShard<TCrystal, TShard> : BaseCrusherRecipe
            where TCrystal : CustomAppliancePart, ICrystal
            where TShard : CustomAppliancePart, IShard
        {
            public virtual int CrystalQuantity { get; protected set; } = 1;
            public override sealed Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
            {
                { GDOUtils.GetCastedGDO<AppliancePart, TCrystal>(), CrystalQuantity }
            };
            public override sealed AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, TShard>();
        }
        public class QuartzCrystalToShard : BaseCrystalToShard<CraftingLibQuartzCrystal, CraftingLibQuartzShard>
        {
            public override string UniqueNameID => "quartzCrystalToShard";
        }
        public class AmethystCrystalToShard : BaseCrystalToShard<CraftingLibAmethystCrystal, CraftingLibAmethystShard>
        {
            public override string UniqueNameID => "amethystCrystalToShard";
        }
        public class DiamondCrystalToShard : BaseCrystalToShard<CraftingLibDiamondCrystal, CraftingLibDiamondShard>
        {
            public override string UniqueNameID => "diamondCrystalToShard";
        }
        public class EmeraldCrystalToShard : BaseCrystalToShard<CraftingLibEmeraldCrystal, CraftingLibEmeraldShard>
        {
            public override string UniqueNameID => "emeraldCrystalToShard";
        }
        public class SapphireCrystalToShard : BaseCrystalToShard<CraftingLibSapphireCrystal, CraftingLibSapphireShard>
        {
            public override string UniqueNameID => "sapphireCrystalToShard";
        }
        public class TopazCrystalToShard : BaseCrystalToShard<CraftingLibTopazCrystal, CraftingLibTopazShard>
        {
            public override string UniqueNameID => "topazCrystalToShard";
        }
        public class GarnetCrystalToShard : BaseCrystalToShard<CraftingLibGarnetCrystal, CraftingLibGarnetShard>
        {
            public override string UniqueNameID => "garnetCrystalToShard";
        }
        public class AquamarineCrystalToShard : BaseCrystalToShard<CraftingLibAquamarineCrystal, CraftingLibAquamarineShard>
        {
            public override string UniqueNameID => "aquamarineCrystalToShard";
        }
        public class OpalCrystalToShard : BaseCrystalToShard<CraftingLibOpalCrystal, CraftingLibOpalShard>
        {
            public override string UniqueNameID => "opalCrystalToShard";
        }
        public class PeridotCrystalToShard : BaseCrystalToShard<CraftingLibPeridotCrystal, CraftingLibPeridotShard>
        {
            public override string UniqueNameID => "peridotCrystalToShard";
        }
        public class PyriteCrystalToShard : BaseCrystalToShard<CraftingLibPyriteCrystal, CraftingLibPyriteShard>
        {
            public override string UniqueNameID => "pyriteCrystalToShard";
        }
        public class BerylCrystalToShard : BaseCrystalToShard<CraftingLibBerylCrystal, CraftingLibBerylShard>
        {
            public override string UniqueNameID => "berylCrystalToShard";
        }
        public class OnyxCrystalToShard : BaseCrystalToShard<CraftingLibOnyxCrystal, CraftingLibOnyxShard>
        {
            public override string UniqueNameID => "onyxCrystalToShard";
        }

        #endregion
    }
}
