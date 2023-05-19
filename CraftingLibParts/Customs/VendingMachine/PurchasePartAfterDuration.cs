﻿using CraftingLib;
using CraftingLib.Utils;
using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class PurchasePartAfterDuration : GenericSystemBase, IModSystem
    {
        EntityQuery Vendors;
        protected override void Initialise()
        {
            base.Initialise();
            Vendors = GetEntityQuery(new QueryHelper()
                .All(typeof(CPartsVendor), typeof(CTakesDuration), typeof(CBeingActedOnBy)));
        }

        protected override void OnUpdate()
        {
            EntityContext ctx = new EntityContext(EntityManager);
            using NativeArray<Entity> entities = Vendors.ToEntityArray(Allocator.Temp);
            using NativeArray<CPartsVendor> vendors = Vendors.ToComponentDataArray<CPartsVendor>(Allocator.Temp);
            using NativeArray<CTakesDuration> durations = Vendors.ToComponentDataArray<CTakesDuration>(Allocator.Temp);

            SMoney player_money = GetOrDefault<SMoney>();

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                CPartsVendor vendor = vendors[i];
                CTakesDuration duration = durations[i];

                if (!duration.Active || duration.Remaining > 0f)
                    continue;
                if (!ctx.RequireBuffer(entity, out DynamicBuffer<CBeingActedOnBy> actors) || actors.IsEmpty)
                    continue;

                for (int j = 0; j < actors.Length; j++)
                {
                    Entity interactor = actors[i].Interactor;
                    if (actors[i].IsTransferOnly || !Require(interactor, out CItemHolder holder) || holder.HeldItem != default || vendor.Cost > player_money)
                        continue;
                    AppliancePartHelpers.CreateAppliancePart(ctx, vendor.PartID, default, CAppliancePartSource.SourceType.None, interactor, out Entity _);
                    player_money.Amount -= vendor.Cost;
                    Set(player_money);
                    break;
                }
            }
        }
    }
}
