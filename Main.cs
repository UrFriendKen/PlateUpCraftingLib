﻿using CraftingLib.Customs.CraftingDesk;
using CraftingLib.Customs.VendingMachine;
using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.Utils;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace CraftingLib
{
    public class Main : BaseMod, IModSystem
    {
        // GUID must be unique and is recommended to be in reverse domain name notation
        // Mod Name is displayed to the player and listed in the mods menu
        // Mod Version must follow semver notation e.g. "1.2.3"
        public const string MOD_GUID = "IcedMilo.PlateUp.CraftingLib";
        public const string MOD_NAME = "CraftingLib";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "IcedMilo";
        public const string MOD_GAMEVERSION = ">=1.1.5";
        // Game version this mod is designed for in semver
        // e.g. ">=1.1.3" current and all future
        // e.g. ">=1.1.3 <=1.2.3" for all from/until

        public static AssetBundle Bundle;

        public static readonly ViewType AppliancePartContainerInfoViewType = (ViewType)VariousUtils.GetID($"{MOD_GUID}:PartialApplianceInfo");
        public static readonly ViewType AppliancePartInfoViewType = (ViewType)VariousUtils.GetID($"{MOD_GUID}:AppliancePartInfo");
        public static readonly ViewType AppliancePartViewType = (ViewType)VariousUtils.GetID($"{MOD_GUID}:AppliancePart");

        public static readonly Dictionary<string, string> GlobalLocalisationTexts = new Dictionary<string, string>()
        {
            { "PARTIAL_APPLIANCE_COMPLETABLE", "Recipe Completed" }
        };

        internal static StartDayWarning AppliancePartWarning = (StartDayWarning)VariousUtils.GetID("AppliancePartWarning");
        public static readonly Dictionary<StartDayWarning, GenericLocalisationStruct> StartDayWarningLocalisationTexts = new Dictionary<StartDayWarning, GenericLocalisationStruct>()
        {
            { AppliancePartWarning, new GenericLocalisationStruct()
                {
                    Name = "Unused appliance parts",
                    Description = "Use or dispose appliance parts before starting the day"
                }
            }
        };

        public Main() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // TODO: Uncomment the following if you have an asset bundle.
            // TODO: Also, make sure to set EnableAssetBundleDeploy to 'true' in your ModName.csproj

            //LogInfo("Attempting to load asset bundle...");
            //Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).First();
            //LogInfo("Done loading asset bundle.");

            AddGameDataObject<VendingMachine>();
            AddGameDataObject<CraftingDesk>();
            AddGameDataObject<CraftingDeskRecipeGroup>();

            // Perform actions when game data is built

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                Dictionary<string, string> baseGameTexts = args.gamedata.GlobalLocalisation.Text;
                foreach (KeyValuePair<string, string> text in GlobalLocalisationTexts)
                {
                    if (baseGameTexts.ContainsKey(text.Key))
                        continue;
                    baseGameTexts.Add(text.Key, text.Value);
                }


                Dictionary<StartDayWarning, GenericLocalisationStruct> startDayWarningTexts = args.gamedata.GlobalLocalisation.StartDayWarningLocalisation.Text;
                foreach (KeyValuePair<StartDayWarning, GenericLocalisationStruct> text in StartDayWarningLocalisationTexts)
                {
                    if (startDayWarningTexts.ContainsKey(text.Key))
                        continue;
                    startDayWarningTexts.Add(text.Key, text.Value);
                }

                AppliancePartRecipeGroup craftingDeskRecipeGroup = GDOUtils.GetCustomGameDataObject<CraftingDeskRecipeGroup>().GameDataObject as AppliancePartRecipeGroup;
                craftingDeskRecipeGroup.Recipes = args.gamedata.Get<AppliancePartRecipe>().Where(x => x.AllowCraftingDesk && x.Result != null && x.Inputs.Count > 0).ToHashSet();
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
