using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftingLib.Customs
{
    public interface IPartialAppliance
    {
        List<ApplianceRecipe> Recipes { get; }
    }

    public abstract class CustomPartialAppliance : CustomAppliance, IPartialAppliance
    {
        [Obsolete("Please set your Name in Info")]
        public virtual string Name { get; protected set; } = "Partial Appliance";


        [Obsolete("Please set your Description in Info")]
        public virtual string Description { get; protected set; } = "For all your crafting needs";

        public virtual List<ApplianceRecipe> Recipes { get; protected set; } = new List<ApplianceRecipe>();

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            PartialAppliance partialAppliance = ScriptableObject.CreateInstance<PartialAppliance>();
            if (BaseGameDataObjectID != -1)
            {
                partialAppliance = UnityEngine.Object.Instantiate<PartialAppliance>((PartialAppliance)gameData.Get<PartialAppliance>().FirstOrDefault((Appliance a) => a.ID == BaseGameDataObjectID));
            }

            if (partialAppliance.ID != ID)
            {
                partialAppliance.ID = ID;
            }

            if (partialAppliance.Prefab != Prefab)
            {
                partialAppliance.Prefab = Prefab;
            }

            if (partialAppliance.HeldAppliancePrefab != HeldAppliancePrefab)
            {
                partialAppliance.HeldAppliancePrefab = HeldAppliancePrefab;
            }

            if (partialAppliance.EffectRange != EffectRange)
            {
                partialAppliance.EffectRange = EffectRange;
            }

            if (partialAppliance.EffectCondition != EffectCondition)
            {
                partialAppliance.EffectCondition = EffectCondition;
            }

            if (partialAppliance.EffectType != EffectType)
            {
                partialAppliance.EffectType = EffectType;
            }

            if (partialAppliance.IsNonInteractive != IsNonInteractive)
            {
                partialAppliance.IsNonInteractive = IsNonInteractive;
            }

            if (partialAppliance.Layer != Layer)
            {
                partialAppliance.Layer = Layer;
            }

            if (partialAppliance.ForceHighInteractionPriority != ForceHighInteractionPriority)
            {
                partialAppliance.ForceHighInteractionPriority = ForceHighInteractionPriority;
            }

            if (partialAppliance.EntryAnimation != EntryAnimation)
            {
                partialAppliance.EntryAnimation = EntryAnimation;
            }

            if (partialAppliance.ExitAnimation != ExitAnimation)
            {
                partialAppliance.ExitAnimation = ExitAnimation;
            }

            if (partialAppliance.SkipRotationAnimation != SkipRotationAnimation)
            {
                partialAppliance.SkipRotationAnimation = SkipRotationAnimation;
            }

            if (partialAppliance.IsPurchasable != IsPurchasable)
            {
                partialAppliance.IsPurchasable = IsPurchasable;
            }

            if (partialAppliance.IsPurchasableAsUpgrade != IsPurchasableAsUpgrade)
            {
                partialAppliance.IsPurchasableAsUpgrade = IsPurchasableAsUpgrade;
            }

            if (partialAppliance.ThemeRequired != ThemeRequired)
            {
                partialAppliance.ThemeRequired = ThemeRequired;
            }

            if (partialAppliance.ShoppingTags != ShoppingTags)
            {
                partialAppliance.ShoppingTags = ShoppingTags;
            }

            if (partialAppliance.RarityTier != RarityTier)
            {
                partialAppliance.RarityTier = RarityTier;
            }

            if (partialAppliance.PriceTier != PriceTier)
            {
                partialAppliance.PriceTier = PriceTier;
            }

            if (partialAppliance.ShopRequirementFilter != ShopRequirementFilter)
            {
                partialAppliance.ShopRequirementFilter = ShopRequirementFilter;
            }

            if (partialAppliance.StapleWhenMissing != StapleWhenMissing)
            {
                partialAppliance.StapleWhenMissing = StapleWhenMissing;
            }

            if (partialAppliance.SellOnlyAsDuplicate != SellOnlyAsDuplicate)
            {
                partialAppliance.SellOnlyAsDuplicate = SellOnlyAsDuplicate;
            }

            if (partialAppliance.SellOnlyAsUnique != SellOnlyAsUnique)
            {
                partialAppliance.SellOnlyAsUnique = SellOnlyAsUnique;
            }

            if (partialAppliance.PreventSale != PreventSale)
            {
                partialAppliance.PreventSale = PreventSale;
            }

            if (partialAppliance.IsNonCrated != IsNonCrated)
            {
                partialAppliance.IsNonCrated = IsNonCrated;
            }

            if (partialAppliance.Info != Info)
            {
                partialAppliance.Info = Info;
            }

            if (PurchaseCostOverride != -1)
            {
                ApplianceOverrides.AddPurchaseCostOverride(partialAppliance.ID, PurchaseCostOverride);
            }

            if (InfoList.Count > 0)
            {
                partialAppliance.Info = new LocalisationObject<ApplianceInfo>();
                foreach (var info in InfoList)
                {
                    partialAppliance.Info.Add(info.Item1, info.Item2);
                }
            }

            if (partialAppliance.Info == null)
            {
                partialAppliance.Info = new LocalisationObject<ApplianceInfo>();
                if (!partialAppliance.Info.Has(Locale.English))
                {
                    partialAppliance.Info.Add(Locale.English, new ApplianceInfo
                    {
                        Name = Name,
                        Description = Description,
                        Sections = Sections,
                        Tags = Tags
                    });
                }
            }
            gameDataObject = partialAppliance;
        }

        public sealed override void OnRegister(Appliance gameDataObject)
        {
            OnRegister((PartialAppliance)gameDataObject);
        }

        public virtual void OnRegister(PartialAppliance gameDataObject)
        {
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gameDataObject)
        {
            base.AttachDependentProperties(gameData, gameDataObject);
            PartialAppliance partialAppliance = (PartialAppliance)gameDataObject;
            if (partialAppliance.Recipes != Recipes)
                partialAppliance.Recipes = Recipes;

            if (!partialAppliance.Properties.Select(property => property.GetType()).Contains(typeof(CPartialAppliance)))
            {
                partialAppliance.Properties.Add(new CPartialAppliance(gameDataObject.ID));
            }
        }
    }
}
