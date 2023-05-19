using Kitchen;
using KitchenData;
using System.Text;
using TMPro;
using UnityEngine;

namespace CraftingLib.Views
{
    public abstract class InfoView<T> : UpdatableObjectView<T> where T : IViewData
    {
        protected const float TopTextHeight = 1.13f;
        protected const float SectionStartOffset = 2.4f;
        protected const float SectionHeight = -0.8f;
        protected const float TagHeight = -0.5f;
        protected readonly Color Affordable = new Color(0.7051f, 1f, 0f, 1f);
        protected readonly Color Unaffordable = new Color(0.8113f, 0.1868f, 0.2777f, 1f);

        public TextMeshPro Title;
        public TextMeshPro Description;
        public GameObject Sections;
        public GameObject PriceTag;
        public TextMeshPro Price;
        public GameObject Backing;
        public GameObject TemplateTag;
        public GameObject TemplateInfo;
        public Animator Animator;

        protected float AddDecorationInfo(float offset, DecorationValues values, IEffectRange range)
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

        protected float AddTag(float offset, string tag)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(TemplateTag, Sections.transform, worldPositionStays: true);
            gameObject.SetActive(value: true);
            gameObject.transform.localPosition = new Vector3(0f, offset, 0f);
            gameObject.transform.Find("Text").GetComponent<TextMeshPro>().text = tag;
            return TagHeight;
        }

        protected float AddSection(float offset, Appliance.Section details, bool centre = false)
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
