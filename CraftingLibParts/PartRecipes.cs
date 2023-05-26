using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace CraftingLibParts
{
    public abstract class StandardRecipe : CustomAppliancePartRecipe
    {
        public override sealed bool AllowCraftingDesk => true;
    }

    public class TwoShardsToOneCrystal : StandardRecipe
    {
        public override string UniqueNameID => "twoShardsToOneCrystal";
        public override Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
        {
            { GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Iron3>(), 2 }
        };
        public override AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Iron>();
    }

    public class CrystalToFaceted : StandardRecipe
    {
        public override string UniqueNameID => "crystalToFaceted";
        public override Dictionary<AppliancePart, int> Inputs => new Dictionary<AppliancePart, int>
        {
            { GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Iron>(), 1 }
        };
        public override AppliancePart Result => GDOUtils.GetCastedGDO<AppliancePart, BasicParts.Iron2>();
    }
}
