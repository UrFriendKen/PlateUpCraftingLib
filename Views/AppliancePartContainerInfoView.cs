using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.Views
{
    public class AppliancePartContainerInfoView : InfoView<AppliancePartContainerInfoView.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper()
                    .All(typeof(CLinkedView), typeof(CAppliancePartContainerInfo)));
            }

            protected override void OnUpdate()
            {
                using NativeArray<CLinkedView> views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using NativeArray<CAppliancePartContainerInfo> infos = Views.ToComponentDataArray<CAppliancePartContainerInfo>(Allocator.Temp);

                int money = 0;
                if (Require(out SMoney sMoney))
                    money = sMoney.Amount;

                for (int i = 0; i < views.Length; i++)
                {
                    CLinkedView view = views[i];
                    CAppliancePartContainerInfo info = infos[i];

                    Dictionary<int, int> usedParts = new Dictionary<int, int>();
                    for (int j = 0; j < info.PartIDs.Length; j++)
                    {
                        int partID = info.PartIDs[j];
                        if (!usedParts.ContainsKey(partID))
                        {
                            usedParts[partID] = info.PartCount[j];
                        }
                    }

                    SendUpdate(view, new ViewData()
                    {
                        ID = info.ID,
                        PlayerMoney = money,
                        Mode = info.Mode,
                        Price = info.Price,
                        ResultID = info.ResultID, // Currrently unused
                        usedParts = usedParts
                    });
                }
            }
        }

        [MessagePackObject(false)]
        public class ViewData : IViewData, IViewResponseData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int ID;

            [Key(1)] public int PlayerMoney;

            [Key(2)] public CApplianceInfo.ApplianceInfoMode Mode;

            [Key(3)] public int Price;

            [Key(4)] public int ResultID;

            [Key(5)] public Dictionary<int, int> usedParts;

            public bool IsChangedFrom(ViewData check)
            {
                return ID != check.ID ||
                    PlayerMoney != check.PlayerMoney ||
                    Mode != check.Mode ||
                    Price != check.Price ||
                    ResultID != check.ResultID ||
                    UsedPartsIsChangedFrom(check);
            }

            private bool UsedPartsIsChangedFrom(ViewData check)
            {
                if (usedParts.Count != check.usedParts.Count)
                    return true;
                foreach (KeyValuePair<int, int> parts in usedParts)
                {
                    if (!check.usedParts.TryGetValue(parts.Key, out int count))
                        return true;
                    if (parts.Value != count)
                        return true;
                }
                return false;
            }
        }

        protected override void UpdateData(ViewData data)
        {
            GameObject sections = new GameObject();
            sections.transform.parent = Sections.transform.parent;
            sections.transform.localPosition = Sections.transform.localPosition;
            sections.transform.localRotation = Sections.transform.localRotation;
            sections.transform.localScale = Sections.transform.localScale;

            UnityEngine.Object.Destroy(Sections);
            Sections = sections;
            if (!GameData.Main.TryGet(data.ID, out Appliance gdo))
            {
                Title.text = $"Partial Appliance ({data.ID})";
                Description.text = "Oopsie! CraftStation GDO not found.";
                return;
            }
            float yPos = SectionStartOffset;
            Title.text = gdo.Name;
            Description.text = gdo.Description;
            foreach (IApplianceProperty property in gdo.Properties)
            {
                IApplianceProperty applianceProperty = property;
                IApplianceProperty applianceProperty2 = applianceProperty;
                if (!(applianceProperty2 is CGivesDecoration cGivesDecoration))
                {
                    if (applianceProperty2 is CHighlyFlammable)
                    {
                        yPos += AddTag(yPos, base.Localisation["CHighlyFlammable"]);
                    }
                }
                else
                {
                    yPos += AddDecorationInfo(yPos, cGivesDecoration.DecorationValues, gdo.EffectRange);
                }
            }
            for (int i = 0; i < gdo.Tags.Count; i++)
            {
                yPos += AddTag(yPos, gdo.Tags[i]);
            }
            for (int i = 0; i < gdo.Sections.Count; i++)
            {
                yPos += AddSection(yPos, gdo.Sections[i]);
            }
            if (data.usedParts.Count > 0)
            {
                List<string> partStrings = new List<string>();
                foreach (KeyValuePair<int, int> part in data.usedParts)
                {
                    string partName = $"{part.Key}"; // Part ID, as default value if gdo cannot be found
                    if (GameData.Main.TryGet<AppliancePart>(part.Key, out AppliancePart partGDO))
                    {
                        partName = partGDO.Name;
                    }
                    partStrings.Add($"{partName} ({(part.Value < 0 ? "∞" : part.Value)})");
                }
                string partsUsedString = String.Join(", ", partStrings);
                yPos += AddSection(yPos, new Appliance.Section()
                {
                    Title = "Parts",       // To populate GlobalLocalisation.Text
                    Description = partsUsedString
                });
            }
            else
            {
                string usedPartsTag = "No Parts";     // To populate GlobalLocalisation.Text
                yPos += AddTag(yPos, usedPartsTag);
            }
            if (gdo.HasUpgrades)
            {
                yPos += AddTag(yPos, base.Localisation["Upgradable"]);
            }
            if (data.Mode == CApplianceInfo.ApplianceInfoMode.Shop)
            {
                PriceTag.SetActive(value: true);
                PriceTag.transform.localPosition = new Vector3(0.8f, yPos + 0.21f, 0f);
                Price.text = $"{data.Price}";
                yPos += -0.3f;
                Price.color = ((data.PlayerMoney >= data.Price) ? Affordable : Unaffordable);
            }
            else
            {
                PriceTag.SetActive(value: false);
            }
            float totalHeight = yPos - SectionStartOffset;
            Vector3 localScale = Backing.transform.localScale;
            localScale.z = TopTextHeight - totalHeight;
            Backing.transform.localScale = localScale;
            Animator?.Update(0f);
        }
    }
}
