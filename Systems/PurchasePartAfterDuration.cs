using CraftingLib.Utils;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLib.Systems
{
    public class PurchasePartAfterDuration : GenericSystemBase, IModSystem
    {
        EntityQuery Vendors;
        protected override void Initialise()
        {
            base.Initialise();
            Vendors = GetEntityQuery(new QueryHelper()
                .All(typeof(CAppliancePartVendor), typeof(CTakesDuration), typeof(CBeingActedOnBy))
                .None(typeof(CVendorLocked)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = Vendors.ToEntityArray(Allocator.Temp);
            using NativeArray<CAppliancePartVendor> vendors = Vendors.ToComponentDataArray<CAppliancePartVendor>(Allocator.Temp);
            using NativeArray<CTakesDuration> durations = Vendors.ToComponentDataArray<CTakesDuration>(Allocator.Temp);

            SMoney player_money = GetOrDefault<SMoney>();

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CAppliancePartVendor vendor = vendors[i];
                CTakesDuration duration = durations[i];
                if (!duration.Active || duration.Remaining > 0f)
                    continue;
                if (!ctx.RequireBuffer(entity, out DynamicBuffer<CBeingActedOnBy> actors) || actors.IsEmpty)
                    continue;
                if (!RequireBuffer(entity, out DynamicBuffer<CVendorOption> optionsBuffer))
                    continue;
                if (vendor.SelectedIndex > -1 && vendor.SelectedIndex > optionsBuffer.Length - 1)
                    continue;
                CVendorOption selectedOption = optionsBuffer[vendor.SelectedIndex];
                int cost = selectedOption.PurchaseCost < 0 ? 0 : selectedOption.PurchaseCost;
                if (cost > player_money)
                    continue;

                for (int j = 0; j < actors.Length; j++)
                {
                    Entity interactor = actors[i].Interactor;
                    if (actors[i].IsTransferOnly || !Require(interactor, out CItemHolder holder) || holder.HeldItem != default)
                        continue;

                    bool success = false;
                    switch (vendor.Type)
                    {
                        case CAppliancePartVendor.VendorType.Part:
                            success = AppliancePartHelpers.CreateAppliancePart(ctx, entity, CAppliancePartSource.SourceType.Vendor, interactor, out Entity _);
                            break;
                        case CAppliancePartVendor.VendorType.Crate:
                            success = AppliancePartHelpers.CreateAppiancePartCrate(ctx, selectedOption.ID);
                            break;
                        default:
                            success = false;
                            break;
                    }

                    if (success)
                    {
                        player_money.Amount -= cost;
                        Set(player_money);
                    }
                    break;
                }
            }
        }
    }
}
