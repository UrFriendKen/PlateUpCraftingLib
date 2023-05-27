using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class PickUpAppliancePart : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;

        Entity AppliancePartEntity;
        CItemHolder TargetItemHolder;
        CItemHolder InteractorItemHolder;
        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out TargetItemHolder) || TargetItemHolder.HeldItem == default || !Has<CAppliancePart>(TargetItemHolder.HeldItem))
                return false;
            AppliancePartEntity = TargetItemHolder.HeldItem;
            if (!Require(data.Interactor, out InteractorItemHolder) || InteractorItemHolder.HeldItem != default)
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            TargetItemHolder.HeldItem = default;
            data.Context.Set(data.Target, TargetItemHolder);
            data.Context.Set(AppliancePartEntity, new CHeldBy()
            {
                Holder = data.Interactor
            });
            InteractorItemHolder.HeldItem = AppliancePartEntity;
            data.Context.Set(data.Interactor, InteractorItemHolder);
        }
    }
}
