using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using System;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomAppliancePart : CustomGenericLocalisation<AppliancePart>, ICustomHasPrefab
    {
        public override sealed int BaseGameDataObjectID => -1;

        public virtual GameObject Prefab { get; protected set; }

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
    }
}
