using UnityEngine;
using UnityEngine.Serialization;

namespace Rts.View.Ui.Character
{
    public class CanvasCharacterView : MonoBehaviour
    {
        [SerializeField] private UiCharactersView uiCharactersView;

        public UiCharactersView UiCharactersView => uiCharactersView;
    }
}