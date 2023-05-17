using KitchenData;
using UnityEngine;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePart : GameDataObject, IHasPrefab
    {
        public GameObject Prefab { get; set; }

        protected override void InitialiseDefaults()
        {
        }
    }
}
