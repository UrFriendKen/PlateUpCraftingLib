using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using Unity.Entities;

namespace CraftingLib.Utils
{
    public static class AppliancePartHelpers
    {
        /// <summary>
        /// Create Appliance Part from Appliance Part Store.
        /// </summary>
        /// <param name="ctx">Create new EntityContext, or use existing data.Context if performing in InteractionSystem.</param>
        /// <param name="source">Entity with CAppliancePartStore component.</param>
        /// <param name="holder">Entity with CItemHolder component, typically the player entity.</param>
        /// <param name="part">Created Part entity. Default, if part was not created</param>
        /// <param name="preventCleanup">Can be set to false if creating during the day. Otherwise, must be set to true.</param>
        /// <returns>True if Part entity was created. Otherwise, false.</returns>
        public static bool CreateAppliancePart(EntityContext ctx, Entity source, Entity holder, out Entity part, bool preventCleanup = true)
        {
            part = default;
            if (!ctx.Require(source, out CAppliancePartStore store))
            {
                Main.LogError("\"source\" does not have CAppliancePartStore component");
                return false;
            }
            if (!store.IsInUse)
            {
                return false;
            }

            if (!GameData.Main.TryGet(store.PartID, out AppliancePart gdo, warn_if_fail: true))
            {
                return false;
            }

            Entity entity = ctx.CreateEntity();
            if (preventCleanup)
            {
                ctx.Set(entity, default(CPreservedOvernight)); // To prevent cleanup by DestroyItemsOvernight
            }
            ctx.Set(entity, default(CDoNotPersist)); // Prevent serializing entity to save file
            ctx.Set(entity, new CAppliancePart()
            {
                ID = store.PartID,
                Source = source
            });
            ctx.Set(entity, new CAttachAppliancePartProperties());

            if (ctx.Require(holder, out CItemHolder cItemHolder) && cItemHolder.HeldItem == default)
            {
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
            }

            part = entity;
            return true;
        }
    }
}
