using KitchenData;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePart : LocalisedGameDataObject<BasicInfo>, IHasPrefab
    {
        public string Name = "Appliance Part";
        public string Description = "Part of an appliance";

        public GameObject Prefab { get; set; }

        public List<IAppliancePartProperty> Properties;

        protected override void InitialiseDefaults()
        {
            Properties = new List<IAppliancePartProperty>();
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

        public bool GetProperty<T>(out T result) where T : IAppliancePartProperty
        {
            result = default(T);
            if (Properties == null)
            {
                return false;
            }
            foreach (IAppliancePartProperty property in Properties)
            {
                if (property is T val)
                {
                    result = val;
                    return true;
                }
            }
            return false;
        }
    }
}
