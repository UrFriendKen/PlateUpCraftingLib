using KitchenData;
using UnityEngine;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePart : LocalisedGameDataObject<BasicInfo>, IHasPrefab
    {
        public string Name = "Appliance Part";
        public string Description = "Part of an appliance";

        public GameObject Prefab { get; set; }

        protected override void InitialiseDefaults()
        {
        }

        public override bool Localise(Locale locale, StringSubstitutor subs)
        {
            BasicInfo basicInfo = Info?.Get(locale);
            if (basicInfo == null)
            {
                return false;
            }
            Name = subs.Parse(basicInfo.Name);
            Description = subs.Parse(basicInfo.Description);
            return true;
        }
    }
}
