using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{

    public class HandTests : MonoBehaviour
    {
        [SerializeField] private GameRules gameRules;

        [SerializeField] private List<TestPair> testPairs;

        [ContextMenu ("Run Tests")]
        public bool TestGameRuleHands() {
            bool result = true;

            foreach (var pair in testPairs)
            {
                result &= TestHand(pair.testedCards, pair.expectedResult);
            }

            if (result == true) {
                Debug.Log("<color=green> Test Completed Successfully </color>");
            }

            return result;
        }

        private bool TestHand(Card[] playerHand, string expectedHandName) {
            Hand result = gameRules.GetHandRank(playerHand);

            if (result.name == expectedHandName) {
                Debug.Log($"{expectedHandName} valid");
                return true;
            } else
            {
                string cards = "Players Hand: ";
                foreach (Card card in playerHand)
                {
                    cards += ($"{card.value} of {card.suit}, ");
                }

                Debug.Log($"<color=red>{expectedHandName} failed. The cards: {cards} resulted in a {result.name} </color>");
                
                return false;
            }

            //return false;
        }
    }

    [Serializable]
    public class TestPair {
        public Card[] testedCards;
        public string expectedResult;
    }
}
