using CraftingLib;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using TestCubes;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedProvider : CustomAppliance
    {
        public override string UniqueNameID => "redProvider";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedProvider>(
            scaleX: 0.8f, scaleY: 0.8f, scaleZ: 0.8f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CAppliancePartStore()
            {
                PartID = GDOUtils.GetCustomGameDataObject<RedPart>().GameDataObject.ID,
                Total = 0,
                Remaining = 0
            }
        };

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Red Provider",
                Description = "This is a part provider",
                Sections = new List<Appliance.Section>()
                {
                    new Appliance.Section()
                    {
                        Title = "Red",
                        Description = "Gives Red Parts"
                    }
                },
                Tags = new List<string>() { "Infinite" }
            })
        };
    }
}
