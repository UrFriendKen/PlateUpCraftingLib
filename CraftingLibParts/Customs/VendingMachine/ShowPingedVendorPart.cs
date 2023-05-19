using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class ShowPingedVendorPart : InteractionSystem
    {
        private CPartsVendor Vendor;
        protected override InteractionType RequiredType => InteractionType.Notify;
        protected override bool AllowAnyMode => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out Vendor))
            {
                return false;
            }
            if (Has<CShowApplianceInfo>(data.Target))
            {
                return false;
            }
            if (!GameData.Main.TryGet<AppliancePart>(Vendor.PartID, out _))
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
            data.Context.Set(data.Target, new CShowApplianceInfo
            {
                Appliance = Vendor.PartID,
                ShowPrice = true,
                Price = Vendor.Cost
            });
        }
    }
}
