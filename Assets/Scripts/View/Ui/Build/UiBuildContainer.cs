using UnityEngine;

namespace Rts.View.Ui.Build
{
    public class UiBuildContainer : AUiView
    {
        [SerializeField] private UiBuildView buildViewPrefab;

        public UiBuildView BuildViewPrefab => buildViewPrefab;
    }
}