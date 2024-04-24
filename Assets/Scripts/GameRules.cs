using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VideoPoker;

public abstract class GameRules : MonoBehaviour
{
    public abstract Hand GetHandRank(Card[] newCards);
}

[Serializable]
public class Hand {
    public string name;
    public int payout;
    public string winningMessage;
    public UnityEvent winningEffect;
}
