using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(PickUpAndDropAppliance))]
    public class RetrievePart : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePartStore PartStore;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem != default)
                return false;
            if (!Require(data.Target, out PartStore) || !PartStore.IsInUse)
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            Entity entity = EntityManager.CreateEntity();

            data.Context.Set(entity, new CRequiresView
            {
                Type = ViewType.HeldAppliance
            });
            data.Context.Set(entity, new CHeldBy
            {
                Holder = data.Interactor
            });
            data.Context.Set(entity, default(CHeldAppliance));
            data.Context.Set(entity, default(CPreservedOvernight)); // To prevent cleanup by DestroyItemsOvernight
            data.Context.Set(entity, default(CDoNotPersist)); // Prevent serializing entity to save file

            data.Context.Set(entity, new CAppliancePart()
            {
                ID = PartStore.PartID,
                Source = data.Target
            });

            data.Context.Set(data.Interactor, new CItemHolder()
            {
                HeldItem = entity
            });
        }
    }
}
