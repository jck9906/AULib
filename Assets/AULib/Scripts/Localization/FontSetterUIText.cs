using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    /// <summary>
    /// 폰트 셋팅 - UGui 텍스트
    /// </summary>
    public class FontSetterUIText : FontSetter<Text>
    {


        //폰트 셋팅 구현
        public override void SetFont(eFontType fontType)
        {
            _textField.font = FontManagerUGui.i.GetFont(fontType);
        }

        public override void SetFontEditor()
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(_textField.font, "Set UGui font");
            var fontm = FontManagerUGui.Get();
            _textField.font = fontm.GetFont(_fontType);
#endif
        }

    }
}
