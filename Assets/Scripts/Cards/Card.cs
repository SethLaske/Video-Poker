using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class Card
    {
        //public Suit CardSuit;
        public int suit;

        //public Value CardValue;
        public int value;

        public Sprite sprite;


        public enum Suit
        {
            Club,
            Diamond,
            Heart,
            Spade,
        }
        public enum Value
        {
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
            King
        }
    }
}