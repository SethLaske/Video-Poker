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
        public abstract Hand GetHandRank(Card[] newCards);
        public abstract Hand[] GetAvailableHands();

        public float maxBetSize;
        public float betIncrement;
    }

    //-//////////////////////////////////////////////////////////////////////
    ///

    [Serializable]
    public class Hand
    {
        public string name;
        public int payout;
        public string winningMessage;
        public UnityEvent winningEffect;
    }
}
