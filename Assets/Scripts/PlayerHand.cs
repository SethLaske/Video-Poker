using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoPoker;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private CardUI[] cardUIs = new CardUI[5];
    [SerializeField] private Card[] cards = new Card[5];


    public void ResetHand()
    {
        Sprite defaultSprite = GameManager.Instance.deckManager.GetDefaultCard().sprite;
        foreach (CardUI card in cardUIs) {
            card.SetHold(false);
            card.SetCardImage(defaultSprite);
        }
    }

    public void NewHand() {
        for (int i = 0; i < cards.Length; i ++) {
            SetCard(i, GameManager.Instance.deckManager.DrawCard());
        }
    }

    public void SetCard(int index, Card newCard) {
        cards[index] = newCard;
        cardUIs[index].SetCardImage(newCard.sprite);
    }
}
