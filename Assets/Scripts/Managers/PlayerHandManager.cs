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
    public class PlayerHandManager : Manager
    {
        [SerializeField] private CardUI[] cardUIs = new CardUI[5];
        [SerializeField] private PlayerCard[] playerCards;

        [SerializeField] private float timeToFlipCard;

        protected override void Initialize()
        {
            base.Initialize();

            playerCards = new PlayerCard[cardUIs.Length];
            for (int i = 0; i < cardUIs.Length; i++)
            {
                int index = i;
                cardUIs[i].cardButton.onClick.AddListener(() => ToggleCard(index));

                PlayerCard newPlayerCard = new PlayerCard(cardUIs[i]);
                playerCards[i] = newPlayerCard;
            }

            ResetHand();
        }
        public void ResetHand()
        {
            Card defaultCard = GameManager.Instance.deckManager.GetDefaultCard();


            foreach (PlayerCard card in playerCards)
            {
                card.SetCard(defaultCard);
            }
        }

        public void NewHand()
        {
            for (int i = 0; i < playerCards.Length; i++)
            {
                playerCards[i].ClearCard();
                //SetCard(i, GameManager.Instance.deckManager.DrawCard());
            }

            StartCoroutine(DrawFirstHand());
        }

        IEnumerator DrawFirstHand() {

            yield return new WaitForSeconds(timeToFlipCard);
            for (int i = 0; i < playerCards.Length; i++)
            {
                SetCard(i, GameManager.Instance.deckManager.DrawCard());
                yield return new WaitForSeconds(timeToFlipCard);
            }

            GameManager.Instance.FirstHandDone();
        }

        public void DrawNewCards()
        {
            for (int i = 0; i < playerCards.Length; i++)
            {
                if (playerCards[i].onHold == false)
                {
                    playerCards[i].ClearCard();
                    //SetCard(i, GameManager.Instance.deckManager.DrawCard());
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
                    SetCard(i, GameManager.Instance.deckManager.DrawCard());
                    yield return new WaitForSeconds(timeToFlipCard);
                }
            }

            GameManager.Instance.EndGame();
        }

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
    /// Track all of each of the players 5 card slots
    /// 
    [Serializable]
    public class PlayerCard
    {

        public Card card { get; private set; }
        CardUI cardUI;
        public bool onHold = false;

        public PlayerCard(CardUI cardUI)
        {
            this.cardUI = cardUI;
        }

        public void ClearCard() {
            cardUI.SetCardImage(GameManager.Instance.deckManager.cardBackSprite);
        }
        public void SetCard(Card newCard)
        {
            card = newCard;
            onHold = false;

            cardUI.SetCardImage(card.sprite);
            cardUI.SetHold(false);

            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.cardDeal);
        }

        public void ToggleCard()
        {
            onHold = !onHold;
            cardUI.SetHold(onHold);

            GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.cardPress);
        }
    }
}
