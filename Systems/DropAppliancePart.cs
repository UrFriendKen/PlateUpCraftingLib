using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class DropAppliancePart : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;

        Entity AppliancePartEntity;
        CItemHolder TargetItemHolder;
        CItemHolder InteractorItemHolder;
        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out InteractorItemHolder) || InteractorItemHolder.HeldItem == default || !Has<CAppliancePart>(InteractorItemHolder.HeldItem))
                return false;
            AppliancePartEntity = InteractorItemHolder.HeldItem;
            if (!Require(data.Target, out TargetItemHolder) || TargetItemHolder.HeldItem != default)
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            InteractorItemHolder.HeldItem = default;
            data.Context.Set(data.Interactor, InteractorItemHolder);
            data.Context.Set(AppliancePartEntity, new CHeldBy()
            {
                Holder = data.Target
            });
            TargetItemHolder.HeldItem = AppliancePartEntity;
            data.Context.Set(data.Target, TargetItemHolder);
        }
    }
}
