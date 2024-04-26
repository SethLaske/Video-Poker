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
    public class HandDisplayObject : MonoBehaviour
    {
        [SerializeField] private Text handName;
        [SerializeField] private Text handMultiplier;
        private Hand hand;
        public void PopulateUI(Hand hand) { 
            this.hand = hand;
            handName.text = hand.name;
            UpdateUI();
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Posting the current payout in the proper currency for each hand
        /// 
        public void UpdateUI()
        {
            float currentPayout = hand.GetPayoutAmount();
            handMultiplier.text = GameManager.Instance.currencyManager.GetCurrencyString(currentPayout);
        }
    }

}