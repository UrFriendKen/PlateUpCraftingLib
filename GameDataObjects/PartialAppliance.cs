using KitchenBlargleBrew.kegerator;
using KitchenData;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities.UniversalDelegates;

namespace CraftingLib.GameDataObjects
{
    public class PartialAppliance : Appliance
    {
        [Serializable]
        public struct ApplianceRecipe
        {
            public Dictionary<AppliancePart, int> Parts;
            public bool RequireExactMatch;
            public Appliance Result;

            public int PartCount()
            {
                return Parts.Select(x => x.Value).Sum();
            }

            public int PartCount(int partID)
            {
                if (!GameData.Main.TryGet(partID, out AppliancePart part, warn_if_fail: true))
                    return 0;
                return Parts.TryGetValue(part, out int count)? count : 0;
            }

            public bool IsMatch(List<int> partIDs)
            {
                List<int> remaining = new List<int>(partIDs);
                foreach(KeyValuePair<AppliancePart, int> part in Parts)
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
        }

        public List<PartialAppliance.ApplianceRecipe> Recipes;
    }
}
