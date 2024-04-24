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

    }
}