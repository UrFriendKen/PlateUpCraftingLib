using CraftingLib.GameDataObjects;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.Customs
{
    public abstract class CustomAppliancePart : CustomGenericLocalisation<AppliancePart>, ICustomHasPrefab
    {
        public override sealed int BaseGameDataObjectID => -1;

        public virtual GameObject Prefab { get; protected set; }

        public virtual List<IAppliancePartProperty> Properties { get; protected set; } = new List<IAppliancePartProperty>();

        /// <summary>
        /// Setting to true prevents Appliance Part being retrieved from Appliance Part Stores using CraftingLib.RetrievePart system.
        /// Set to false want to use the base Appliance Part Store behavior.
        /// </summary>
        public virtual bool IsNonRetrievable { get; protected set; }

        /// <summary>
        /// Setting to true allows withdrawing Appliance Part from Partial Appliances using CraftingLib.WithdrawPart system.
        /// Set to false if you do not want the part to be withdrawable, or want to make your own withdrawal system.
        /// </summary>
        public virtual bool IsWithdrawable { get; protected set; }

        /// <summary>
        /// Setting to true allows detaching Appliance Part from Attachable Appliances using CraftingLib.DetachPart system.
        /// Set to false if you do not want the part to be detachable, or want to make your own detach system.
        /// </summary>
        public virtual bool IsDetachable { get; protected set; }

        public virtual HashSet<Appliance> AttachableAppliances { get; protected set; } = new HashSet<Appliance>();

        public virtual List<IComponentData> ComponentsAddWhenAttached { get; protected set; } = new List<IComponentData>();

        public virtual List<IComponentData> ComponentsAddWhenDetached { get; protected set; } = new List<IComponentData>();

        public virtual int PurchaseCost { get; protected set; } = 3;

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
            if (appliancePart.IsNonRetrievable != IsNonRetrievable)
            {
                appliancePart.IsNonRetrievable = IsNonRetrievable;
            }
            if (appliancePart.IsWithdrawable != IsWithdrawable)
            {
                appliancePart.IsWithdrawable = IsWithdrawable;
            }
            if (appliancePart.IsDetachable != IsDetachable)
            {
                appliancePart.IsDetachable = IsDetachable;
            }
            if (appliancePart.PurchaseCost != PurchaseCost)
            {
                appliancePart.PurchaseCost = PurchaseCost;
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
            if (appliancePart.AttachableToAppliances != AttachableAppliances)
            {
                appliancePart.AttachableToAppliances = AttachableAppliances;
            }
            if (appliancePart.ComponentsAddWhenAttached != ComponentsAddWhenAttached)
            {
                appliancePart.ComponentsAddWhenAttached = ComponentsAddWhenAttached;
            }
            if (appliancePart.ComponentsAddWhenDetached != ComponentsAddWhenDetached)
            {
                appliancePart.ComponentsAddWhenDetached = ComponentsAddWhenDetached;
            }
        }
    }
}
