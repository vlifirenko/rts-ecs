using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui.Build
{
    public class UiBuildButtonView : AUiView
    {
        [SerializeField] public Button button;

        public Button Button => button;
    }
}