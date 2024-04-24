using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private GameObject holdObject;


    public void SetCardImage(Sprite sprite) { 
        cardImage.sprite = sprite;
    }
    
    public void SetHold(bool hold) {
        holdObject.SetActive(hold);
    }
}