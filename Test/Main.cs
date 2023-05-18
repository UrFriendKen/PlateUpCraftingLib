using CraftingLib;
using Kitchen;
using KitchenCraftingLibTest.Customs.ApplianceParts;
using KitchenCraftingLibTest.Customs.ApplianceRecipes;
using KitchenCraftingLibTest.Customs.PartialAppliances;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.References;
using KitchenMods;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace KitchenCraftingLibTest
{
    public class Main : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "IcedMilo.PlateUp.CraftingLibTest";
        public const string MOD_NAME = "CraftingLibTest";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "IcedMilo";
        public const string MOD_GAMEVERSION = ">=1.1.5";

        public Main() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            AddGameDataObject<RedPart>();
            AddGameDataObject<RedProvider>();
            AddGameDataObject<YellowPart>();
            AddGameDataObject<YellowProvider>();

            AddGameDataObject<TestStage1ToNeon>();
            AddGameDataObject<TestStage1ToTestStage2>();
            AddGameDataObject<TestStage2ToSafetyHob>();
            AddGameDataObject<TestStage1>();
            AddGameDataObject<TestStage2>();

            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // Register custom GDOs
            AddGameData();

            // Perform actions when game data is built
            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                if (args.gamedata.TryGet(ApplianceReferences.Grabber, out Appliance grabber))
                {
                    if (!grabber.GetProperty(out CPartAttachmentPoint _))
                        grabber.Properties.Add(new CPartAttachmentPoint()
                        {
                            MaxAttachmentCount = 5
                        });
                }
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
