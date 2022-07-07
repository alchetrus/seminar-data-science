using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class CharacterState : MonoBehaviour
    {
        public new Rigidbody2D rigidbody2D;
        public int horizontal = 0;
        public int vertical = 0;
        public bool isFacingRight = true;

        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}