using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui.Resource
{
    public class UiMiningView : AUiPopupView
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Slider slider;

        public TMP_Text Title => title;
        public Slider Slider => slider;
    }
}