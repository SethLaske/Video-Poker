using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    [Serializable]
    public class Card
    {
        [SerializeField] public int suit;

        public int value;

        public Sprite sprite;


        /*public enum Suit
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
        }*/
    }
}