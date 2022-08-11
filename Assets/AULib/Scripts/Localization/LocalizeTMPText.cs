using TMPro;


/// <summary>
/// TextMeshPro ó��
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