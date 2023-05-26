using CraftingLib.Customs.CraftingDesk;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;

namespace CraftingLib.Customs.CraftingDesk
{
    public class CraftingDesk : CustomAppliance
    {
        public override string UniqueNameID => "craftingDesk";

        public override GameObject Prefab => TestCubeManager.GetPrefab<CraftingDesk>(
            scaleX: 0.85f, scaleY: 0.5f, scaleZ: 0.4f, material: MaterialUtils.GetExistingMaterial("Plastic - Blue"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CAppliancePartCraftStation()
            {
                RecipeGroupID = GDOUtils.GetCustomGameDataObject<CraftingDeskRecipeGroup>()?.GameDataObject.ID ?? 0,
                SlotCount = 3,
                AllowAddAnyPart = true,
                MultipleCraftsInDay = false
            },
            new CTakesDuration()
            {
                Total = 3f,
                Manual = true,
                Mode = InteractionMode.Items
            },
            new CDisplayDuration()
            {
                Process = ProcessReferences.Purchase,
                ShowWhenEmpty = true,
                IsBad = false
            }
        };

        public override bool IsPurchasable => false;

        public override bool IsPurchasableAsUpgrade => true;

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Crafting Desk",
                Description = "Combine appliance parts"
            })
        };
    }
}
