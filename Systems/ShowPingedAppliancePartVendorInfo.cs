using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateBefore(typeof(ShowPingedApplianceInfo))]
    public class ShowPingedAppliancePartVendorInfo : InteractionSystem, IModSystem
    {
        CAppliance Appliance;
        AppliancePart Part;
        protected override InteractionType RequiredType => InteractionType.Notify;
        protected override bool AllowAnyMode => true;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out CAppliancePartVendor vendor))
                return false;
            if (!Require(data.Target, out Appliance))
                return false;
            if (Has<CShowAppliancePartVendorInfo>(data.Target))
                return false;
            if (!RequireBuffer(data.Target, out DynamicBuffer<CVendorOption> optionsBuffer)) 
                return false;
            if (vendor.SelectedIndex < 0 || vendor.SelectedIndex > optionsBuffer.Length - 1)
                return false;
            CVendorOption vendorOption = optionsBuffer[vendor.SelectedIndex];
            if (!GameData.Main.TryGet(vendorOption.ID, out Part) || Part.Name == "")
                return false;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            data.Context.Set(data.Target, new CTemporaryApplianceInfo
            {
                RemainingLifetime = 0.2f
            });
            data.Context.Set(data.Target, new CShowAppliancePartVendorInfo
            {
                ApplianceID = Appliance.ID,
                PartID = Part.ID,
                ShowPrice = true,
                Price = Part.PurchaseCost
            });
        }
    }
}
