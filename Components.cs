using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
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
        public int CustomSourceTypeID;

        public void Consume(EntityContext ctx, bool returnPart = false)
        {
            if (CustomSourceTypeID != default)
            {
                // Invoke handler from yet to be declared registry
                // Handler should be an Action<CAppliancePart, EntityContext, bool> ?
            }

            else
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
                            if (!ctx.RequireBuffer(Source, out DynamicBuffer<CConsumedPart> partsBuffer))
                            {
                                break;
                            }

                            for (int i = partsBuffer.Length - 1; i > -1; i--)
                            {
                                CConsumedPart consumedPart = partsBuffer[i];
                                if (consumedPart.ID == ID && consumedPart.IsPartHeld)
                                {
                                    partsBuffer.RemoveAt(i);
                                    break;
                                }
                            }

                            if (returnPart)
                            {
                                ctx.AppendToBuffer(Source, new CConsumedPart()
                                {
                                    ID = ID,
                                    IsPartHeld = false
                                });
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Return()
        {

        }
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
            None = 0,
            Store = 1,
            PartialAppliance = 2,
            AttachableAppliance = 4
        }

        public SourceType Type;
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

            if (!ctx.RequireBuffer(partialApplianceEntity, out DynamicBuffer<CConsumedPart> buffer))
            {
                buffer = ctx.AddBuffer<CConsumedPart>(partialApplianceEntity);
            }

            int matchingConsumedPartCount = 0;
            foreach (CConsumedPart consumedPart in buffer)
            {
                if (consumedPart.ID == partID)
                    matchingConsumedPartCount++;
            }
            
            foreach (ApplianceRecipe recipe in gdo.Recipes)
            {
                if (recipe.PartCount(partID) > matchingConsumedPartCount)
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

            ctx.AppendToBuffer(partialApplianceEntity, new CConsumedPart()
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

            if (!ctx.RequireBuffer(e, out DynamicBuffer<CConsumedPart> buffer))
            {
                buffer = ctx.AddBuffer<CConsumedPart>(e);
            }

            List<int> consumedParts = new List<int>();
            foreach (CConsumedPart consumedPart in buffer)
            {
                if (consumedPart.IsPartHeld)
                    return false;
                consumedParts.Add(consumedPart.ID);
            }

            int i = 0;
            foreach (ApplianceRecipe recipe in gdo.Recipes.OrderByDescending(recipe => recipe.PartCount()))
            {
                if (recipe.IsMatch(consumedParts))
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
    public struct CSpecialPartialAppliance : IApplianceProperty, IAttachableProperty, IComponentData, IModComponent
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
    }

    [InternalBufferCapacity(5)]
    public struct CConsumedPart : IBufferElementData
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
    public struct CPartialApplianceInfo : IComponentData, IModComponent
    {
        public int ID;
        public ApplianceInfoMode Mode;
        public int Price;
        public int RecipeIndex;
        public FixedListInt128 PartIDs;
        public FixedListInt128 PartCount;
    }

    public struct CShowPartialApplianceInfo : IComponentData, IModComponent
    {
        public int ID;
        public int Price;
        public bool ShowPrice;
        public int RecipeIndex;
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
            if (ctx.RequireBuffer(applianceEntity, out DynamicBuffer<CConsumedPart> buffer) && buffer.Length >= MaxAttachmentCount)
                return false;
            return true;
        }

        public bool AttachPart(EntityContext ctx, Entity applianceEntity, CAppliancePart part)
        {
            if (part.Source == applianceEntity)
                return false;

            if (!ctx.RequireBuffer(applianceEntity, out DynamicBuffer<CConsumedPart> buffer))
                buffer = ctx.AddBuffer<CConsumedPart>(applianceEntity);
            if (buffer.Length >= MaxAttachmentCount)
                return false;

            ctx.AppendToBuffer(applianceEntity, new CConsumedPart()
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
