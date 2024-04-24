using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker{ 
    public class HandDisplayObject : MonoBehaviour
    {
        [SerializeField] private Text handName;
        [SerializeField] private Text handMultiplier;
        public void PopulateUI(Hand hand) { 
            handName.text = hand.name;
            handMultiplier.text = hand.payout.ToString();
        }
    }

}