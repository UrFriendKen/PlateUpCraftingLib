using CraftingLib;
using CraftingLib.Customs;
using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using Unity.Entities;
using UnityEngine;
using static KitchenCraftingLibTest.Customs.Components;

namespace KitchenCraftingLibTest.Customs.ApplianceParts
{
    public class RedPart : CustomAppliancePart
    {
        public override string UniqueNameID => "redPart";

        public override GameObject Prefab => TestCubeManager.GetPrefab<RedPart>(
            scaleX: 0.3f, scaleY: 0.3f, scaleZ: 0.3f, material: MaterialUtils.GetExistingMaterial("Plastic - Red"));

        public override List<(Locale, BasicInfo)> InfoList => new List<(Locale, BasicInfo)>()
        {
            (Locale.English, new BasicInfo()
            {
                Name = "Red Part",
                Description = "A red part. Duh"
            })
        };

        public override List<IAppliancePartProperty> Properties => new List<IAppliancePartProperty>()
        {
            new CNonDisposablePart()
        };
        public override bool IsDetachable => true;

        public override bool IsAttachable => true;

        public override HashSet<Appliance> AttachableAppliances => new HashSet<Appliance>()
        {
            GDOUtils.GetExistingGDO(ApplianceReferences.Grabber) as Appliance
        };

        public override List<IComponentData> ComponentsAddWhenAttached => new List<IComponentData>()
        {
            new CPermanentlyOnFire()
        };

        public override List<IComponentData> ComponentsAddWhenDetached => new List<IComponentData>()
        {
            new CRemovePermanentlyOnFire()
        };

        public override void OnRegister(AppliancePart gameDataObject)
        {
            base.OnRegister(gameDataObject);
        }
    }
}
