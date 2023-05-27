using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class ShowPingedAppliancePartStoreInfo : InteractionSystem, IModSystem
    {
        private CAppliancePartStore Store;
        private CAppliance Appliance;

        protected override InteractionType RequiredType => InteractionType.Notify;
        protected override bool AllowAnyMode => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out Store))
            {
                return false;
            }
            if (!Require(data.Target, out Appliance))
            {
                return false;
            }
            if (Has<CShowApplianceContainerInfo>(data.Target))
            {
                return false;
            }
            if (!GameData.Main.TryGet<Appliance>(Appliance.ID, out var output) || output.Name == "")
            {
                return false;
            }
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            data.Context.Set(data.Target, new CTemporaryApplianceInfo
            {
                RemainingLifetime = 0.2f
            });
            bool showPrice = Require(data.Target, out CForSale sale) && sale.Price > 0;

            FixedListInt128 partIDs = new FixedListInt128();
            FixedListInt128 partCount = new FixedListInt128();

            if (Store.IsInUse || !Has<CDynamicAppliancePartStore>(data.Target))
            {
                partIDs.Add(Store.PartID);
                partCount.Add(Store.IsInfinite? -1 : Store.Remaining - Store.HeldCount);
            }

            // To Do: Find another use for recipe index instead of completed index since ProgressView blocks PartialApplianceInfoView.
            data.Context.Set(data.Target, new CShowApplianceContainerInfo
            {
                ID = Appliance.ID,
                ShowPrice = showPrice,
                Price = sale.Price,
                //ResultID = recipeIndex,
                PartIDs = partIDs,
                PartCount = partCount
            });
        }
    }
}
