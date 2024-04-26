using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Store the attributes of each card
    /// 

    [Serializable]
    public class Card
    {
        public int suit;

        public int value;

        public Sprite sprite;

    }
}