using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class ManageAppliancePartIndicators : IndicatorManager, IModSystem
    {
        private bool ShowAppliance;

        protected override ViewType ViewType => Main.AppliancePartInfoViewType;

        protected override EntityQuery GetSearchQuery()
        {
            return GetEntityQuery(typeof(CShowAppliancePartInfo));
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
            if (!Require(source, out CShowAppliancePartInfo showInfo))
            {
                return default;
            }
            Entity entity = base.CreateIndicator(source);
            Set(entity, new CPosition(pos));
            ShowAppliance = Has<CVendorLocked>(source);
            if (!ShowAppliance)
            {
                Set(entity, new CAppliancePartInfo
                {
                    ID = showInfo.PartID,
                    Mode = ((!showInfo.ShowPrice) ? CApplianceInfo.ApplianceInfoMode.Garage : CApplianceInfo.ApplianceInfoMode.Shop),
                    Price = showInfo.Price
                });
            }
            else
            {
                Set(entity, new CApplianceInfo
                {
                    ID = showInfo.ApplianceID,
                    Mode = CApplianceInfo.ApplianceInfoMode.Garage,
                    Price = showInfo.Price
                });
            }
            
            return entity;
        }
    }
}
