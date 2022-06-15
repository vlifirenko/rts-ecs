using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui
{
    public class UiHoverView : AUiPopupView
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Slider healthSlider;

        public TMP_Text NameText => nameText;
        public Slider HealthSlider => healthSlider;
        public TMP_Text DescriptionText => descriptionText;
    }
}