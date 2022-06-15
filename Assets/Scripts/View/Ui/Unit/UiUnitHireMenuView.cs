using UnityEngine;

namespace Rts.View.Ui.Unit
{
    public class UiUnitHireMenuView : AUiView
    {
        [SerializeField] private UiHireListView hireList;
        [SerializeField] private UiHireQueueView queueView;

        public UiHireListView HireList => hireList;
        public UiHireQueueView QueueView => queueView;
    }
}