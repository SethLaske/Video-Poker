using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages the players actions and cards
    /// 
    public class PlayerHandController : Branch
    {
        [SerializeField] private CardView[] cardViews = new CardView[5];
        [SerializeField] private PlayerCard[] playerCards;

        [SerializeField] private float timeToFlipCard;

        public DeckController deckController;

        protected override void Initialize()
        {
            base.Initialize();

            if (deckController == null) Debug.LogError("DeckController not assigned to PlayerHandController");

            playerCards = new PlayerCard[cardViews.Length];
            for (int i = 0; i < cardViews.Length; i++)
            {
                int index = i;
                cardViews[i].cardButton.onClick.AddListener(() => ToggleCard(index));

                PlayerCard newPlayerCard = new PlayerCard(cardViews[i]);
                newPlayerCard.cardBack = deckController.cardBackSprite;
                playerCards[i] = newPlayerCard;
            }

            ResetHand();
        }

        public override void Tick(float delta) { 

            deckController.Tick(delta);

            foreach (CardView cardUI in cardViews) {
                cardUI.Tick(delta);
            }

            base.Tick(delta);   //The cardUIs must run their initializing functions before the PlayerHandManager

        }

        public void ResetHand()
        {
            Card defaultCard = deckController.GetDefaultCard();


            foreach (PlayerCard card in playerCards)
            {
                card.SetCard(defaultCard);
            }
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Starts the first hand sequence, drawing 5 new cards
        /// 
        public void NewHand()
        {
            deckController.ShuffleDeck();

            for (int i = 0; i < playerCards.Length; i++)
            {
                playerCards[i].ClearCard();
            }

            StartCoroutine(DrawFirstHand());
        }

        IEnumerator DrawFirstHand() {

            yield return new WaitForSeconds(timeToFlipCard);
            for (int i = 0; i < playerCards.Length; i++)
            {
                SetCard(i, deckController.DrawCard());
                yield return new WaitForSeconds(timeToFlipCard);
            }

            GameManager.Instance.FirstHandDone();
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Draws nonheld cards before triggering the end of the game
        /// 
        public void DrawNewCards()
        {
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i].onHold == false)
                {
                    playerCards[i].ClearCard();
                }
            }

            StartCoroutine(DrawSecondHand());
        }

        IEnumerator DrawSecondHand()
        {

            yield return new WaitForSeconds(timeToFlipCard);
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i].onHold == false)
                {
                    SetCard(i, deckController.DrawCard());
                    yield return new WaitForSeconds(timeToFlipCard);
                }
            }

            GameManager.Instance.EndGame();
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// 
        /// 

        private void SetCard(int index, Card newCard)
        {
            playerCards[index].SetCard(newCard);
        }

        private void ToggleCard(int index)
        {
            if (!GameManager.Instance.isGameActive) {
                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonReject);
                return;
            }

            if (index >= playerCards.Length || index < 0)
            {
                Debug.LogError("Toggling out of bounds: " + index);
                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonReject);
                return;
            }

            playerCards[index].ToggleCard();
        }

        public Card[] GetCurrentCardArray()
        {
            Card[] cards = new Card[playerCards.Length];
            for (int i = 0; i < playerCards.Length; i++)
            {
                cards[i] = playerCards[i].card;
            }

            return cards;
        }
    }

    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Controls one of card slots
    /// 
    [Serializable]
    public class PlayerCard
    {

        public Card card { get; private set; }
        CardView cardView;
        public bool onHold = false;

        public Sprite cardBack;

        public PlayerCard(CardView cardUI)
        {
            this.cardView = cardUI;
        }

        public void ClearCard() {
            cardView.SetCardImage(cardBack);
        }
        public void SetCard(Card newCard)
        {
            card = newCard;
            onHold = false;

            cardView.SetCardImage(card.sprite);
            cardView.SetHold(false);

            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.cardDeal);
        }

        public void ToggleCard()
        {
            onHold = !onHold;
            cardView.SetHold(onHold);

            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.cardPress);
        }
    }
}
