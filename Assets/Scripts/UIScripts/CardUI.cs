using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Maintaing the UI for each card on screen
    /// 
    public class CardUI : Branch
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private GameObject holdObject;
        public Button cardButton;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color holdColor;
        private ColorBlock colorBlock;

        protected override void Initialize()
        {
            base.Initialize();

            colorBlock = cardButton.colors;

            if (cardImage == null)
            {
                Debug.LogError("CardUI - cardImage - not assigned");
            }

            if (holdObject == null)
            {
                Debug.LogError("CardUI - holdObject - not assigned");
            }

            if (cardButton == null ) {
                Debug.LogError("CardUI - cardButton - not assigned");
            }
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Functions to change the appearance or hold of a card
        /// 
        public void SetCardImage(Sprite sprite)
        {
            cardImage.sprite = sprite;
        }

        public void SetHold(bool hold)
        {
            holdObject.SetActive(hold);
            if (hold)
            {
                colorBlock.normalColor = holdColor;
                colorBlock.selectedColor = holdColor;
            }
            else {
                colorBlock.normalColor = normalColor;
                colorBlock.selectedColor = normalColor;
            }
            cardButton.colors = colorBlock;
        }
    }
}
