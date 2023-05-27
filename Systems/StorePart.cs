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
            if (Has<CNonStorablePart>())
                return false;
            if (!Require(data.Target, out PartStore))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartStore specialStore) && specialStore.SpecialStore)
                return false;
            if (PartStore.IsFull)
                return false;
            if (PartStore.PartID != Part.ID && (PartStore.IsInUse || !Has<CDynamicAppliancePartStore>()))
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            bool isReturn = data.Target == Part.Source;
            Part.Consume(data.Context, isReturn);
            if (!isReturn)
            {
                PartStore.PartID = Part.ID;
                if (!PartStore.IsInfinite)
                    PartStore.Remaining++;
                data.Context.Set(data.Target, PartStore);
            }

            data.Context.Destroy(Holder.HeldItem);
            data.Context.Set(data.Interactor, default(CItemHolder));
        }
    }
}
