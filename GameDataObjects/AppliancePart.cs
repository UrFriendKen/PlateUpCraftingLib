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
    }
}
