using UnityEngine;

namespace ModdingLab.Instance.Visual
{
    [System.Serializable]
    public class SpriteSheet
    {
        public string id;

        public int width;
        public int height;

        public Texture sheetTexture;

        public Vector2Int textureSize { get => new Vector2Int(sheetTexture.width, sheetTexture.height); }
        public Vector2 spriteSize { get => textureSize / new Vector2(width, height); }
        public Vector2 spriteUV { get => Vector2.one / spriteSize; }

        public SpriteSheet(string id, int width, int height, Texture sheetTexture)
        {
            this.id = id;
            this.width = width;
            this.height = height;
            this.sheetTexture = sheetTexture;
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
}