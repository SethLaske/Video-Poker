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

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Flush and Straights will be checked first, followed by set hands
        /// Set hands will be distinguished by the number of unique cards and their counts
        /// 
        public override Hand GetHandRank(Card[] playerHand)
        {
            if (playerHand.Length != 5) {   //All hands in Jacks or Better will be 5 cards
                return nothing;
            }

            cards = SortCardsByValue(playerHand);

            bool isFlush = IsFlush();

            bool isStraight = IsStraight();

            if (isFlush && isStraight)
            {
                //If already a straight, then checking for an ace and king is enough to ensure its royal
                if (cards[0].value == Card.Value.Ace && cards[4].value == Card.Value.King)
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

            if (valueCounts.Count >= 5)     //5 unique cards that form neither straight nor flush guarantees nothing
            {
                return nothing;
            }

            if (valueCounts.Count == 2)     //2 unique cards determines a quad or full house
            {
                if (valueCounts.ContainsValue(4))
                {
                    return quads;
                }
                return fullHouse;
            }

            if (valueCounts.Count == 3)     //3 unique cards determines a trip or two pair
            {
                if (valueCounts.ContainsValue(3))
                {
                    return trips;
                }
                return twoPair;
            }

            for (int i = (int) Card.Value.Jack; i <= 13; i++)      //Checking for any cards from jack to ace that have a count greater than 1
            {
                int value = i % 13;
                if (valueCounts.ContainsKey(value) && valueCounts[value] > 1)
                {
                    return jackOrBetter;
                }
            }

            return nothing;
        }

        //ChatGPT wrote this sorting syntax
        private Card[] SortCardsByValue(Card[] unsortedCards)
        {
            return unsortedCards.OrderBy(card => card.value).ToArray();
        }

        private bool IsFlush()
        {
            Card.Suit firstSuit = cards[0].suit;
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
            Card.Value firstValue = Card.Value.Ace;

            if (firstValue == Card.Value.Ace && cards[1].value == Card.Value.Ten)
            {                                                   //Aces can only be in a straight Ace-5 or 10-Ace. 
                firstValue = Card.Value.Nine;                     //If the first value is an ace and the second a 10
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
                if (valueCounts.ContainsKey((int)card.value))
                {
                    valueCounts[(int)card.value]++;
                }
                else
                {
                    valueCounts.Add((int)card.value, 1);
                }
            }

            return valueCounts;
        }



    }
}
