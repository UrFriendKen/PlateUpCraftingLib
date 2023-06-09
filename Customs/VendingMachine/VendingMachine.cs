﻿using CraftingLib.Views;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using TestCubes;
using UnityEngine;

namespace CraftingLib.Customs.VendingMachine
{
    public class VendingMachine : CustomAppliance
    {
        public override string UniqueNameID => "vendingMachine";

        public override GameObject Prefab => TestCubeManager.GetPrefab<VendingMachine>(
            scaleX: 0.85f, scaleZ: 0.5f, material: MaterialUtils.GetExistingMaterial("Plastic - Dark Green"));

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>()
        {
            new CAppliancePartVendor(),
            new CImmovable(),
            new CFixedRotation(),
            new CDestroyApplianceAtDay()
            {
                HideBin = true
            },
            new CTakesDuration()
            {
                Total = 3f,
                Manual = true,
                ManualNeedsEmptyHands = true,
                Mode = InteractionMode.Appliances
            },
            new CDisplayDuration()
            {
                Process = ProcessReferences.Purchase,
                ShowWhenEmpty = false,
                IsBad = false
            }
        };

        public override bool IsPurchasable => false;

        public override bool IsPurchasableAsUpgrade => false;

        public override List<(Locale, ApplianceInfo)> InfoList => new List<(Locale, ApplianceInfo)>()
        {
            (Locale.English, new ApplianceInfo()
            {
                Name = "Parts Vending Machine",
                Description = "Buy Appliance Parts Here"
            })
        };

        public override void SetupPrefab(GameObject prefab)
        {
            VendorDisplayPartSubview partDisplayView = prefab.AddComponent<VendorDisplayPartSubview>();
            GameObject holder = new GameObject("Part Hold Point");
            holder.transform.SetParent(partDisplayView.transform);
            holder.transform.localScale = Vector3.one * 0.6f;
            holder.transform.localPosition = new Vector3(0f, 1f, 0f);
            holder.transform.localRotation = Quaternion.identity;
            partDisplayView.Holder = holder;
        }
    }
}
