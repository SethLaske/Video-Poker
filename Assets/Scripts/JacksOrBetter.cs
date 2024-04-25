using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Differentiates all hands found in Jacks or Better
    /// 

    [CreateAssetMenu(fileName = "JacksOrBetter", menuName = "GameRules/JacksOrBetter")]
    public class JacksOrBetter : GameRules
    {
        [SerializeField] private Hand royalFlush;
        [SerializeField] private Hand straightFlush;
        [SerializeField] private Hand quads;
        [SerializeField] private Hand fullHouse;
        [SerializeField] private Hand flush;
        [SerializeField] private Hand straight;
        [SerializeField] private Hand trips;
        [SerializeField] private Hand twoPair;
        [SerializeField] private Hand jackOrBetter;

        [SerializeField] private Hand nothing;


        private Card[] cards;
        public override Hand GetHandRank(Card[] newCards)
        {
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
                    return royalFlush;
                }

                return straightFlush;
            }

            if (isFlush)
            {
                return flush;
            }

            if (isStraight)
            {
                return straight;
            }

            Dictionary<int, int> valueCounts = GetValueCounts();

            if (valueCounts.Count >= 5)
            {
                //5 unique cards that form neither straight nor flush results in nothing
                return nothing;
            }

            if (valueCounts.Count == 2)
            {
                if (valueCounts.ContainsValue(4))
                {
                    return quads;
                }
                return fullHouse;
            }

            if (valueCounts.Count == 3)
            {
                if (valueCounts.ContainsValue(3))
                {
                    return trips;
                }
                return twoPair;
            }

            for (int i = 10; i <= 13; i++)
            {
                int value = i % 13;
                if (valueCounts.ContainsKey(value) && valueCounts[value] > 1)
                {
                    return jackOrBetter;
                }
            }

            return nothing;
        }

        public override Hand[] GetAvailableHands()
        {
            Hand[] hands = new Hand[9];

            hands[0] = royalFlush;
            hands[1] = straightFlush;
            hands[2] = quads;
            hands[3] = fullHouse;
            hands[4] = flush;
            hands[5] = straight;
            hands[6] = trips;
            hands[7] = twoPair;
            hands[8] = jackOrBetter;

            return hands;
        }

        //ChatGPT assisted with this sorting syntax
        private Card[] SortCardsByValue(Card[] unsortedCards)
        {
            return unsortedCards.OrderBy(card => card.value).ToArray();
        }

        private bool IsFlush()
        {
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

        private bool IsStraight()
        {
            int firstValue = cards[0].value;

            if (firstValue == 0 && cards[1].value == 9)
            {                                                   //Aces can only be in a straight Ace-5 or 10-Ace. 
                firstValue = 8;                                 //If the first value is an ace and the second a 10
            }                                                   //then the Ace will be temporarily counted as a 9 to check for a straight

            for (int i = 1; i < cards.Length; i++)
            {
                if (firstValue + i != cards[i].value)
                {
                    return false;
                }
            }

            return true;
        }

        private Dictionary<int, int> GetValueCounts()
        {
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
}
