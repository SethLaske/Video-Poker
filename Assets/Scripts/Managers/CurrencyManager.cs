using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VideoPoker;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Manages maintaining currency conversions, currently US $ and Credits
    /// 
    public class CurrencyManager : Branch
    {
        private bool isCredits;

        [SerializeField] private float creditsPerDollar = 4;

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Returns a formatted string in the current currency type
        /// 
        public string GetCurrencyString(float dollars)
        {
            if (isCredits)
            {
                float creditCount = dollars * creditsPerDollar;
                if (creditCount != 1)
                {
                    return creditCount + " Credits";
                }
                return creditCount + " Credit";
            }
            else
            {
                return "$" + dollars.ToString("F2");
            }
        }

        //-//////////////////////////////////////////////////////////////////////
        ///
        /// Switched currency type and triggers UI and Help managers to readjust all currencies
        /// 
        public void ToggleCurrency()
        {
            isCredits = !isCredits;

            GameManager.Instance.uiManager.UpdateCurrencyFields();
            GameManager.Instance.helpManager.UpdatePayoutTable();
        }
    }
}
