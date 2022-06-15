using UnityEngine;
using UnityEngine.UI;

namespace Rts.View.Ui.Build
{
    public class UiBuildMenuView : AUiView
    {
        [SerializeField] private Button backButton;
        [SerializeField] private UiBuildItemView buildItemPrefab;

        public Button BackButton => backButton;
        public UiBuildItemView BuildItemPrefab => buildItemPrefab;
    }
}