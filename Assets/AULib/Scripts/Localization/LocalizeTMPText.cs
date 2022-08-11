using TMPro;


/// <summary>
/// TextMeshPro Ã³¸®
/// </summary>
namespace AULib
{
    public class LocalizeTMPText : LocalizeText<TextMeshProUGUI>
    {
        protected override void OnAfterStringChanged(string strValue)
        {
            _textField.text = strValue;
        }
    }
}