using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomAppliancePartRecipeGroup : CustomGameDataObject
    {
        public override sealed int BaseGameDataObjectID => -1;

        /// <summary>
        /// Recipes in this group
        /// </summary>
        public virtual HashSet<AppliancePartRecipe> Recipes { get; protected set; } = new HashSet<AppliancePartRecipe>();

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            AppliancePartRecipeGroup applianceRecipe = ScriptableObject.CreateInstance<AppliancePartRecipeGroup>();
            if (applianceRecipe.ID != ID)
            {
                applianceRecipe.ID = ID;
            }
            gameDataObject = applianceRecipe;
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gameDataObject)
        {
            AppliancePartRecipeGroup appliancePartRecipe = (AppliancePartRecipeGroup)gameDataObject;
            if (appliancePartRecipe.Recipes != Recipes)
            {
                appliancePartRecipe.Recipes = Recipes;
            }
        }
    }
}
