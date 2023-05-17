using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(MakePing))]
    public class ShowPingedPartialApplianceInfo : ApplianceInteractionSystem, IModSystem
    {
        private CPartialAppliance PartialAppliance;

        protected override InteractionType RequiredType => InteractionType.Notify;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out PartialAppliance))
            {
                return false;
            }
            if (Has<CShowPartialApplianceInfo>(data.Target))
            {
                return false;
            }
            if (!GameData.Main.TryGet<PartialAppliance>(PartialAppliance.ID, out var output) || output.Name == "")
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
            PartialAppliance.IsComplete(data.Context, data.Target, out int recipeIndex);
            // To Do: Find another use for recipe index instead of completed index since ProgressView blocks PartialApplianceInfoView.
            data.Context.Set(data.Target, new CShowPartialApplianceInfo
            {
                ID = PartialAppliance.ID,
                ShowPrice = showPrice,
                Price = sale.Price,
                RecipeIndex = recipeIndex
            });
        }
    }
}
