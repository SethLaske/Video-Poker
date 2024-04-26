using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages the help systems, specifically displaying a current payout table to players
    /// 
    public class HelpManager : Branch
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

        ///
        /// Creating displays for each possible hand and populating it with the hand
        /// 
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

        public void TogglePayoutTable() { 
            payTable.gameObject.SetActive(!payTable.gameObject.activeInHierarchy);
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Updating the table to reflect current payouts
        /// 

        public void UpdatePayoutTable() { 
            foreach (HandDisplayObject hand in displayObjects)
            {
                hand.UpdateUI();
            }
        }
    }
}
