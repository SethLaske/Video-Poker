using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Track the rules for finding poker hands, along with their names and payouts
    /// Scriptable objects are used to allow mutliple variations to be easily created from the same hands
    /// 
    public abstract class GameRules : ScriptableObject
    {
        public abstract Hand GetHandRank(Card[] playerHand);
        public abstract Hand[] GetAvailableHands();

        public float maxBetSize;
        public float betIncrement;
    }

    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Organize the various hands a player
    ///

    [Serializable]
    public class Hand
    {
        public string name;
        public int payoutMultiplier;
        public string winningMessage;

        [SerializeField] private List<PayoutThreshold> customPayouts = new List<PayoutThreshold>();

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Get the amount a player will win from the hand with their current bet size
        ///
        public float GetPayoutAmount()
        {
            float betAmount = GameManager.Instance.playerBalanceManager.GetBet();
            if (customPayouts.Count > 0) {

                foreach (var payout in customPayouts)
                {
                    if (betAmount >= payout.lowerLimit) { 
                        return payout.newMultiplier * betAmount;
                    }
                }
            }

            return payoutMultiplier * betAmount;
        }


    }

    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Custom thresholds to allow for different player multipliers based on player bet size
    ///
    [Serializable]
    public struct PayoutThreshold {
        [Tooltip ("In dollars")] 
        public float lowerLimit;

        public float newMultiplier;
    }
}
