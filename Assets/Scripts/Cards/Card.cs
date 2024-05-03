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
        public Suit suit;

        public Value value;

        public Sprite sprite;

        public enum Value { 
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,

            Joker
        }

        public enum Suit { 
            Clubs,
            Diamonds,
            Hearts,
            Spades,

            Undefined
        }

    }
}