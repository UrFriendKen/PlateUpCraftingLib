using CraftingLib.Systems;
using HarmonyLib;
using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingLib.Patches
{
    [HarmonyPatch]
    static class ShowPingedApplianceInfo_Patch
    {
        [HarmonyPatch(typeof(ShowPingedApplianceInfo), "IsPossible")]
        [HarmonyPrefix]
        static bool IsPossible_Prefix(ref InteractionData data)
        {
            return !PatchController.HasCPartialAppliance(data.Target);
        }
    }
}
