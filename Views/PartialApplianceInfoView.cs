using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace CraftingLib.Views
{
    public class PartialApplianceInfoView : UpdatableObjectView<PartialApplianceInfoView.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper()
                    .All(typeof(CLinkedView), typeof(CPartialApplianceInfo)));
            }

            protected override void OnUpdate()
            {
                using NativeArray<CLinkedView> views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using NativeArray<CPartialApplianceInfo> infos = Views.ToComponentDataArray<CPartialApplianceInfo>(Allocator.Temp);

                int money = 0;
                if (Require(out SMoney sMoney))
                    money = sMoney.Amount;

                for (int i = 0; i < views.Length; i++)
                {
                    CLinkedView view = views[i];
                    CPartialApplianceInfo info = infos[i];

                    Dictionary<int, int> consumedParts = new Dictionary<int, int>();
                    for (int j = 0; j < info.PartIDs.Length; j++)
                    {
                        int partID = info.PartIDs[j];
                        if (!consumedParts.ContainsKey(partID))
                        {
                            consumedParts[partID] = info.PartCount[j];
                        }
                    }

                    SendUpdate(view, new ViewData()
                    {
                        ID = info.ID,
                        PlayerMoney = money,
                        Mode = info.Mode,
                        Price = info.Price,
                        RecipeIndex = info.RecipeIndex, // Currrently unused
                        ConsumedParts = consumedParts
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

            [Key(4)] public int RecipeIndex;

            [Key(5)] public Dictionary<int, int> ConsumedParts;

            public bool IsChangedFrom(ViewData check)
            {
                return ID != check.ID ||
                    PlayerMoney != check.PlayerMoney ||
                    Mode != check.Mode ||
                    Price != check.Price ||
                    RecipeIndex != check.RecipeIndex ||
                    ConsumedPartsIsChangedFrom(check);
            }

            private bool ConsumedPartsIsChangedFrom(ViewData check)
            {
                if (ConsumedParts.Count != check.ConsumedParts.Count)
                    return true;
                foreach (KeyValuePair<int, int> parts in ConsumedParts)
                {
                    if (!check.ConsumedParts.TryGetValue(parts.Key, out int count))
                        return true;
                    if (parts.Value != count)
                        return true;
                }
                return false;
            }
        }

        private const float TopTextHeight = 1.13f;
        private const float SectionStartOffset = 2.4f;
        private const float SectionHeight = -0.8f;
        private const float TagHeight = -0.5f;
        private readonly Color Affordable = new Color(0.7051f, 1f, 0f, 1f);
        private readonly Color Unaffordable = new Color(0.8113f, 0.1868f, 0.2777f, 1f);

        public TextMeshPro Title;
        public TextMeshPro Description;
        public GameObject Sections;
        public GameObject PriceTag;
        public TextMeshPro Price;
        public GameObject Backing;
        public GameObject TemplateTag;
        public GameObject TemplateInfo;
        public Animator Animator;

        protected override void UpdateData(ViewData data)
        {
            GameObject sections = new GameObject();
            sections.transform.parent = Sections.transform.parent;
            sections.transform.localPosition = Sections.transform.localPosition;
            sections.transform.localRotation = Sections.transform.localRotation;
            sections.transform.localScale = Sections.transform.localScale;

            UnityEngine.Object.Destroy(Sections);
            Sections = sections;
            if (!GameData.Main.TryGet(data.ID, out PartialAppliance gdo))
            {
                Title.text = $"Partial Appliance ({data.ID})";
                Description.text = "Oopsie! PartAttachmentPoint GDO not found.";
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
            if (data.ConsumedParts.Count > 0)
            {
                List<string> partStrings = new List<string>();
                foreach (KeyValuePair<int, int> part in data.ConsumedParts)
                {
                    string partName = $"{part.Key}"; // Part ID, as default value if gdo cannot be found
                    if (GameData.Main.TryGet<AppliancePart>(part.Key, out AppliancePart partGDO))
                    {
                        partName = partGDO.Name;
                    }
                    partStrings.Add($"{partName} ({part.Value})");
                }
                string partsInsertedString = String.Join(", ", partStrings);
                yPos += AddSection(yPos, new Appliance.Section()
                {
                    Title = "Parts Inserted",       // To populate GlobalLocalisation.Text
                    Description = partsInsertedString
                });
            }
            else
            {
                string consumedPartsTag = "No Parts Inserted";     // To populate GlobalLocalisation.Text
                yPos += AddTag(yPos, consumedPartsTag);
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

        private float AddDecorationInfo(float offset, DecorationValues values, IEffectRange range)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DecorationType[] types = DecorationValues.Types;
            foreach (DecorationType decorationType in types)
            {
                for (int j = 0; j < values[decorationType]; j++)
                {
                    stringBuilder.Append(GameData.Main.GlobalLocalisation.GetIcon(decorationType));
                    stringBuilder.Append(" ");
                }
            }
            return AddSection(offset, new Appliance.Section
            {
                Title = base.Localisation["ADDS_DECORATION"],
                Description = stringBuilder.ToString(),
                RangeDescription = ""
            }, centre: true);
        }

        private float AddTag(float offset, string tag)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(TemplateTag, Sections.transform, worldPositionStays: true);
            gameObject.SetActive(value: true);
            gameObject.transform.localPosition = new Vector3(0f, offset, 0f);
            gameObject.transform.Find("Text").GetComponent<TextMeshPro>().text = tag;
            return TagHeight;
        }

        private float AddSection(float offset, Appliance.Section details, bool centre = false)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(TemplateInfo, Sections.transform, worldPositionStays: true);
            gameObject.SetActive(value: true);
            gameObject.transform.localPosition = new Vector3(0f, offset, 0f);
            gameObject.transform.Find("Title").GetComponent<TextMeshPro>().text = details.Title;
            TextMeshPro component = gameObject.transform.Find("Description").GetComponent<TextMeshPro>();
            component.text = details.Description;
            component.alignment = (centre ? TextAlignmentOptions.Center : TextAlignmentOptions.Left);
            gameObject.transform.Find("Range").GetComponent<TextMeshPro>().text = details.RangeDescription;
            return SectionHeight;
        }
    }
}
