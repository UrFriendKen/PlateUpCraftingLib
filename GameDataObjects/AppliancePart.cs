﻿using Kitchen;
using KitchenData;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.GameDataObjects
{
    public class AppliancePart : LocalisedGameDataObject<BasicInfo>, IHasPrefab
    {
        public string Name = "Appliance Part";
        public string Description = "Part of an appliance";

        public GameObject Prefab { get; set; }

        public List<IAppliancePartProperty> Properties;

        public bool IsWithdrawable;

        public bool IsAttachable;

        public bool IsDetachable;

        public HashSet<Appliance> AttachableToAppliances;

        public List<IComponentData> ComponentsAddWhenAttached;

        public List<IComponentData> ComponentsAddWhenDetached;

        protected override void InitialiseDefaults()
        {
            Properties = new List<IAppliancePartProperty>();
            AttachableToAppliances = new HashSet<Appliance>();
            ComponentsAddWhenAttached = new List<IComponentData>();
            ComponentsAddWhenDetached = new List<IComponentData>();
        }

        public override bool Localise(Locale locale, StringSubstitutor subs)
        {
            BasicInfo basicInfo = Info?.Get(locale);
            if (basicInfo == null)
            {
                return false;
            }
            Name = subs.Parse(basicInfo.Name);
            Description = subs.Parse(basicInfo.Description);
            return true;
        }

        public bool GetProperty<T>(out T result) where T : IAppliancePartProperty
        {
            result = default(T);
            if (Properties == null)
            {
                return false;
            }
            foreach (IAppliancePartProperty property in Properties)
            {
                if (property is T val)
                {
                    result = val;
                    return true;
                }
            }
            return false;
        }

        public bool IsAttachableTo(int applianceID)
        {
            if (!IsAttachable)
                return false;
            if (!GameData.Main.TryGet<Appliance>(applianceID, out Appliance appliance))
                return false;
            if (!AttachableToAppliances.Contains(appliance))
                return false;
            return true;
        }
    }
}
