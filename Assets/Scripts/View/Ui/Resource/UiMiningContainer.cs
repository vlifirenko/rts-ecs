using UnityEngine;

namespace Rts.View.Ui.Resource
{
    public class UiMiningContainer : AUiView
    {
        [SerializeField] private UiMiningView miningViewPrefab;

        public UiMiningView MiningViewPrefab => miningViewPrefab;
    }
}