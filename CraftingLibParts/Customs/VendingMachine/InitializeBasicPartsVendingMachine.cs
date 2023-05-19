using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using Unity.Entities;

namespace CraftingLibParts.Customs.VendingMachine
{
    public class InitializeBasicPartsVendingMachine : NightSystem, IModSystem
    {
        protected override void Initialise()
        {
            base.Initialise();
            RequireSingletonForUpdate<PopulateBasicPartsOffers.SOffersPopulated>();
        }

        protected override void OnUpdate()
        {
            if (!TryGetSingletonEntity<CreateBasicPartsVendingMachine.SBasicPartsVendingMachine>(out Entity singletonEntity))
                return;
            if (Has<CIsInitialized>(singletonEntity))
                return;

            int partID = 0;
            int partCost = 0;

            for (int i = 0; i < PopulateBasicPartsOffers.Offers.Count; i++)
            {
                if (!GameData.Main.TryGet(PopulateBasicPartsOffers.Offers[i], out AppliancePart part))
                    continue;
                partID = part.ID;
                partCost = part.PurchaseCost;
                break;
            }
            Set(singletonEntity, new CPartsVendor()
            {
                PartID = partID,
                Cost = partCost
            });
            Set(singletonEntity, new CIsInitialized());
        }
    }
}
