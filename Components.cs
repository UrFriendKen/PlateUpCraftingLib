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
    }
    public struct CAttachAppliancePartProperties : IComponentData, IModComponent { }

    public struct CNonDisposablePart : IAppliancePartProperty, IComponentData, IModComponent { }



    public struct CAppliancePartStore : IApplianceProperty, IComponentData, IAttachableProperty, IModComponent
    {
        public int PartID;
        public int Total;
        public int Remaining;

        public bool IsInUse => (IsInfinite || Remaining > 0) && PartID != 0;
        public bool IsFull => !IsInfinite && Remaining == Total;
        public bool IsInfinite => Total < 1;

        public bool Consume()
        {
            if (!IsInUse)
                return false;

            if (Remaining > 0)
                Remaining--;
            return true;
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

        public bool NeedsPart(EntityContext ctx, Entity e, int partID)
        {
            if (!GameData.Main.TryGet(ID, out PartialAppliance gdo, warn_if_fail: true))
                return false;

            if (!ctx.RequireBuffer(e, out DynamicBuffer<CConsumedPart> buffer))
            {
                buffer = ctx.AddBuffer<CConsumedPart>(e);
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

        public bool DepositPart(EntityContext ctx, Entity e, int partID)
        {
            if (!NeedsPart(ctx, e, partID))
                return false;

            ctx.AppendToBuffer(e, new CConsumedPart()
            {
                ID = partID
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
            ecb.AddBuffer<CConsumedPart>(e);
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
    }

    [InternalBufferCapacity(5)]
    public struct CConsumedPart : IBufferElementData
    {
        public int ID;
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
}
