using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomAppliancePart : CustomGenericLocalisation<AppliancePart>, ICustomHasPrefab
    {
        public override sealed int BaseGameDataObjectID => -1;

        public virtual GameObject Prefab { get; protected set; }

        public virtual List<IAppliancePartProperty> Properties { get; protected set; } = new List<IAppliancePartProperty>();

        public virtual void SetupPrefab(GameObject prefab)
        {
        }

        public override void Convert(GameData gameData, out GameDataObject gameDataObject)
        {
            AppliancePart appliancePart = ScriptableObject.CreateInstance<AppliancePart>();
            if (appliancePart.ID != ID)
            {
                appliancePart.ID = ID;
            }
            if (appliancePart.Prefab != Prefab)
            {
                appliancePart.Prefab = Prefab;
            }
            if (appliancePart.Info != Info)
            {
                appliancePart.Info = Info;
            }
            if (InfoList.Count > 0)
            {
                appliancePart.Info = new LocalisationObject<BasicInfo>();
                foreach (var info in InfoList)
                {
                    appliancePart.Info.Add(info.Item1, info.Item2);
                }
            }
            if (appliancePart.Info == null)
            {
                appliancePart.Info = new LocalisationObject<BasicInfo>();
                if (!appliancePart.Info.Has(Locale.English))
                {
                    appliancePart.Info.Add(Locale.English, new BasicInfo
                    {
                        Name = "Appliance Part",
                        Description = "Part of an appliance"
                    });
                }
            }
            gameDataObject = appliancePart;
        }

        public override void AttachDependentProperties(GameData gameData, GameDataObject gameDataObject)
        {
            AppliancePart appliancePart = (AppliancePart)gameDataObject;
            if (appliancePart.Properties != Properties)
            {
                appliancePart.Properties = Properties;
            }
        }
    }
}
