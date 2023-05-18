using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateAfter(typeof(PickUpAndDropAppliance))]
    public class AttachPart : ApplianceInteractionSystem, IModSystem
    {
        private List<IComponentData> ComponentsToAdd;
        private CPartAttachmentPoint PartAttachmentPoint;
        private CAppliancePart Part;
        private Entity PartEntity;
        private bool IsReturn;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out PartAttachmentPoint))
                return false;
            if (Require(data.Target, out CSpecialPartialAppliance specialPartial) && specialPartial.SpecialAttach)
                return false;
            if (!Require(data.Target, out CAppliance appliance))
                return false;
            int applianceID = appliance.ID;
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem == default)
                return false;
            if (!Require(holder.HeldItem, out Part))
                return false;
            if (!GameData.Main.TryGet(Part.ID, out AppliancePart partGDO, warn_if_fail: true))
                return false;
            if (!partGDO.IsAttachableTo(applianceID))
                return false;
            IsReturn = data.Target == Part.Source;
            if (!IsReturn && data.Context.RequireBuffer(data.Target, out DynamicBuffer<CConsumedPart> buffer) && 
                buffer.Length >= PartAttachmentPoint.MaxAttachmentCount)
                return false;
            ComponentsToAdd = partGDO.ComponentsAddWhenAttached;
            PartEntity = holder.HeldItem;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            PartAttachmentPoint.AttachPart(data.Context, data.Target, Part);
            Part.Consume(data.Context, IsReturn);
            data.Context.Destroy(PartEntity);
            data.Context.Set(data.Interactor, default(CItemHolder));

            foreach (IComponentData comp in ComponentsToAdd)
            {
                Main.LogInfo(comp);
                data.Context.Set(data.Target, (dynamic)comp);
            }
        }
    }
}
