using CraftingLib.Utils;
using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(PickUpAndDropAppliance))]
    public class RetrievePart : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePartStore Store;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem != default)
                return false;
            if (!Require(data.Target, out Store) || !Store.IsInUse)
                return false;
            if (Require(data.Target, out CSpecialAppliancePartStore specialStore) && specialStore.SpecialRetrieve)
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            AppliancePartHelpers.CreateAppliancePart(data.Context, data.Target, CAppliancePartSource.SourceType.Store, data.Interactor, out _);
        }
    }
}
