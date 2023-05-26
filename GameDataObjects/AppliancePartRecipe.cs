using CraftingLib.Customs.CraftingDesk;
using KitchenData;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePartRecipe : GameDataObject
    {
        public HashSet<Appliance> PossibleAppliances;
        public bool AllowCraftingDesk;
        public Dictionary<AppliancePart, int> Inputs;
        public AppliancePart Result;

        private int CraftingDeskApplianceID => GDOUtils.GetCustomGameDataObject<CraftingDesk>().GameDataObject.ID;

        protected override void InitialiseDefaults()
        {
            Inputs = new Dictionary<AppliancePart, int>();
        }

        public bool IsMatch(int applianceID, List<int> appliancePartIDs)
        {
            if (!(AllowCraftingDesk && applianceID == CraftingDeskApplianceID) && !PossibleAppliances.Select(x => x.ID).Contains(applianceID))
                return false;

            Dictionary<int, int> requiredParts = Inputs.ToDictionary(item => item.Key.ID, item => item.Value);
            foreach (int partID in appliancePartIDs)
            {
                bool isPartInRecipe = requiredParts.TryGetValue(partID, out int partCount);
                if (!isPartInRecipe || partCount < 1)
                {
                    return false;
                }
                requiredParts[partID]--;
                if (requiredParts[partID] < 1)
                    requiredParts.Remove(partID);
            }
            return requiredParts.Select(item => item.Value > 0).Count() == 0;
        }

        public bool AppliesToAppliance(int applianceID)
        {
            return PossibleAppliances.Select(appliance => appliance.ID).Contains(applianceID) && applianceID != 0;
        }

        public bool UsesPart(int partID)
        {
            return Inputs.Keys.Select(part => part.ID).Contains(partID) && partID != 0;
        }
    }
}
