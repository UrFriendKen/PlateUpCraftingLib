using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLib.Systems
{
    [UpdateAfter(typeof(PickUpAndDropAppliance))]
    public class AddPartToCraftStation : ApplianceInteractionSystem, IModSystem
    {
        private CAppliancePartCraftStation CraftStation;
        private CAppliancePart Part;
        private Entity PartEntity;
        private bool IsReturn;

        protected override InteractionType RequiredType => InteractionType.Grab;

        protected override bool IsPossible(ref InteractionData data)
        {
            if (!Require(data.Target, out CraftStation))
                return false;
            if (Require(data.Target, out CSpecialAppliancePartHolder specialHolder) && specialHolder.SpecialAdd)
                return false;
            if (!Require(data.Target, out CAppliance appliance))
                return false;
            int applianceID = appliance.ID;
            if (!Require(data.Interactor, out CItemHolder holder) || holder.HeldItem == default)
                return false;
            if (!Require(holder.HeldItem, out Part))
                return false;
            if (!GameData.Main.TryGet(Part.ID, out AppliancePart partGDO, warn_if_fail: true))
                return false;
            if (!CraftStation.AllowAddAnyPart && !CraftStation.UsesPart(applianceID, Part.ID))
                return false;
            IsReturn = data.Target == Part.Source;
            if (!IsReturn && data.Context.RequireBuffer(data.Target, out DynamicBuffer<CUsedPart> buffer) && 
                buffer.Length >= CraftStation.SlotCount)
                return false;
            PartEntity = holder.HeldItem;
            return true;
        }

        protected override void Perform(ref InteractionData data)
        {
            CraftStation.AddPart(data.Context, data.Target, Part);
            Part.Consume(data.Context, IsReturn);
            data.Context.Destroy(PartEntity);
            data.Context.Set(data.Interactor, default(CItemHolder));
        }
    }
}
