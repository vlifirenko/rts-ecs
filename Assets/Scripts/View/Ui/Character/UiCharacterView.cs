using UnityEngine;

namespace Rts.View.Ui.Character
{
    public class UiCharacterView : AUiView
    {
        [SerializeField] private UiBuffView buffPrefab;
        [SerializeField] private Transform buffContainer;

        public UiBuffView BuffPrefab => buffPrefab;
        public Transform BuffContainer => buffContainer;
    }
}