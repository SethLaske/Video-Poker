using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VideoPoker;

public class PayTable : MonoBehaviour
{
    [SerializeField] private int royalFlushPayout = 800;
    [SerializeField] private int straightFlushPayout = 50;
    [SerializeField] private int quadsPayout = 25;
    [SerializeField] private int fullHousePayout = 9;
    [SerializeField] private int flushPayout = 6;
    [SerializeField] private int straightPayout = 4;
    [SerializeField] private int tripsPayout = 3;
    [SerializeField] private int twoPairPayout = 2;
    [SerializeField] private int jackOrBetterPayout = 1;


    private Card[] cards;
    public int GetPayout(Card[] newCards) {
        cards = SortCardsByValue(newCards);

        /*for (int i = 0; i < cards.Length; i++) {
            Debug.Log($"Card {(i+1)} is the {cards[i].value} of {cards[i].suit}");
        }*/

        bool isFlush = IsFlush();

        bool isStraight = IsStraight();

        if (isFlush && isStraight)
        {
            //If its a valid straight, then checking for the ace and king is enough to ensure its royal
            if (cards[0].value == 0 && cards[4].value == 12)
            {
                return royalFlushPayout;
            }

            return straightFlushPayout;
        }

        if (isFlush)
        {
            return flushPayout;
        }

        if (isStraight)
        {
            return straightPayout;
        }

        Dictionary<int, int> valueCounts = GetValueCounts();

        if (valueCounts.Count >= 5) {
            //5 unique cards that form neither straight nor flush results in nothing
            return 0;
        }

        if (valueCounts.Count == 2) {
            if (valueCounts.ContainsValue(4)) { 
                return quadsPayout;
            }
            return fullHousePayout;
        }

        if (valueCounts.Count == 3) {
            if (valueCounts.ContainsValue(3))
            {
                return tripsPayout;
            }
            return twoPairPayout;
        }

        for (int i = 10; i <= 13; i++) {
            int value = i % 13;
            if (valueCounts.ContainsKey(value) && valueCounts[value] > 1) { 
                return jackOrBetterPayout;
            }
        }

        return 0;
    }

    //ChatGPT
    private Card[] SortCardsByValue(Card[] unsortedCards)
    {
        return unsortedCards.OrderBy(card => card.value).ToArray();
    }

    private bool IsFlush() {
        int firstSuit = cards[0].suit;
        foreach (Card card in cards)
        {
            if (card.suit != firstSuit)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsStraight() {
        int firstValue = cards[0].value;

        if (firstValue == 0 && cards[1].value == 9) {       //Aces can only be in a straight Ace-5 or 10-Ace. 
            firstValue = 8;                                 //If the first value is an ace and the second a 10
        }                                                   //then the Ace will be temporarily counted as a 9 to check for a regular straight

        for (int i = 1; i < cards.Length; i++)
        {
            if (firstValue + i != cards[i].value) { 
                return false;
            }
        }

        return true;
    }

    private Dictionary<int, int> GetValueCounts() {
        Dictionary<int, int> valueCounts = new Dictionary<int, int>();
        foreach (Card card in cards)
        {
            if (valueCounts.ContainsKey(card.value))
            {
                valueCounts[card.value]++;
            }
            else
            {
                valueCounts.Add(card.value, 1);
            }
        }

        return valueCounts;
    }
}
