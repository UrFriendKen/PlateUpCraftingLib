using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class PopulateBasicPartsOffers : RestaurantSystem, IModSystem
    {
        public static List<int> Offers;

        public struct SOffersPopulated : IComponentData, IModComponent { }

        protected override void OnUpdate()
        {
            if (Has<SIsNightFirstUpdate>() || Offers.IsNullOrEmpty())
                Offers = PartsRegistry.GetAllBasicApplianceParts().Select(x => x.ID).ToList();

            if (Has<SIsDayTime>() && TryGetSingletonEntity<SOffersPopulated>(out Entity singletonEntity))
                EntityManager.DestroyEntity(singletonEntity);
        }
    }
}
