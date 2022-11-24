using System.Collections.Generic;
using UnityEngine;

namespace ModdingLaboratory.Instance.Visual
{
    [System.Serializable]
    public class SpriteSheet
    {
        int width;
        int height;

        public Texture sheetTexture;

        public string defaultImage;
        public string defaultAnimation;
        public Dictionary<string, SpriteSheetImage> images;
        public Dictionary<string, SpriteSheetAnimation> animations;

        public Vector2Int textureSize { get => new Vector2Int(sheetTexture.width, sheetTexture.height); }
        public Vector2 spriteSize { get => textureSize / new Vector2(width, height); }
        public Vector2 spriteUV { get => Vector2.one / spriteSize; }

        public SpriteSheet(int width, int height, Texture sheetTexture, string defaultImage, string defaultAnim, Dictionary<string, SpriteSheetImage> images, Dictionary<string, SpriteSheetAnimation> animations)
        {
            this.width = width;
            this.height = height;
            this.sheetTexture = sheetTexture;

            this.defaultImage = defaultImage;
            this.defaultAnimation = defaultAnim;
            this.images = images;
            this.animations = animations;
        }
    }

    [System.Serializable]
    public class SpriteSheetAnimation
    {
        public string name;
        public int index;
        public int length;
        public bool loop;

        public float duration;

        public SpriteSheetAnimation(string name, int index, int length, bool loop, float duration)
        {
            this.name = name;
            this.index = index;
            this.length = length;
            this.loop = loop;
            this.duration = duration;
        }
    }

    [System.Serializable]
    public class SpriteSheetImage
    {
        public string name;
        public int x;
        public int y;

        public SpriteSheetImage(string name, int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }

}