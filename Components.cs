using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using static Kitchen.CApplianceInfo;

namespace CraftingLib
{
    public interface IAppliancePartProperty : IAttachableProperty, IComponentData { }
    public struct CAppliancePart : IComponentData, IModComponent
    {
        public int ID;
        public Entity Source;
        public CAppliancePartSource.SourceType SourceType;
        public FixedString128 CustomSourceTypeID;

        public void Consume(EntityContext ctx, bool returnPart = false)
        {
            bool success = true;
            if (!ctx.Require(Source, out CAppliancePartSource source))
            {
                success = false;
                Main.LogError("CAppliancePart.Consume unable to find CAppliancePartSource component in \"Source\"");
            }

            if (success && !source.Type.HasFlag(SourceType))
            {
                success = false;
                Main.LogError($"Source does not have {SourceType} flag set");
            }
                
            if (success)
            {
                switch (SourceType)
                {
                    case CAppliancePartSource.SourceType.Store:
                        if (!ctx.Require(Source, out CAppliancePartStore sourcePartStore))
                        {
                            Main.LogError("\"source\" does not have CAppliancePartStore component");
                            break;
                        }

                        sourcePartStore.HeldCount--;
                        if (sourcePartStore.HeldCount < 0)
                            sourcePartStore.HeldCount = 0;
                        if (!returnPart && !sourcePartStore.IsInfinite)
                        {
                            sourcePartStore.Remaining--;
                            if (sourcePartStore.Remaining < 0)
                                sourcePartStore.Remaining = 0;
                        }
                        ctx.Set(Source, sourcePartStore);
                        break;
                    case CAppliancePartSource.SourceType.PartialAppliance:
                    case CAppliancePartSource.SourceType.AttachableAppliance:
                    case CAppliancePartSource.SourceType.CraftStation:
                        if (!ctx.RequireBuffer(Source, out DynamicBuffer<CUsedPart> partsBuffer))
                        {
                            break;
                        }

                        for (int i = partsBuffer.Length - 1; i > -1; i--)
                        {
                            CUsedPart usedPart = partsBuffer[i];
                            if (usedPart.ID == ID && usedPart.IsPartHeld)
                            {
                                partsBuffer.RemoveAt(i);
                                break;
                            }
                        }

                        if (returnPart)
                        {
                            ctx.AppendToBuffer(Source, new CUsedPart()
                            {
                                ID = ID,
                                IsPartHeld = false
                            });
                        }

                        break;
                    case CAppliancePartSource.SourceType.Custom:
                        // Invoke handler from yet to be declared registry
                        // Handler should be an Action<CAppliancePart, EntityContext, bool>?
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public struct CShowAppliancePartInfo : IComponentData, IModComponent
    {
        public int ApplianceID;
        public int PartID;
        public int Price;
        public bool ShowPrice;
    }

    public struct CAppliancePartInfo : IComponentData
    {
        public int ID;
        public ApplianceInfoMode Mode;
        public int Price;
    }

    public struct CAppliancePartCraftStation : IApplianceProperty, IComponentData, IAttachableProperty, IAttachmentLogic, IModComponent
    {
        public int RecipeGroupID;
        public int SlotCount;
        public bool AllowAddAnyPart;
        public bool MultipleCraftsInDay;

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, this);
            if (!em.RequireComponent(e, out CAppliancePartSource source))
            {
                ecb.AddComponent(e, new CAppliancePartSource()
                {
                    Type = CAppliancePartSource.SourceType.CraftStation
                });
            }
            else
            {
                source.Type |= CAppliancePartSource.SourceType.CraftStation;
                em.SetComponentData(e, source);
            }
        }

        public bool HasResult(EntityContext ctx, Entity e, out int resultID)
        {
            return HasResult(ctx, e, out resultID, out _);
        }

        public bool HasResult(EntityContext ctx, Entity e, out AppliancePart result)
        {
            return HasResult(ctx, e, out _, out result);
        }

        public bool HasResult(EntityContext ctx, Entity e, out int resultID, out AppliancePart result)
        {
            resultID = 0;
            result = null;
            if (!GameData.Main.TryGet(RecipeGroupID, out AppliancePartRecipeGroup recipeGroup, warn_if_fail: true))
                return false;
            if (!ctx.RequireBuffer(e, out DynamicBuffer<CUsedPart> buffer))
                buffer = ctx.AddBuffer<CUsedPart>(e);

            List<int> usedParts = new List<int>();
            foreach (CUsedPart usedPart in buffer)
            {
                if (usedPart.IsPartHeld)
                    return false;
                usedParts.Add(usedPart.ID);
            }

            int applianceID = 0;
            if (ctx.Require(e, out CAppliance appliance))
                applianceID = appliance.ID;

            if (!recipeGroup.IsMatch(applianceID, usedParts, out result))
                return false;
            resultID = result.ID;
            return true;
        }

        public bool UsesPart(int applianceID, int partID)
        {
            if (!GameData.Main.TryGet(RecipeGroupID, out AppliancePartRecipeGroup recipeGroup, warn_if_fail: true))
                return false;
            return recipeGroup.UsesPart(applianceID, partID);
        }

        public bool AddPart(EntityContext ctx, Entity applianceEntity, CAppliancePart part)
        {
            if (part.Source == applianceEntity)
                return false;

            if (!ctx.RequireBuffer(applianceEntity, out DynamicBuffer<CUsedPart> buffer))
                buffer = ctx.AddBuffer<CUsedPart>(applianceEntity);
            if (buffer.Length >= SlotCount)
                return false;

            ctx.AppendToBuffer(applianceEntity, new CUsedPart()
            {
                ID = part.ID
            });
            return true;
        }
    }
    public struct CCraftPerformed : ICommandData, IModComponent { }

    public struct CRecipeSatisfied : IComponentData, IModComponent
    {
        public int Result;
    }

    public struct CAddAppliancePartProperties : IComponentData, IModComponent { }

    /// <summary>
    /// Attach this to your Appliance Part to prevent it from being acted on by CraftingLib.StorePart system. Make your own InteractionSystem instead.
    /// </summary>
    public struct CNonStorablePart : IAppliancePartProperty, IComponentData, IModComponent { }
    /// <summary>
    /// Attach this to your Appliance Part to prevent it from being acted on by CraftingLib.DepositPart system. Make your own InteractionSystem instead.
    /// </summary>
    public struct CNonDepositablePart : IAppliancePartProperty, IComponentData, IModComponent { }
    /// <summary>
    /// Attaching this to your Appliance Part allows it to be acted on by CraftingLib.AttachPart system. Otherwise, make your own InteractionSystem instead.
    /// </summary>
    public struct CAttachablePart : IAppliancePartProperty, IComponentData, IModComponent { }

    public struct CNonDisposablePart : IAppliancePartProperty, IComponentData, IModComponent { }

    public struct CAppliancePartSource : IComponentData, IModComponent
    {
        public enum SourceType
        {
            Custom = 0,
            Store = 1,
            PartialAppliance = 2,
            AttachableAppliance = 4,
            CraftStation = 8,
            Vendor = 16
        }

        public SourceType Type;
        public FixedString128 CustomSourceTypeID;
    }

    public struct CAppliancePartStore : IApplianceProperty, IComponentData, IAttachableProperty, IAttachmentLogic, IModComponent
    {
        public int PartID;
        public int Total;
        public int Remaining;
        public int HeldCount;

        public bool IsInUse => (IsInfinite || Remaining > 0) && PartID != 0;
        public bool IsFull => !IsInfinite && Remaining == Total;
        public bool IsInfinite => Total < 1;

        public int GetNetRemaining()
        {
            return Remaining - HeldCount;
        }

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, this);
            if (!em.RequireComponent(e, out CAppliancePartSource source))
            {
                ecb.AddComponent(e, new CAppliancePartSource()
                {
                    Type = CAppliancePartSource.SourceType.Store
                });
            }
            else
            {
                source.Type |= CAppliancePartSource.SourceType.Store;
                em.SetComponentData(e, source);
            }
        }
    }
    public struct CDynamicAppliancePartStore : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent { }
    public struct CDestroyWhenDepleted : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent { }

