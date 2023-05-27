using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class ShowPingedAppliancePartHolderInfo : InteractionSystem, IModSystem
    {
        CAppliance Appliance;
        AppliancePart Part;
        protected override InteractionType RequiredType => InteractionType.Notify;
        protected override bool AllowAnyMode => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out CItemHolder holder) || holder.HeldItem == default)
                return false;
            if (!Require(data.Target, out Appliance))
                return false;
            if (Has<CShowAppliancePartInfo>(data.Target))
                return false;
            if (!Require(holder.HeldItem, out CAppliancePart cAppliancePart))
                return false;
            if (!GameData.Main.TryGet(cAppliancePart.ID, out Part) || Part.Name == "")
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            data.Context.Set(data.Target, new CTemporaryApplianceInfo
            {
                RemainingLifetime = 0.2f
            });
            data.Context.Set(data.Target, new CShowAppliancePartInfo
            {
                ApplianceID = Appliance.ID,
                PartID = Part.ID,
                ShowPrice = false,
                Price = Part.PurchaseCost
            });
        }
    }
}
