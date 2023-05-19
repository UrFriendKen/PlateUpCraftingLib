using CraftingLib.GameDataObjects;
using Kitchen;
using KitchenData;
using KitchenMods;
using MessagePack;
using System.Text;
using TMPro;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace CraftingLib.Views
{
    public class AppliancePartInfoView : InfoView<AppliancePartInfoView.ViewData>
    {
        public class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            EntityQuery Views;
            protected override void Initialise()
            {
                base.Initialise();
                Views = GetEntityQuery(new QueryHelper()
                    .All(typeof(CLinkedView), typeof(CAppliancePartVendorInfo)));
            }

            protected override void OnUpdate()
            {
                using NativeArray<CLinkedView> views = Views.ToComponentDataArray<CLinkedView>(Allocator.Temp);
                using NativeArray<CAppliancePartVendorInfo> infos = Views.ToComponentDataArray<CAppliancePartVendorInfo>(Allocator.Temp);

                int money = 0;
                if (Require(out SMoney sMoney))
                    money = sMoney.Amount;

                for (int i = 0; i < views.Length; i++)
                {
                    CLinkedView view = views[i];
                    CAppliancePartVendorInfo info = infos[i];

                    SendUpdate(view, new ViewData()
                    {
                        ID = info.ID,
                        PlayerMoney = money,
                        Mode = info.Mode,
                        Price = info.Price
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

            public bool IsChangedFrom(ViewData check)
            {
                return ID != check.ID ||
                    PlayerMoney != check.PlayerMoney ||
                    Mode != check.Mode ||
                    Price != check.Price;
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
            if (!GameData.Main.TryGet(data.ID, out AppliancePart gdo))
            {
                Title.text = $"Appliance Part ({data.ID})";
                Description.text = "Oopsie! AppliancePart GDO not found.";
                return;
            }
            float yPos = SectionStartOffset;
            Title.text = gdo.Name;
            Description.text = gdo.Description;
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
