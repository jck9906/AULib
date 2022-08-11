using UnityEngine.UI;



/// <summary>
/// UnityEngine.UI.Text Ã³¸®
/// </summary>
namespace AULib
{

    public class LocalizeUIText : LocalizeText<Text>
    {

        protected override void OnAfterStringChanged(string strValue)
        {
            _textField.text = strValue;
        }

        
    }
}