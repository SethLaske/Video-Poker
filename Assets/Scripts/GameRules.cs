using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VideoPoker
{
    public abstract class GameRules : ScriptableObject
    {
        public abstract Hand GetHandRank(Card[] newCards);
        public abstract Hand[] GetAvailableHands();

        public float maxBetSize;
    }

    [Serializable]
    public class Hand
    {
        public string name;
        public int payout;
        public string winningMessage;
        public UnityEvent winningEffect;
    }
}