    /// <summary>
    /// Attach this to your Appliance Part Stores to prevent them from being acted on by store/retrieve part systems. Make your own InteractionSystem instead.
    /// </summary>
    public struct CSpecialAppliancePartStore : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent
    {
        /// <summary>
        /// Prevent acting by CraftingLib.RetrievePart
        /// </summary>
        public bool SpecialRetrieve;
        /// <summary>
        /// Prevent acting by CraftingLib.StorePart
        /// </summary>
        public bool SpecialStore;
    }


    public struct CAppliancePartVendor : IApplianceProperty, IComponentData, IAttachableProperty, IAttachmentLogic, IModComponent
    {
        public enum VendorType
        {
            Crate,
            Part
        }

        public VendorType Type;
        public int SelectedIndex;

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, this);
            if (!em.RequireComponent(e, out CAppliancePartSource source))
            {
                ecb.AddComponent(e, new CAppliancePartSource()
                {
                    Type = CAppliancePartSource.SourceType.Vendor
                });
            }
            else
            {
                source.Type |= CAppliancePartSource.SourceType.Vendor;
                em.SetComponentData(e, source);
            }
        }
    }
    public struct CVendorLocked : IComponentData, IModComponent
    {
        public enum LockReason
        {
            InvalidID,
            NotEnoughMoney
        }

        public LockReason Reason;
    }
    public struct CVendorOption : IBufferElementData
    {
        public int ID;
        public int PurchaseCost;
    }


    public struct CPartialAppliance : IApplianceProperty, IComponentData, IAttachableProperty, IAttachmentLogic, IModComponent
    {
        public int ID;

        public CPartialAppliance()
        {
            ID = 0;
        }

        public CPartialAppliance(int id)
        {
            ID = id;
        }

        public bool NeedsPart(EntityContext ctx, Entity partialApplianceEntity, int partID)
        {
            if (!GameData.Main.TryGet(ID, out PartialAppliance gdo, warn_if_fail: true))
                return false;

            if (!ctx.RequireBuffer(partialApplianceEntity, out DynamicBuffer<CUsedPart> buffer))
            {
                buffer = ctx.AddBuffer<CUsedPart>(partialApplianceEntity);
            }

            int matchingUsedPartCount = 0;
            foreach (CUsedPart usedPart in buffer)
            {
                if (usedPart.ID == partID)
                    matchingUsedPartCount++;
            }
            
            foreach (ApplianceRecipe recipe in gdo.Recipes)
            {
                if (recipe.PartCount(partID) > matchingUsedPartCount)
                    return true;
            }
            return false;
        }

        public bool DepositPart(EntityContext ctx, Entity partialApplianceEntity, CAppliancePart part)
        {
            if (part.Source == partialApplianceEntity)
                return false;

            if (!NeedsPart(ctx, partialApplianceEntity, part.ID))
                return false;

            ctx.AppendToBuffer(partialApplianceEntity, new CUsedPart()
            {
                ID = part.ID
            });
            return true;
        }

        public bool IsComplete(EntityContext ctx, Entity e, out int index)
        {
            return PrivateIsComplete(ctx, e, out index, out _);
        }

        public bool IsComplete(EntityContext ctx, Entity e, out Appliance result)
        {
            return PrivateIsComplete(ctx, e, out _, out result);
        }

        private bool PrivateIsComplete(EntityContext ctx, Entity e, out int index, out Appliance appliance)
        {
            index = -1;
            appliance = null;
            if (!GameData.Main.TryGet(ID, out PartialAppliance gdo, warn_if_fail: true))
                return false;

            if (!ctx.RequireBuffer(e, out DynamicBuffer<CUsedPart> buffer))
            {
                buffer = ctx.AddBuffer<CUsedPart>(e);
            }

            List<int> usedParts = new List<int>();
            foreach (CUsedPart usedPart in buffer)
            {
                if (usedPart.IsPartHeld)
                    return false;
                usedParts.Add(usedPart.ID);
            }

            int i = 0;
            foreach (ApplianceRecipe recipe in gdo.Recipes)
            {
                if (recipe.IsMatch(usedParts) && recipe.Result != null)
                {
                    index = i;
                    appliance = recipe.Result;
                    return true;
                }
                i++;
            }
            return false;
        }

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, this);
            if (!em.RequireComponent(e, out CAppliancePartSource source))
            {
                ecb.AddComponent(e, new CAppliancePartSource()
                {
                    Type = CAppliancePartSource.SourceType.PartialAppliance
                });
            }
            else
            {
                source.Type |= CAppliancePartSource.SourceType.PartialAppliance;
                em.SetComponentData(e, source);
            }
        }
    }

    /// <summary>
    /// Attach this to your Partial Appliance to prevent them from being acted on by deposit part systems. Make your own InteractionSystem instead.
    /// </summary>
    public struct CSpecialAppliancePartHolder : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
    {
        /// <summary>
        /// Prevent acting by CraftingLib.DepositPart
        /// </summary>
        public bool SpecialDeposit;
        /// <summary>
        /// Prevent acting by CraftingLib.WithdrawPart
        /// </summary>
        public bool SpecialWithdraw;
        /// <summary>
        /// Prevent acting by CraftingLib.AttachPart
        /// </summary>
        public bool SpecialAttach;
        /// <summary>
        /// Prevent acting by CraftingLib.DetachPart
        /// </summary>
        public bool SpecialDetach;
        /// <summary>
        /// Prevent acting by CraftingLib.AddPartToCraftStation
        /// </summary>
        public bool SpecialAdd;
        /// <summary>
        /// Prevent acting by CraftingLib.RemovePartToCraftStation
        /// </summary>
        public bool SpecialRemove;
    }

    [InternalBufferCapacity(5)]
    public struct CUsedPart : IBufferElementData
    {
        public int ID;
        public bool IsPartHeld;
    }

    /// <summary>
    /// Mark entities with CPartialAppliance that have at least one recipe satisfied
    /// </summary>
    public struct CIsCompletable : IComponentData, IModComponent
    {
        /// <summary>
        /// ApplianceID of result from first recipe with the most parts.
        /// </summary>
        public int ID;
        public OccupancyLayer Layer;
    }

    public struct CLockDurationNight : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent { }
    public struct CLockDurationDay : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent { }
    public struct CAppliancePartContainerInfo : IComponentData, IModComponent
    {
        public int ID;
        public ApplianceInfoMode Mode;
        public int Price;
        public int ResultID;
        public FixedListInt128 PartIDs;
        public FixedListInt128 PartCount;
    }

    public struct CShowApplianceContainerInfo : IComponentData, IModComponent
    {
        public int ID;
        public int Price;
        public bool ShowPrice;
        public int ResultID;
        public FixedListInt128 PartIDs;
        public FixedListInt128 PartCount;
    }



    public struct CPartAttachmentPoint : IApplianceProperty, IComponentData, IAttachableProperty, IAttachmentLogic, IModComponent
    {
        public int MaxAttachmentCount;

        public bool CanAttachPart(EntityContext ctx, Entity applianceEntity, int applianceID, int partID)
        {
            if (!GameData.Main.TryGet(partID, out AppliancePart partGDO, warn_if_fail: true))
                return false;
            if (!partGDO.IsAttachableTo(applianceID))
                return false;
            if (ctx.RequireBuffer(applianceEntity, out DynamicBuffer<CUsedPart> buffer) && buffer.Length >= MaxAttachmentCount)
                return false;
            return true;
        }

        public bool AttachPart(EntityContext ctx, Entity applianceEntity, CAppliancePart part)
        {
            if (part.Source == applianceEntity)
                return false;

            if (!ctx.RequireBuffer(applianceEntity, out DynamicBuffer<CUsedPart> buffer))
                buffer = ctx.AddBuffer<CUsedPart>(applianceEntity);
            if (buffer.Length >= MaxAttachmentCount)
                return false;

            ctx.AppendToBuffer(applianceEntity, new CUsedPart()
            {
                ID = part.ID
            });
            return true;
        }

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, this);
            if (!em.RequireComponent(e, out CAppliancePartSource source))
            {
                ecb.AddComponent(e, new CAppliancePartSource()
                {
                    Type = CAppliancePartSource.SourceType.AttachableAppliance
                });
            }
            else
            {
                source.Type |= CAppliancePartSource.SourceType.AttachableAppliance;
                em.SetComponentData(e, source);
            }
        }
    }
}
