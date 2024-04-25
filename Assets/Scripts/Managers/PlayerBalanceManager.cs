using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages the players balance and bet sizes
    /// 
    public class PlayerBalanceManager : Manager
    {
        [SerializeField] private float playerBalance = 20;

        [SerializeField] private float betSize = .25f;

        protected override void Initialize()
        {
            base.Initialize();

            betSize = GameManager.Instance.gameRules.betIncrement;
        }

        public float GetBalance() { 
            return playerBalance;
        }

        public float GetBet() { 
            return betSize;
        }

        public float ChangeBalance(float change)
        {
            if (playerBalance + change < 0) {
                return -1f;
            }

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



            if (newBetSize > 0 && newBetSize <= GameManager.Instance.gameRules.maxBetSize && (newBetSize <= playerBalance || change < 0))
            {
                betSize = newBetSize;
            }
            else { 
                GameManager.Instance.audioManager.PlaySound(GameManager.Instance.audioManager.buttonReject);
            }

            return betSize;
        }
    }
}
