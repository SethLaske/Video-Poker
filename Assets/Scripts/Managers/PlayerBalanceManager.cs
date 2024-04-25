using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    public class PlayerBalanceManager : MonoBehaviour
    {
        [SerializeField] private float playerBalance = 20;

        [SerializeField] private float betSize = .25f;


        public float ChangeBalance(float change)
        {
            playerBalance += change;
            GameManager.Instance.uiManager.UpdatePlayerBalance(playerBalance);
            return playerBalance;
        }

        public float ChangeBet(float change) {
            if (GameManager.Instance.isGameActive) {
                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonReject);
                return betSize;
            }

            float newBetSize = betSize + change;

            if (newBetSize > 0 && newBetSize <= GameManager.Instance.gameRules.maxBetSize) { 
                betSize = newBetSize;
                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonReject);
            }

            return betSize;
        }
    }
}
