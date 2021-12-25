using EditorToolket;
using UnityEditor;
using UnityEngine;

namespace TaggableTexts
{
    public class PatternPreviewEditor
    {
        enum PreviewType
        {
            Auto,
            Custom,
            TextAsset,
        }

        static PreviewType previewType;
        static string previewInputCustom;
        static TextAsset previewInputText;

        static GUIStyle wrapText { get => new GUIStyle() { wordWrap = true }; }

        public static void DrawPatternPreivew(TagPatternToken patternToken)
        {
            DrawPatternPreivew(new TagPatternToken[] { patternToken });
        }
        public static void DrawPatternPreivew(TagPatternToken[] patternTokens)
        {
            if (patternTokens.Length == 0) return;

            CommonEditor.DrawLayoutGroup("PattenToken Preview", "Helpbox", () =>
            {
                CommonEditor.EnumToolbar<PreviewType>(ref previewType);

                string input = GetPreviewInput(patternTokens);
                string output = ApplyPreviewTexts(input, patternTokens);

                DrawPreview(output);
            });
        }
        public static string GetPreviewInput(TagPatternToken[] patternTokens)
        {
            string input = "";

            CommonEditor.DrawLayoutGroup("Preview Input", "GroupBox", () =>
            {
                switch (previewType)
                {
                    case PreviewType.Auto:
                        for (int i = 0; i < patternTokens.Length; i++)
                        {
                            TagPatternToken patternToken = patternTokens[i];
                            if(patternToken != null && patternToken.isValidRule)
                            {
                                input += $"<{patternToken.keyword} preview input babababa/>";
                            }
                        }
                        break;

                    case PreviewType.Custom:
                        previewInputCustom = EditorGUILayout.TextField(previewInputCustom);
                        input = previewInputCustom;
                        break;

                    case PreviewType.TextAsset:
                        previewInputText = EditorGUILayout.ObjectField(previewInputText, typeof(TextAsset), false) as TextAsset;
                        if (previewInputText != null) input = previewInputText.text;
                        break;
                }

                GUILayout.Label(input, wrapText);
            });

            return input;
        }
        public static string ApplyPreviewTexts(string input, TagPatternToken[] patternTokens)
        {
            for (int i = 0; i < patternTokens.Length; i++)
            {
                TagPatternToken patternToken = patternTokens[i];
                if (patternToken != null && patternToken.isValidRule)
                {
                    input = patternToken.ApplyToString(input);
                }
            }

            return input;
        }
        public static void DrawPreview(string output)
        {
            GUIStyle style = new GUIStyle() { richText = true, wordWrap = true };

            CommonEditor.DrawLayoutGroup("Source Output", "GroupBox", () =>
            {
                EditorGUILayout.LabelField(output, EditorStyles.wordWrappedLabel);
            });
            CommonEditor.DrawLayoutGroup("Rich Output", "GroupBox", () =>
            {
                EditorGUILayout.LabelField(output, style);
            });

        }
    }
}
