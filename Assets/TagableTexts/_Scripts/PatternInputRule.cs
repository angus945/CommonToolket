using System.Collections.Generic;
using UnityEngine;

namespace TaggableTexts
{
    [System.Serializable]
    public class PatternInputRule
    {
        public enum InputRuleType
        {
            Custom,
            AnyString,
            Unsigned_Integer,
            Unsigned_Float,
            Signed_Integer,
            Signed_Float,
        }
        public static readonly Dictionary<InputRuleType, string> InputRuleTables = new Dictionary<InputRuleType, string>()
        {
            [InputRuleType.AnyString]            = "(.+?)",
            [InputRuleType.Unsigned_Integer]     = "/[0-9]+/",
            [InputRuleType.Unsigned_Float]       = "/[.0-9]+/",
            [InputRuleType.Signed_Integer]       = "/[.0-9]+/",
            [InputRuleType.Signed_Float]         = "/[-.0-9]+/",
        };

        [SerializeField] InputRuleType ruleType;
        [SerializeField] string customPattern;

        public PatternInputRule()
        {
            ruleType = InputRuleType.AnyString;
            customPattern = "";
        }

        public override string ToString()
        {
            switch (ruleType)
            {
                case InputRuleType.Custom:
                    return customPattern;

                default:
                    return InputRuleTables[ruleType];
            }
        }
    }

}
