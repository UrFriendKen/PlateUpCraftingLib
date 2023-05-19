using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateAfter(typeof(PickUpAndDropAppliance))]
    public class DepositPart : ApplianceInteractionSystem, IModSystem
    {
        private CPartialAppliance PartialAppliance;
        private CAppliancePart Part;
        private Entity PartEntity;
        private bool IsReturn;

        protected override bool AllowActOrGrab => true;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out PartialAppliance))
                return false;
            if (Require(data.Target, out CSpecialPartialAppliance specialPartial) && specialPartial.SpecialDeposit)
                return false;
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem == default)
                return false;
            if (!Require(holder.HeldItem, out Part))
                return false;
            if (Has<CNonDepositablePart>(holder.HeldItem))
                return false;
            IsReturn = Part.Source == data.Target;
            if (!IsReturn && !PartialAppliance.NeedsPart(data.Context, data.Target, Part.ID))
                return false;
            PartEntity = holder.HeldItem;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            PartialAppliance.DepositPart(data.Context, data.Target, Part);
            Part.Consume(data.Context, IsReturn);
            data.Context.Destroy(PartEntity);
            data.Context.Set(data.Interactor, default(CItemHolder));
        }
    }
}
