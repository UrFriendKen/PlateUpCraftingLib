using KitchenData;

namespace CraftingLib.GameDataObjects
{
    public class ApplianceRecipeInfo : Localisation
    {

        /// <summary>
        /// Name of recipe in recipe book.
        /// </summary>
        public string Name;

        /// <summary>
        /// Body of recipe in recipe book.
        /// </summary>
        public string Description;

        /// <summary>
        /// Quotes/Jokes/Lore. Or leave empty if you're boring.
        /// </summary>
        public string FlavourText;

        /// <summary>
        /// Title of section in pinged info for next available non-hidden recipe.
        /// </summary>
        public string MissingPartsTitle;

        /// <summary>
        /// Body of section in pinged info for notifying action to be taken and/or completed recipe result.
        /// </summary>
        public string RecipeCompletedBody;

        public override void Export(LocalisationContext context)
        {
            base.SetContext(context);
            context.Add("NAME", Name);
            context.Add("DESCRIPTION", Description);
            context.Add("FLAVOUR", FlavourText);
            context.Add("MISSING", MissingPartsTitle);
            context.Add("COMPLETED_BODY", RecipeCompletedBody);
        }

        public override void Import(LocalisationContext context)
        {
            base.SetContext(context);
            Name = context.Get("NAME");
            Description = context.Get("DESCRIPTION");
            Description = context.Get("FLAVOUR");
            MissingPartsTitle = context.Get("MISSING");
            RecipeCompletedBody = context.Get("COMPLETED_BODY");
        }
    }
}
