using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace AULib
{
    public class LocalizeMenuItem : EditorWindow
    {
        [MenuItem("CONTEXT/Text/Localize(AL)")]
        static void AddLocalizeUIText(MenuCommand command)
        {
            Text context = (Text)command.context;

            var stringEvent = AddLocalizeStringEvent(context.gameObject);

            var uiText = context.gameObject.AddComponent<LocalizeUIText>();
            uiText.TextField = context;
            uiText.LocalizedStringEvent = stringEvent;

            var fontSetter = context.gameObject.AddComponent<FontSetterUIText>();
            fontSetter.TextField = context;
        }

        [MenuItem("CONTEXT/TextMeshProUGUI/Localize(AL)")]
        static void AddLocalizeTMPro(MenuCommand command)
        {
            TextMeshProUGUI context = (TextMeshProUGUI)command.context;

            var stringEvent = AddLocalizeStringEvent(context.gameObject);

            var uiText = context.gameObject.AddComponent<LocalizeTMPText>();            
            uiText.TextField = context;
            uiText.LocalizedStringEvent = stringEvent;

            var fontSetter = context.gameObject.AddComponent<FontSetterTMPText>();
            fontSetter.TextField = context;
        }

        private static LocalizeStringEvent AddLocalizeStringEvent(GameObject go)
        {
            var stringEvent = Undo.AddComponent(go, typeof(LocalizeStringEvent)) as LocalizeStringEvent;

            //Localize 현재 버전에서의 문제 때문에 강제로 false 처리
            stringEvent.StringReference.WaitForCompletion = false;
            return stringEvent;
        }





    }

}

