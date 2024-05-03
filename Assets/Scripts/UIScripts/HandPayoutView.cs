using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Used for creating each entry in the payout table
    /// 
    public class HandPayoutView : MonoBehaviour
    {
        [SerializeField] private Text handName;
        [SerializeField] private Text handMultiplier;
        private Hand hand;
        public void PopulatePayoutView(Hand hand) { 
            this.hand = hand;
            handName.text = hand.name;
            UpdatePayoutView();
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Posting the current payout in the proper currency for each hand
        /// 
        public void UpdatePayoutView()
        {
            float currentPayout = hand.GetPayoutAmount();
            handMultiplier.text = GameManager.Instance.currencyManager.GetCurrencyString(currentPayout);
        }
    }

}