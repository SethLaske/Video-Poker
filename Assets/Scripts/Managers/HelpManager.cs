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
        [SerializeField] private HandPayoutView handPayoutViewPrefab;

        private List<HandPayoutView> displayObjects = new List<HandPayoutView>();

        public bool isHelpScreenOn { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();

            FillPayoutTable();

            isHelpScreenOn = false;
            payTable.gameObject.SetActive(false);
        }

        ///
        /// Creating displays for each possible hand and populating it with the hand
        /// 
        private void FillPayoutTable() { 
            Hand[] payoutHands = GameManager.Instance.gameRules.GetAvailableHands();

            HandPayoutView newView = null;
            foreach (Hand hand in payoutHands)
            {
                newView = Instantiate(handPayoutViewPrefab, payTable.transform);
                newView.PopulatePayoutView(hand);
                displayObjects.Add( newView );
            }
        }

        public void TogglePayoutTable() { 
            isHelpScreenOn = !isHelpScreenOn;
            payTable.gameObject.SetActive(isHelpScreenOn);
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Updating the table to reflect current payouts
        /// 

        public void UpdatePayoutTable() { 
            foreach (HandPayoutView hand in displayObjects)
            {
                hand.UpdatePayoutView();
            }
        }
    }
}
