using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class HelpManager : Manager
    {
        [SerializeField] private GameObject payTable;
        [SerializeField] private HandDisplayObject handDisplayObjectPrefab;

        private List<HandDisplayObject> displayObjects = new List<HandDisplayObject>();
        protected override void Initialize()
        {
            base.Initialize();

            FillPayoutTable();
            payTable.gameObject.SetActive(false);
        }

        public void TogglePayoutTable() { 
            payTable.gameObject.SetActive(!payTable.gameObject.activeInHierarchy);
        }

        private void FillPayoutTable() { 
            Hand[] payoutHands = GameManager.Instance.gameRules.GetAvailableHands();

            HandDisplayObject newObject = null;
            foreach (Hand hand in payoutHands)
            {
                newObject = Instantiate(handDisplayObjectPrefab, payTable.transform);
                newObject.PopulateUI(hand);
                displayObjects.Add( newObject );
            }
        }

        public void UpdatePayoutTable() { 
            foreach (HandDisplayObject hand in displayObjects)
            {
                hand.UpdateUI();
            }
        }
    }
}
