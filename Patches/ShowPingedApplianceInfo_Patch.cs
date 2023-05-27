using CraftingLib.Systems;
using HarmonyLib;
using Kitchen;

namespace CraftingLib.Patches
{
    [HarmonyPatch]
    static class ShowPingedApplianceInfo_Patch
    {
        [HarmonyPatch(typeof(ShowPingedApplianceInfo), "IsPossible")]
        [HarmonyPrefix]
        static bool IsPossible_Prefix(ref InteractionData data)
        {
            return !(PatchController.StaticHas<CPartialAppliance>(data.Target) || PatchController.StaticHas<CAppliancePartVendor>(data.Target) || PatchController.StaticHas<CShowApplianceContainerInfo>(data.Target));
        }
    }
}
