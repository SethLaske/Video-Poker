using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class HelpManager : MonoBehaviour
    {
        [SerializeField] private GameObject payTable;
        [SerializeField] private HandDisplayObject handDisplayObjectPrefab;
        void Start()
        {
            FillPayoutTable();
            payTable.gameObject.SetActive(false);
        }

        public void TogglePayoutTable() { 
            payTable.gameObject.SetActive(!payTable.gameObject.activeInHierarchy);
        }

        private void FillPayoutTable() { 
            Hand[] payoutHands = GameManager.Instance.pokerHands.GetAvailableHands();

            HandDisplayObject newObject = null;
            foreach (Hand hand in payoutHands)
            {
                newObject = Instantiate(handDisplayObjectPrefab, payTable.transform);
                newObject.PopulateUI(hand);
            }
        }
    }
}
