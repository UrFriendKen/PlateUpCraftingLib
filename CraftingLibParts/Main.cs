using KitchenLib;
using KitchenLib.Event;
using KitchenMods;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace CraftingLibParts
{
    public class Main : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "IcedMilo.PlateUp.CraftingLibParts";
        public const string MOD_NAME = "CraftingLib Parts";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "IcedMilo";
        public const string MOD_GAMEVERSION = ">=1.1.5";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until

        public static AssetBundle Bundle;

        public Main() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            // Basic Parts
            AddGameDataObject<BasicParts.CraftingLibAluminumNugget>();
            AddGameDataObject<BasicParts.CraftingLibTitaniumNugget>();
            AddGameDataObject<BasicParts.CraftingLibChromiumNugget>();
            AddGameDataObject<BasicParts.CraftingLibIronNugget>();
            AddGameDataObject<BasicParts.CraftingLibCobaltNugget>();
            AddGameDataObject<BasicParts.CraftingLibCopperNugget>();
            AddGameDataObject<BasicParts.CraftingLibZincNugget>();
            AddGameDataObject<BasicParts.CraftingLibSilverNugget>();
            AddGameDataObject<BasicParts.CraftingLibTinNugget>();
            AddGameDataObject<BasicParts.CraftingLibGoldNugget>();
            AddGameDataObject<BasicParts.CraftingLibCarbon>();

            AddGameDataObject<BasicParts.CraftingLibQuartzCrystal>();
            AddGameDataObject<BasicParts.CraftingLibAmethystCrystal>();
            AddGameDataObject<BasicParts.CraftingLibDiamondCrystal>();
            AddGameDataObject<BasicParts.CraftingLibEmeraldCrystal>();
            AddGameDataObject<BasicParts.CraftingLibSapphireCrystal>();
            AddGameDataObject<BasicParts.CraftingLibTopazCrystal>();
            AddGameDataObject<BasicParts.CraftingLibGarnetCrystal>();
            AddGameDataObject<BasicParts.CraftingLibAquamarineCrystal>();
            AddGameDataObject<BasicParts.CraftingLibOpalCrystal>();
            AddGameDataObject<BasicParts.CraftingLibPeridotCrystal>();
            AddGameDataObject<BasicParts.CraftingLibPyriteCrystal>();
            AddGameDataObject<BasicParts.CraftingLibBerylCrystal>();
            AddGameDataObject<BasicParts.CraftingLibOnyxCrystal>();

            AddGameDataObject<BasicParts.CraftingLibSand>();



            // Intermediate Parts
            AddGameDataObject<IntermediateParts.CraftingLibAluminumIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibTitaniumIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibChromiumIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibIronIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibCobaltIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibCopperIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibZincIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibSilverIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibTinIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibGoldIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibBrassIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibBronzeIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibHighCarbonSteelIngot>();
            AddGameDataObject<IntermediateParts.CraftingLibStainlessSteelIngot>();

            AddGameDataObject<IntermediateParts.CraftingLibQuartzShard>();
            AddGameDataObject<IntermediateParts.CraftingLibAmethystShard>();
            AddGameDataObject<IntermediateParts.CraftingLibDiamondShard>();
            AddGameDataObject<IntermediateParts.CraftingLibEmeraldShard>();
            AddGameDataObject<IntermediateParts.CraftingLibSapphireShard>();
            AddGameDataObject<IntermediateParts.CraftingLibTopazShard>();
            AddGameDataObject<IntermediateParts.CraftingLibGarnetShard>();
            AddGameDataObject<IntermediateParts.CraftingLibAquamarineShard>();
            AddGameDataObject<IntermediateParts.CraftingLibOpalShard>();
            AddGameDataObject<IntermediateParts.CraftingLibPeridotShard>();
            AddGameDataObject<IntermediateParts.CraftingLibPyriteShard>();
            AddGameDataObject<IntermediateParts.CraftingLibBerylShard>();
            AddGameDataObject<IntermediateParts.CraftingLibOnyxShard>();

            AddGameDataObject<IntermediateParts.CraftingLibQuartzFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibAmethystFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibDiamondFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibEmeraldFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibSapphireFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibTopazFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibGarnetFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibAquamarineFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibOpalFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibPeridotFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibPyriteFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibBerylFaceted>();
            AddGameDataObject<IntermediateParts.CraftingLibOnyxFaceted>();

            AddGameDataObject<IntermediateParts.CraftingLibGlassBlock>();



            // Smelter Recipes
            AddGameDataObject<SmelterRecipes.AluminumNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.TitaniumNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.ChromiumNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.IronNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.CobaltNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.CopperNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.ZincNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.SilverNuggetToIngot>();
            AddGameDataObject<SmelterRecipes.GoldNuggetToIngot>();

            AddGameDataObject<SmelterRecipes.SandToGlass>();

            AddGameDataObject<SmelterRecipes.BrassAlloying>();
            AddGameDataObject<SmelterRecipes.BronzeAlloying>();
            AddGameDataObject<SmelterRecipes.HighCarbonSteelAlloying>();
            AddGameDataObject<SmelterRecipes.StainlessSteelAlloying>();


            // Crusher Recipes
            AddGameDataObject<CrusherRecipes.QuartzCrystalToShard>();
            AddGameDataObject<CrusherRecipes.AmethystCrystalToShard>();
            AddGameDataObject<CrusherRecipes.DiamondCrystalToShard>();
            AddGameDataObject<CrusherRecipes.EmeraldCrystalToShard>();
            AddGameDataObject<CrusherRecipes.SapphireCrystalToShard>();
            AddGameDataObject<CrusherRecipes.TopazCrystalToShard>();
            AddGameDataObject<CrusherRecipes.GarnetCrystalToShard>();
            AddGameDataObject<CrusherRecipes.AquamarineCrystalToShard>();
            AddGameDataObject<CrusherRecipes.OpalCrystalToShard>();
            AddGameDataObject<CrusherRecipes.PeridotCrystalToShard>();
            AddGameDataObject<CrusherRecipes.PyriteCrystalToShard>();
            AddGameDataObject<CrusherRecipes.BerylCrystalToShard>();
            AddGameDataObject<CrusherRecipes.OnyxCrystalToShard>();


            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // TODO: Uncomment the following if you have an asset bundle.
            // TODO: Also, make sure to set EnableAssetBundleDeploy to 'true' in your ModName.csproj

            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            LogInfo("Done loading asset bundle.");

            // Register custom GDOs
            AddGameData();

            // Perform actions when game data is built
            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
            };
        }
        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
