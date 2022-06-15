using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui.Unit
{
    public class UiUnitSelectView : AUiView
    {
        [SerializeField] private TMP_Text unitName;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider expSlider;

        public Image AvatarImage => avatarImage;
        public TMP_Text LevelText => levelText;
        public Slider HealthSlider => healthSlider;
        public Slider ExpSlider => expSlider;
        public TMP_Text UnitName => unitName;
    }
}