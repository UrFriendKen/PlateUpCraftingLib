﻿using HarmonyLib;
using Kitchen;

namespace CraftingLib.Patches
{
    [HarmonyPatch]
    static class StartNewDay_Patch
    {
        [HarmonyPatch(typeof(StartNewDay), "OnUpdate")]
        [HarmonyPrefix]
        static bool OnUpdate_Prefix()
        {
            return !SStartDayWarnings_Patch.AppliancePartsPresent.IsBlocking();
        }
    }
}
