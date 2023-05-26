using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomAppliancePartRecipe : CustomGameDataObject
    {
        public override sealed int BaseGameDataObjectID => -1;

        /// <summary>
        /// Appliances that can use this recipe.
        /// </summary>
        public virtual HashSet<Appliance> PossibleAppliances { get; protected set; } = new HashSet<Appliance>();
        /// <summary>
        /// If true, recipe can be used with Crafting Desk. Otherwise, make your own crafting appliance. Default true.
        /// </summary>
        public virtual bool AllowCraftingDesk { get; protected set; } = true;
        /// <summary>
        /// Dictionary containing quantity of each required AppliancePart.
        /// </summary>
        public virtual Dictionary<AppliancePart, int> Inputs { get; protected set; } = new Dictionary<AppliancePart, int>();
        /// <summary>
        /// Appliance Part obtained from recipe.
        /// </summary>
        public virtual AppliancePart Result { get; protected set; }

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            AppliancePartRecipe appliancePartRecipe = ScriptableObject.CreateInstance<AppliancePartRecipe>();
            if (appliancePartRecipe.ID != ID)
            {
                appliancePartRecipe.ID = ID;
            }
            if (appliancePartRecipe.AllowCraftingDesk != AllowCraftingDesk)
            {
                appliancePartRecipe.AllowCraftingDesk = AllowCraftingDesk;
            }
            gameDataObject = appliancePartRecipe;
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gameDataObject)
        {
            AppliancePartRecipe appliancePartRecipe = (AppliancePartRecipe)gameDataObject;
            if (appliancePartRecipe.PossibleAppliances != PossibleAppliances)
            {
                appliancePartRecipe.PossibleAppliances = PossibleAppliances;
            }
            if (appliancePartRecipe.Inputs != Inputs)
            {
                appliancePartRecipe.Inputs = Inputs;
            }
            if (appliancePartRecipe.Result != Result)
            {
                appliancePartRecipe.Result = Result;
            }
        }
    }
}
