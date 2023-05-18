using CraftingLib;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class YellowProvider : CustomAppliance
    {
        public override string UniqueNameID => "yellowProvider";

        public override GameObject Prefab => TestCubeManager.GetPrefab<YellowProvider>(
            scale: 0.8f, material: MaterialUtils.GetExistingMaterial("Plastic - Yellow"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CAppliancePartStore()
            {
                PartID = GDOUtils.GetCustomGameDataObject<YellowPart>().GameDataObject.ID,
                Total = 3,
                Remaining = 2
            },
            new CSpecialAppliancePartStore()
            {
                SpecialStore = true,
                SpecialRetrieve = false
            },
            new CDestroyWhenDepleted()
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Yellow Provider",
                Description = "This is a part provider",
                Sections = new List<Appliance.Section>()
                {
                    new Appliance.Section()
                    {
                        Title = "Yellow",
                        Description = "Gives Yellow Parts. Starts with 2 parts."
                    }
                }
            })
        };
    }
}
