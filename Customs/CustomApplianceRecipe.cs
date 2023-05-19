using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomApplianceRecipe : CustomLocalisedGameDataObject<ApplianceRecipe, ApplianceRecipeInfo>
    {
        public override sealed int BaseGameDataObjectID => -1;

        /// <summary>
        /// Dictionary containing quantity of each required AppliancePart.
        /// </summary>
        public virtual Dictionary<AppliancePart, int> Parts { get; protected set; }

        /// <summary>
        /// Appliance obtained from recipe. Can also be a PartialAppliance.
        /// </summary>
        public virtual Appliance Result { get; protected set; }

        /// <summary>
        /// If true, recipe will not be completed if there are additional parts deposited.
        /// </summary>
        public virtual bool RequireExactMatch { get; protected set; } = false;

        /// <summary>
        /// If true, recipe will not be made visible and no notification for completed recipe will be shown.
        /// </summary>
        public virtual bool IsRecipeHidden { get; protected set; } = false;

        /// <summary>
        /// If true, parts required will not be visible.
        /// </summary>
        public virtual bool IsPartsHidden { get; protected set; } = false;

        /// <summary>
        /// If true, result will not be shown when recipe is completed.
        /// </summary>
        public virtual bool IsResultHidden { get; protected set; } = false;

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            ApplianceRecipe applianceRecipe = ScriptableObject.CreateInstance<ApplianceRecipe>();
            if (applianceRecipe.ID != ID)
            {
                applianceRecipe.ID = ID;
            }
            if (applianceRecipe.RequireExactMatch != RequireExactMatch)
            {
                applianceRecipe.RequireExactMatch = RequireExactMatch;
            }
            if (applianceRecipe.IsRecipeHidden != IsRecipeHidden)
            {
                applianceRecipe.IsRecipeHidden = IsRecipeHidden;
            }
            if (applianceRecipe.IsPartsHidden != IsPartsHidden)
            {
                applianceRecipe.IsPartsHidden = IsPartsHidden;
            }
            if (applianceRecipe.IsResultHidden != IsResultHidden)
            {
                applianceRecipe.IsResultHidden = IsResultHidden;
            }
            if (applianceRecipe.Info == null)
            {
                applianceRecipe.Info = new LocalisationObject<ApplianceRecipeInfo>();
                if (!applianceRecipe.Info.Has(Locale.English))
                {
                    applianceRecipe.Info.Add(Locale.English, new ApplianceRecipeInfo
                    {
                        Name = "Appliance Recipe",
                        Description = "Even I don't know the parts needed...",
                        FlavourText = string.Empty,
                        //MissingPartsTitle = "Add More Parts",
                        //RecipeCompletedBody = string.Empty
                    });
                }
            }
            gameDataObject = applianceRecipe;
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gameDataObject)
        {
            ApplianceRecipe applianceRecipe = (ApplianceRecipe)gameDataObject;
            if (applianceRecipe.Name.IsNullOrEmpty())
            {
                applianceRecipe.Name = $"{GetType().Name}";
            }
            if (applianceRecipe.Result != Result)
            {
                applianceRecipe.Result = Result;
            }
            if (applianceRecipe.Parts != Parts)
            {
                applianceRecipe.Parts = Parts;
            }
            //if (applianceRecipe.RecipeCompletedBody.IsNullOrEmpty() && applianceRecipe.Result != null)
            //{
            //    applianceRecipe.RecipeCompletedBody = $"Interact to craft{(IsResultHidden? "" : $" {applianceRecipe.Result.Name}")}";
            //}
        }
    }
}
