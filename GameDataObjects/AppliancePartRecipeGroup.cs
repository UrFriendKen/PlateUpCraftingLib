using KitchenData;
using System.Collections.Generic;
using System.Linq;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePartRecipeGroup : GameDataObject
    {
        public HashSet<AppliancePartRecipe> Recipes;

        protected override void InitialiseDefaults()
        {
            Recipes = new HashSet<AppliancePartRecipe>();
        }

        public bool IsMatch(int applianceID, List<int> appliancePartIDs, out AppliancePart result)
        {
            foreach (AppliancePartRecipe recipe in Recipes.OrderByDescending(x => x.Inputs.Count))
            {
                if (recipe.Result == null)
                    continue;

                if (recipe.IsMatch(applianceID, appliancePartIDs))
                {
                    result = recipe.Result;
                    return true;
                }
            }
            result = null;
            return false;
        }

        public bool UsesPart(int applianceID, int appliancePartID)
        {
            foreach (AppliancePartRecipe recipe in Recipes.OrderByDescending(x => x.Inputs.Count))
            {
                if (recipe.Result == null)
                    continue;
                if (!recipe.AppliesToAppliance(applianceID))
                    continue;
                if (recipe.UsesPart(appliancePartID))
                    return true;
            }
            return false;
        }
    }
}
