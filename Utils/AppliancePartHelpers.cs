using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using Unity.Entities;

namespace CraftingLib.Utils
{
    public static class AppliancePartHelpers
    {
        public static bool CreateAppliancePart<T>() // Some custom appliance source type
        {
            return false;
        }

        /// <summary>
        /// Create Appliance Part from Appliance Part Store.
        /// </summary>
        /// <param name="ctx">Create new EntityContext, or use existing data.Context if performing in InteractionSystem.</param>
        /// <param name="source">Entity with CAppliancePartSource component.</param>
        /// <param name="sourceType">SourceType to be part is to be created from.</param>
        /// <param name="holder">Entity with CItemHolder component, typically the player entity.</param>
        /// <param name="part">Created Part entity. Default, if part was not created</param>
        /// <param name="preventCleanup">Can be set to false if creating during the day. Otherwise, must be set to true.</param>
        /// <returns>True if Part entity was created. Otherwise, false.</returns>
        public static bool CreateAppliancePart(EntityContext ctx, Entity source, CAppliancePartSource.SourceType sourceType, Entity holder, out Entity part, bool preventCleanup = true)
        {
            part = default;
            if (!ctx.Require(holder, out CItemHolder cItemHolder) || cItemHolder.HeldItem != default)
            {
                Main.LogError("\"holder\" does not have CItemHolder component or item holder is occupied");
                return false;
            }

            if (!ctx.Require(source, out CAppliancePartSource sourceComp))
            {
                Main.LogError("\"source\" does not have CAppliancePartSource component");
                return false;
            }
            if (!sourceComp.Type.HasFlag(sourceType))
            {
                Main.LogError("CAppliancePartSource.Type does not match given \"sourceType\"");
                return false;
            }

            int partID = 0;
            switch (sourceType)
            {
                case CAppliancePartSource.SourceType.Store:
                    if (!ctx.Require(source, out CAppliancePartStore store))
                    {
                        Main.LogError("\"source\" does not have CAppliancePartStore component");
                        return false;
                    }
                    if (!store.IsInUse)
                        return false;
                    partID = store.PartID;
                    store.HeldCount++;
                    ctx.Set(source, store);
                    break;
                case CAppliancePartSource.SourceType.Vendor:
                    if (!ctx.Require(source, out CAppliancePartVendor vendor))
                    {
                        Main.LogError("\"source\" does not have CAppliancePartVendor component");
                        return false;
                    }
                    if (!ctx.RequireBuffer(source, out DynamicBuffer<CVendorOption> optionsBuffer))
                    {
                        Main.LogError("\"source\" does not have CVendorOption buffer");
                        return false;
                    }
                    if (vendor.SelectedIndex < 0 || vendor.SelectedIndex > optionsBuffer.Length - 1)
                    {
                        Main.LogError("CAppliancePartVendor.SelectedIndex out of range!");
                        return false;
                    }
                    partID = optionsBuffer[vendor.SelectedIndex].ID;
                    break;
                case CAppliancePartSource.SourceType.PartialAppliance:
                case CAppliancePartSource.SourceType.AttachableAppliance:
                    if (!ctx.RequireBuffer(source, out DynamicBuffer<CConsumedPart> partsBuffer))
                    {
                        return false;
                    }

                    for (int i = partsBuffer.Length - 1; i > -1; i--)
                    {
                        CConsumedPart consumedPart = partsBuffer[i];
                        if (consumedPart.IsPartHeld)
                            continue;
                        if (!GameData.Main.TryGet(consumedPart.ID, out AppliancePart partGDO, warn_if_fail: true))
                            continue;
                        if (sourceType == CAppliancePartSource.SourceType.PartialAppliance ? !partGDO.IsWithdrawable : !partGDO.IsDetachable)
                            continue;
                        consumedPart.IsPartHeld = true;
                        partsBuffer[i] = consumedPart;
                        partID = consumedPart.ID;
                        break;
                    }
                    break;
                case CAppliancePartSource.SourceType.Custom:
                    partID = 0;// Call Func in yet to be declared registry. Should return a part id
                    break;
                default:
                    break;
            }

            if (partID == 0)
                return false;

            Entity entity = ctx.CreateEntity();
            if (preventCleanup)
            {
                ctx.Set(entity, default(CPreservedOvernight)); // To prevent cleanup by DestroyItemsOvernight
            }
            ctx.Set(entity, default(CDoNotPersist)); // Prevent serializing entity to save file
            ctx.Set(entity, new CAppliancePart()
            {
                ID = partID,
                Source = source,
                SourceType = sourceType,
                CustomSourceTypeID = sourceComp.CustomSourceTypeID
            });
            ctx.Set(entity, new CAddAppliancePartProperties());


            ctx.Set(entity, default(CHeldAppliance));
            ctx.Set(entity, new CRequiresView
            {
                Type = Main.AppliancePartViewType
            });
            ctx.Set(entity, new CHeldBy
            {
                Holder = holder
            });
            ctx.Set(holder, new CItemHolder()
            {
                HeldItem = entity
            });

            part = entity;
            return true;
        }
    }
}
