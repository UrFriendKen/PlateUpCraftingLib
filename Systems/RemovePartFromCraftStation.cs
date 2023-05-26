using CraftingLib.Utils;
using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(PickUpAndDropAppliance))]
    public class RemovePartFromCraftStation : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePartCraftStation CraftStation;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem != default)
                return false;
            if (!Require(data.Target, out CraftStation))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartHolder specialHolder) && specialHolder.SpecialRemove)
                return false;
            if (!data.Context.RequireBuffer(data.Target, out DynamicBuffer<CUsedPart> partsBuffer) || partsBuffer.Length <= 0)
                return false;

            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            AppliancePartHelpers.CreateAppliancePart(data.Context, data.Target, CAppliancePartSource.SourceType.CraftStation, data.Interactor, out _);
        }
    }
}
