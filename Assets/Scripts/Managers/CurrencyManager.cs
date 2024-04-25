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
    public class CurrencyManager : Manager
    {
        private bool isCredits;

        [SerializeField] private float creditsPerDollar = 4;

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

        public void ToggleCurrency()
        {
            isCredits = !isCredits;
        }
    }
}
