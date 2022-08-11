using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{

    public interface IFontSetter
    {
        public bool GetIsSetFontWithFontManager();
        public eFontType GetFontType();
        public void SetFontEditor();

    }
    /// <summary>
    /// ��Ʈ ���� - �߻� Ŭ����
    /// </summary>
    public abstract class FontSetter<T> : MonoBehaviour, IFontSetter
    {
        //��뿩��
        [SerializeField] protected bool _isSetFontWithFontManager = true;
        //public bool IsSetFontWithFontManager => _isSetFontWithFontManager;

        //��Ʈ Ÿ��
        [SerializeField] protected eFontType _fontType;
        //public eFontType FontType => _fontType;

        //�ؽ�Ʈ �ʵ�
        [SerializeField] protected  T _textField;
        public T TextField
        {
            get => _textField;
            set => _textField = value;
        }


        #region Unity call

        protected void Awake()
        {
            if (_textField == null)
            {
                _textField = GetComponent<T>();
            }
        }

        protected void Start()
        {

            if (_isSetFontWithFontManager)
            {
                SetFont(_fontType);
            }
        }
        #endregion





        public bool GetIsSetFontWithFontManager() => _isSetFontWithFontManager;
        public eFontType GetFontType() => _fontType;

       
        public abstract void SetFontEditor();
        public abstract void SetFont(eFontType fontType);

        
    }
}
