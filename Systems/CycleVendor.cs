using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class CycleVendor : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Grab;
        protected override bool AllowAnyMode => true;

        int BufferLength;
        CAppliancePartVendor Vendor;
        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out Vendor))
                return false;
            if (Has<CVendorLocked>(data.Target))
                return false;
            if (!RequireBuffer(data.Target, out DynamicBuffer<CVendorOption> buffer) || buffer.Length < 1)
                return false;
            BufferLength = buffer.Length;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            Vendor.SelectedIndex = ++Vendor.SelectedIndex % BufferLength;
            Set(data.Target, Vendor);
        }
    }
}
