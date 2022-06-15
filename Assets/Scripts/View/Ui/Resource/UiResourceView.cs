using Rts.Models.Enums;
using TMPro;
using UnityEngine;

namespace Rts.View.Ui.Resource
{
    public class UiResourceView : AUiView
    {
        [SerializeField] private EResourceType resourceType;
        [SerializeField] private TMP_Text valueText;

        public EResourceType ResourceType => resourceType;
        public TMP_Text ValueText => valueText;
    }
}