using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ManagePartialApplianceIndicators : IndicatorManager, IModSystem
    {
        protected override ViewType ViewType => Main.PartialApplianceInfoViewType;

        protected override EntityQuery GetSearchQuery()
        {
            return GetEntityQuery(typeof(CShowPartialApplianceInfo));
        }

        protected override bool ShouldHaveIndicator(Entity candidate)
        {
            if (Has<CHeldAppliance>(candidate))
            {
                return false;
            }
            Entity e = candidate;
            if (Require(candidate, out CHeldBy held))
            {
                e = held.Holder;
            }
            if (!Require(e, out CBeingLookedAt lookedAt))
            {
                return false;
            }
            if (Require(lookedAt.Interactor, out CItemHolder holder) && holder.HeldItem != default(Entity))
            {
                return false;
            }
            if (!Has<CPosition>(e))
            {
                return false;
            }
            return true;
        }

        protected override Entity CreateIndicator(Entity source)
        {
            Entity e = source;
            if (Require(source, out CHeldBy held))
            {
                e = held.Holder;
            }
            if (!Require(e, out CPosition pos))
            {
                return default;
            }
            if (!Require(source, out CShowPartialApplianceInfo showInfo))
            {
                return default;
            }
            Entity entity = base.CreateIndicator(source);
            Set(entity, new CPosition(pos));
            Set(entity, new CPartialApplianceInfo
            {
                ID = showInfo.ID,
                Mode = ((!showInfo.ShowPrice) ? CApplianceInfo.ApplianceInfoMode.Garage : CApplianceInfo.ApplianceInfoMode.Shop),
                Price = showInfo.Price,
                RecipeIndex = showInfo.RecipeIndex
            });
            return entity;
        }
    }
}
