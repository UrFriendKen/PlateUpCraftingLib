using CraftingLib.GameDataObjects;
using CraftingLib.Utils;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(PickUpAndDropAppliance))]
    public class DetachPart : ApplianceInteractionSystem, IModSystem
    {
        private List<IComponentData> ComponentsToAdd;
        private CPartAttachmentPoint PartAttachmentPoint;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem != default)
                return false;
            if (!Require(data.Target, out PartAttachmentPoint))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartHolder specialPartial) && specialPartial.SpecialDetach)
                return false;
            if (!data.Context.RequireBuffer(data.Target, out DynamicBuffer<CUsedPart> partsBuffer) || partsBuffer.Length <= 0)
                return false;

            bool hasDetachableParts = false;
            for (int i = partsBuffer.Length - 1; i > -1; i--)
            {
                CUsedPart UsedPart = partsBuffer[i];
                if (UsedPart.IsPartHeld)
                    continue;
                if (!GameData.Main.TryGet(UsedPart.ID, out AppliancePart partGDO, warn_if_fail: true))
                    continue;
                if (!partGDO.IsDetachable)
                    continue;
                hasDetachableParts = true;
                ComponentsToAdd = partGDO.ComponentsAddWhenDetached;
                break;
            }
            if (!hasDetachableParts)
                return false;

            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            if (AppliancePartHelpers.CreateAppliancePart(data.Context, data.Target, CAppliancePartSource.SourceType.AttachableAppliance, data.Interactor, out _))
            {
                foreach (IComponentData comp in ComponentsToAdd)
                {
                    Main.LogInfo(comp);
                    data.Context.Set(data.Target, (dynamic)comp);
                }
            }
        }
    }
}
