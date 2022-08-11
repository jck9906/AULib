using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    /// <summary>
    /// ��Ʈ ���� - UGui �ؽ�Ʈ
    /// </summary>
    public class FontSetterUIText : FontSetter<Text>
    {


        //��Ʈ ���� ����
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
