using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateAfter(typeof(PickUpAndDropAppliance))]
    public class StorePart : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePart Part;
        private CAppliancePartStore PartStore;
        private CItemHolder Holder;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out Holder) || Holder.HeldItem == default)
                return false;
            if (!Require(Holder.HeldItem, out Part))
                return false;
            if (!Require(data.Target, out PartStore))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartStore specialStore) && specialStore.SpecialStore)
                return false;
            if (PartStore.IsFull)
                return false;
            if (PartStore.IsInUse && PartStore.PartID != Part.ID)
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            if (Part.Source != default)
            {
                if (data.Target == Part.Source)
                {
                    data.Context.Destroy(Holder.HeldItem);
                    data.Context.Set(data.Interactor, default(CItemHolder));
                    return;
                }

                if (data.Context.Require(Part.Source, out CAppliancePartStore sourcePartStore)
                    && !sourcePartStore.IsInfinite && sourcePartStore.Remaining > 0)
                {
                    sourcePartStore.Remaining--;
                    data.Context.Set(Part.Source, sourcePartStore);
                }
            }

            PartStore.PartID = Part.ID;
            if (!PartStore.IsInfinite)
                PartStore.Remaining++;
            data.Context.Set(data.Target, PartStore);

            data.Context.Destroy(Holder.HeldItem);
            data.Context.Set(data.Interactor, default(CItemHolder));
        }
    }
}
