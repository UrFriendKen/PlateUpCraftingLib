using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class DisposePartInOutsideBin : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePart Part;
        private Entity PartEntity;

        protected override bool AllowActOrGrab => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Has<CApplianceExternalBin>( data.Target))
                return false;
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem == default)
                return false;
            if (!Require(holder.HeldItem, out Part))
                return false;
            if (Has<CNonDisposablePart>(holder.HeldItem))
                return false;
            PartEntity = holder.HeldItem;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            Part.Consume(data.Context);
            data.Context.Destroy(PartEntity);
            data.Context.Set(data.Interactor, default(CItemHolder));
        }
    }
}
