﻿using CraftingLib.GameDataObjects;
using CraftingLib.Utils;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(PickUpAndDropAppliance))]
    public class WithdrawPart : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem != default)
                return false;
            if (!Has<CPartialAppliance>(data.Target))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartHolder specialPartial) && specialPartial.SpecialWithdraw)
                return false;
            if (!data.Context.RequireBuffer(data.Target, out DynamicBuffer<CUsedPart> partsBuffer) || partsBuffer.Length <= 0)
                return false;

            bool hasWithdrawableParts = false;
            for (int i = partsBuffer.Length - 1; i > -1; i--)
            {
                CUsedPart usedPart = partsBuffer[i];
                if (usedPart.IsPartHeld)
                    continue;
                if (!GameData.Main.TryGet(usedPart.ID, out AppliancePart partGDO, warn_if_fail: true))
                    continue;
                if (!partGDO.IsWithdrawable)
                    continue;
                hasWithdrawableParts = true;
                break;
            }
            if (!hasWithdrawableParts)
                return false;

            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            AppliancePartHelpers.CreateAppliancePart(data.Context, data.Target, CAppliancePartSource.SourceType.PartialAppliance, data.Interactor, out _);
        }
    }
}
