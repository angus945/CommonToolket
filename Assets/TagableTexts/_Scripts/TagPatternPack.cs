using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaggableTexts
{
    [CreateAssetMenu(fileName = "new TagPatternPack", menuName = "TagableTexts/PatternPack")]
    public class TagPatternPack : ScriptableObject
    {
        [SerializeField] TagPatternToken[] patternTokens = null;

    }
}
