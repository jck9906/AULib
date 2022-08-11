using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace AULib.Editor
{
    /// <summary>
    /// Unity Text를 TMPro Text로 변경
    /// </summary>
    public class TextToTMPMenuItem 
    {
        [MenuItem("CONTEXT/Text/Convert to TMP")]
        static void ConvertTextToTMP(MenuCommand command)
        {
            Text context = (Text)command.context;
            GameObject thisGameObject = context.gameObject;

            string contextText = context.text;
            int contextFontSize = context.fontSize;
            Color contextColor = context.color;
            TextAnchor contextAlignment = context.alignment;



            HorizontalWrapMode contextHorizontalOverflow = context.horizontalOverflow;
            VerticalWrapMode contextVerticalOverflow = context.verticalOverflow;


            Undo.DestroyObjectImmediate(context);

            TextMeshProUGUI textMeshProUGUI = Undo.AddComponent<TextMeshProUGUI>(thisGameObject );
            textMeshProUGUI.text = contextText;
            textMeshProUGUI.fontSize = contextFontSize;
            textMeshProUGUI.color = contextColor;
            textMeshProUGUI.alignment = GetTMPAlignmentOptions(contextAlignment);
            textMeshProUGUI.enableWordWrapping = (contextHorizontalOverflow == HorizontalWrapMode.Wrap) ? true : false;
            textMeshProUGUI.overflowMode = GetTMPOverflowModes(contextVerticalOverflow);            

            TextAlignmentOptions GetTMPAlignmentOptions(TextAnchor anchor) => anchor switch
            {
                TextAnchor.UpperLeft => TextAlignmentOptions.TopLeft,
                TextAnchor.UpperCenter => TextAlignmentOptions.Top,
                TextAnchor.UpperRight => TextAlignmentOptions.TopRight,
                TextAnchor.MiddleLeft => TextAlignmentOptions.Left,
                TextAnchor.MiddleCenter => TextAlignmentOptions.Center,
                TextAnchor.MiddleRight => TextAlignmentOptions.Right,
                TextAnchor.LowerLeft => TextAlignmentOptions.BottomLeft,
                TextAnchor.LowerCenter => TextAlignmentOptions.Bottom,
                TextAnchor.LowerRight => TextAlignmentOptions.BottomRight,
                _ => TextAlignmentOptions.Baseline
            };

            TextOverflowModes GetTMPOverflowModes(VerticalWrapMode mode) => mode switch
            {
                VerticalWrapMode.Overflow => TextOverflowModes.Overflow,
                VerticalWrapMode.Truncate => TextOverflowModes.Truncate,
                _ => TextOverflowModes.Overflow
            };

        }
    }
}
