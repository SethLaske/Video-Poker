using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private GameObject holdObject;
        public Button cardButton;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color holdColor;
        private ColorBlock colorBlock;

        private void Awake()
        {
            colorBlock = cardButton.colors;
        }

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
