using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui.Unit
{
    public class UiHireUnitItemView : AUiView
    {
        [SerializeField] private string id;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button button;
        
        public Button Button => button;
        public TMP_Text TitleText => titleText;
        public TMP_Text CostText => costText;

        public string Id
        {
            get => id;
            set => id = value;
        }
    }
}