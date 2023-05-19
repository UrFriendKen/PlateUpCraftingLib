using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using KitchenMods;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class CycleVendor : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionMode RequiredMode => InteractionMode.Appliances;

        protected override InteractionType RequiredType => InteractionType.Grab;

        public CPartsVendor Vendor;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out Vendor))
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            if (PopulateBasicPartsOffers.Offers.IsNullOrEmpty())
            {
                Vendor.PartID = 0;
                Vendor.Cost = 0;
                return;
            }
            else
            {
                int totalCount = PopulateBasicPartsOffers.Offers.Count;
                int index = PopulateBasicPartsOffers.Offers.IndexOf(Vendor.PartID);
                for (int i = 0; i < totalCount; i++)
                {
                    if (!GameData.Main.TryGet(PopulateBasicPartsOffers.Offers[(index + i + 1) % totalCount], out AppliancePart part))
                        continue;
                    Vendor.PartID = part.ID;
                    Vendor.Cost = part.PurchaseCost;
                    break;
                }
            }
            Set(data.Target, Vendor);
        }
    }
}
