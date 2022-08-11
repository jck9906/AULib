using UnityEngine.UI;



/// <summary>
/// UnityEngine.UI.Text ó��
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