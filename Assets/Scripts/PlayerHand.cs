using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoPoker;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private CardUI[] cardUIs = new CardUI[5];
    private PlayerCard[] playerCards;

    private void Start()
    {

        playerCards = new PlayerCard[cardUIs.Length];
        for (int i = 0; i < cardUIs.Length; i++)
        {
            int index = i;
            cardUIs[i].cardButton.onClick.AddListener(()  => ToggleCard(index));

            PlayerCard newPlayerCard = new PlayerCard(cardUIs[i]);
            playerCards[i] = newPlayerCard;
        }

        ResetHand();
    }
    public void ResetHand()
    {
        Card defaultCard = GameManager.Instance.deckManager.GetDefaultCard();
        
        //Debug.Log("Card UIs Length: " + cardUIs.Length);
        //Debug.Log("Player Cards Length: " + playerCards.Length);

        foreach (PlayerCard card in playerCards) {
            card.SetCard(defaultCard);
        }
    }

    public void NewHand() {
        for (int i = 0; i < playerCards.Length; i ++) {
            SetCard(i, GameManager.Instance.deckManager.DrawCard());
        }
    }

    public void DrawNewCards() {
        for (int i = 0; i < playerCards.Length; i++) {
            if (playerCards[i].onHold == false) {
                SetCard(i, GameManager.Instance.deckManager.DrawCard());
            }
        }
    }

    public void SetCard(int index, Card newCard) {
        playerCards[index].SetCard(newCard);
    }

    public void ToggleCard(int index) { 
        if (index >= playerCards.Length || index < 0) {
            Debug.LogError("Toggling out of bounds: " + index);
            return;
        }

        playerCards[index].ToggleCard();
    }
}

public class PlayerCard {
    Card card;
    CardUI cardUI;
    public bool onHold = false;

    public PlayerCard(CardUI cardUI) { 
        this.cardUI = cardUI;


    }

    public void SetCard(Card newCard) { 
        card = newCard;
        onHold = false;

        cardUI.SetCardImage(card.sprite);
        cardUI.SetHold(false);
    }

    public void ToggleCard() { 
        onHold = !onHold;
        cardUI.SetHold(onHold);
    }
}
