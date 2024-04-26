using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages shuffling and drawing cards from the deck
    /// 
    public class DeckManager : Branch
    {
        [Tooltip ("Sprites should be sorted by Suit then ordered Ace to King")]
        [SerializeField] private List<Sprite> cardSprites; 

        [SerializeField] private int normalCards = 52;
        [SerializeField] public Sprite cardBackSprite;

        private List<Card> availableCards = new List<Card>();
        private List<Card> drawnCards = new List<Card>();

        protected override void Initialize()
        {
            base.Initialize();

            availableCards.Clear();
            drawnCards.Clear();

            for (int i = 0; i < cardSprites.Count; i++)
            {

                Card newCard = new Card();

                if (i < normalCards)
                {
                    newCard.suit = i / 13;
                    newCard.value = i % 13;
                }
                else
                {
                    newCard.suit = -1;
                    newCard.value = -1;
                }

                newCard.sprite = cardSprites[i];

                availableCards.Add(newCard);
            }
        }

        public void ShuffleDeck() {
            availableCards.AddRange(drawnCards);

            drawnCards.Clear();
        }

        public Card DrawCard() {
            if (availableCards.Count <= 0) {
                Debug.LogError("Deck is empty");
                ShuffleDeck();
            }

            int index = Random.Range(0, availableCards.Count);
            Card drawnCard = availableCards[index];
            drawnCards.Add(drawnCard);
            availableCards.RemoveAt(index);

            return drawnCard;
        }

        public Card GetDefaultCard() { 
            Card card = new Card();
            card.suit = 0;
            card.value = 0;
            card.sprite = cardBackSprite;
            return card;
        }
    }
}
