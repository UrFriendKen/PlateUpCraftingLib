using CraftingLib.Systems;
using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;
using Unity.Entities;

namespace CraftingLib.Patches
{
    [HarmonyPatch]
    internal static class SStartDayWarnings_Patch
    {
        static EntityQuery ApplianceParts;

        internal static WarningLevel AppliancePartsPresent = WarningLevel.Unknown;

        [HarmonyPatch(typeof(SStartDayWarnings), "Primary", MethodType.Getter)]
        [HarmonyPostfix]
        static void Primary_Get_Postfix(ref StartDayWarning __result)
        {
            if (__result != StartDayWarning.Ready && __result != StartDayWarning.PlayersNotReady)
                return;

            if (ApplianceParts == default)
            {
                if (!PatchController.StaticTryGetEntityQuery(new QueryHelper().All(typeof(CAppliancePart)), out ApplianceParts))
                    return;
            }

            AppliancePartsPresent = WarningLevel.Error.If(!ApplianceParts.IsEmpty);
            if (AppliancePartsPresent.IsActive())
            {
                __result = Main.AppliancePartWarning;
            }
        }
    }
}
