using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class ShowPingedCraftStationInfo : InteractionSystem, IModSystem
    {
        private CAppliance Appliance;

        protected override InteractionType RequiredType => InteractionType.Notify;
        protected override bool AllowAnyMode => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Has<CAppliancePartCraftStation>(data.Target))
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

            if (data.Context.RequireBuffer(data.Target, out DynamicBuffer<CUsedPart> partBuffer))
            {
                for (int i = 0; i < partBuffer.Length; i++)
                {
                    CUsedPart part = partBuffer[i];
                    int existingIndex = partIDs.IndexOf(part.ID);
                    if (existingIndex != -1)
                    {
                        partCount[existingIndex]++;
                        continue;
                    }
                    partIDs.Add(part.ID);
                    partCount.Add(1);
                }
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
