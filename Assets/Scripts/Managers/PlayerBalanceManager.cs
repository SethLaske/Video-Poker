using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    public class PlayerBalanceManager : MonoBehaviour
    {
        [SerializeField] private float playerBalance = 20;

        [SerializeField] public float currentBetSize = .25f;
        public float EditBalance(float change)
        {
            playerBalance += change;
            GameManager.Instance.uiManager.UpdatePlayerBalance(playerBalance);
            return playerBalance;
        }
    }
}
