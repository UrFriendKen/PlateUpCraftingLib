using KitchenData;
using System.Collections.Generic;
using System.Linq;

namespace CraftingLib.GameDataObjects
{
    public class ApplianceRecipe : LocalisedGameDataObject<ApplianceRecipeInfo>
    {
        public Dictionary<AppliancePart, int> Parts;
        public bool RequireExactMatch;
        public bool IsRecipeHidden;
        public bool IsPartsHidden;
        public bool IsResultHidden;
        public Appliance Result;

        public string Name = "Appliance Recipe";
        public string Description = "Even I don't know the parts needed...";
        public string FlavourText = string.Empty;
        //public string MissingPartsTitle = "Add More Parts";
        //public string RecipeCompletedBody = string.Empty;

        public ApplianceRecipe() { }

        public int PartCount()
        {
            return Parts.Select(x => x.Value).Sum();
        }

        public int PartCount(int partID)
        {
            if (!GameData.Main.TryGet(partID, out AppliancePart part, warn_if_fail: true))
                return 0;
            return Parts.TryGetValue(part, out int count) ? count : 0;
        }

        public bool IsMatch(List<int> partIDs)
        {
            List<int> remaining = new List<int>(partIDs);
            foreach (KeyValuePair<AppliancePart, int> part in Parts)
            {
                for (int i = 0; i < part.Value; i++)
                {
                    if (!remaining.Contains(part.Key.ID))
                        return false;
                    remaining.Remove(part.Key.ID);
                }

                if (RequireExactMatch && remaining.Contains(part.Key.ID))
                    return false;
            }
            return !RequireExactMatch || remaining.Count == 0;
        }

        protected override void InitialiseDefaults()
        {
            Parts = new Dictionary<AppliancePart, int>();
        }

        public override bool Localise(KitchenData.Locale locale, StringSubstitutor subs)
        {
            ApplianceRecipeInfo applianceRecipeInfo = Info?.Get(locale);
            if (applianceRecipeInfo == null)
            {
                return false;
            }
            Name = subs.Parse(applianceRecipeInfo.Name);
            Description = subs.Parse(applianceRecipeInfo.Description);
            FlavourText = subs.Parse(applianceRecipeInfo.FlavourText);
            //MissingPartsTitle = subs.Parse(applianceRecipeInfo.MissingPartsTitle);
            //RecipeCompletedBody = subs.Parse(applianceRecipeInfo.RecipeCompletedBody);
            return true;
        }
    }
}
