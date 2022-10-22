using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModdingLab
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSheetAnimator : MonoBehaviour
    {
        public SpriteSheet spriteSheet;

        SpriteRenderer spriteRenderer;

        void Start()
        {
            //spriteRenderer.sprite = spriteSheet.texture;
        }
    }
}
