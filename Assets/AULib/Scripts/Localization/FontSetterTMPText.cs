using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AULib
{
    /// <summary>
    /// ��Ʈ ���� - TMP �ؽ�Ʈ
    /// </summary>
    public class FontSetterTMPText : FontSetter<TextMeshProUGUI>
    {


        //��Ʈ ���� ����
        public override void SetFont(eFontType fontType)
        {
            _textField.font = FontManagerTMP.i.GetFont(fontType);
        }

        public override void SetFontEditor()
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(_textField, "Set TMP font");
            var fontm = FontManagerTMP.Get();
            _textField.font = fontm.GetFont(_fontType);
#endif
        }

    }
}
