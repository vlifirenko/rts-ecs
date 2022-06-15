using Rts.View.Ui.Build;
using Rts.View.Ui.Resource;
using Rts.View.Ui.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rts.View.Ui
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private UiUnitSelectView uiSelectView;
        [SerializeField] private UiHoverView uiHoverView;
        [SerializeField] private UiBuildButtonView uiBuildButtonView;
        [SerializeField] private UiBuildMenuView uiBuildMenuView;
        [SerializeField] private UiUnitHireMenuView unitHireMenuView;
        [SerializeField] private UiResourceView[] resources;

        public Canvas Canvas => canvas;
        public UiHoverView UiHoverView => uiHoverView;
        public UiUnitSelectView UISelectView => uiSelectView;
        public UiBuildButtonView UiBuildButtonView => uiBuildButtonView;
        public UiBuildMenuView UiBuildMenuView => uiBuildMenuView;
        public UiResourceView[] Resources => resources;
        public UiUnitHireMenuView UnitHireMenuView => unitHireMenuView;
    }
}