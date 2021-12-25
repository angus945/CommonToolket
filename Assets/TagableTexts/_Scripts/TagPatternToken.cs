using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TaggableTexts
{
    [CreateAssetMenu(fileName = "new TagPattern (Token)", menuName = "TagableTexts/PatternToken")]
    public class TagPatternToken : ScriptableObject
    {


        [SerializeField] string tagKeyword = "typing keyword";
        [SerializeField] PatternInputRule inputRule = new PatternInputRule();
        public string keyword { get => tagKeyword; }
        public string patternRegex { get => $"<{tagKeyword} {inputRule}/>"; }
        public bool isValidRule
        {
            get
            {
                if (string.IsNullOrEmpty(patternRegex)) return false;
                if (string.IsNullOrWhiteSpace(patternRegex)) return false;

                try
                {
                    Regex.Match("", patternRegex);
                }
                catch (ArgumentException)
                {
                    return false;
                }

                return true;
            }
        }

        [SerializeField] string replaceWord = null;
        public string ApplyToString(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            if (!isValidRule) return input;

            MatchCollection matchCollection = Regex.Matches(input, patternRegex);

            for (int i = 0; i < matchCollection.Count; i++)
            {
                string match = matchCollection[i].ToString();
                string tagInput = match.Replace($"<{tagKeyword} ", "").Replace("/>", "");

                string replace = replaceWord.Replace("<input>", tagInput);
                input = input.Replace(match, replace);
            }

            return input;
        }
    }

}
