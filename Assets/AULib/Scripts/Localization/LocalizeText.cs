using UnityEngine;
using UnityEngine.Localization.Components;

namespace AULib
{

    public enum eStringTable
    {
        UI,
        System,
        Object
    }


    /// <summary>
    /// ���ö���� ���� Text �ڵ鷯    
    /// LocalizeStringEvent���� ���� Awake �Ǿ�� �ؼ�, 
    /// �� Ŭ������ ���� Ŭ������ Script excute order�� ��� �ؾ� ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LocalizeText<T> : BaseBehaviour
    {
        //�ؽ�Ʈ �ʵ�
        [SerializeField] protected T _textField;
        public T TextField
        {
            get => _textField;
            set => _textField = value;
        }

        [SerializeField] protected LocalizeStringEvent _localizedStringEvent;
        public LocalizeStringEvent LocalizedStringEvent
        {
            get => _localizedStringEvent;
            set => _localizedStringEvent = value;
        }

        //���̺� Ÿ��
        [SerializeField] protected eStringTable _tableType = eStringTable.UI;
        public eStringTable TableType
        {
            get => _tableType;
            set => _tableType = value;
        }

      

        //���̺� Ű �̸�
        [SerializeField] protected string _keyName;
        public string keyName
        {
            get => _keyName;
            set 
            {
                _keyName = value;
                OnSetKeyName();
            }  
        }


        #region Unity call

        protected override void Awake()
        {
            base.Awake();
            if (_textField == null)
            {
                _textField = GetComponent<T>();
            }

            if (_localizedStringEvent == null)
            {
                _localizedStringEvent = GetComponent<LocalizeStringEvent>();
            }
            

           

        }


        protected override void Start()
        {
            base.Start();
        }

        protected  void OnEnable()
        {
            _localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
        }

        protected  void OnDisable()
        {
            _localizedStringEvent.OnUpdateString.RemoveListener(OnStringChanged);
        }
        #endregion Unity call





        /// <summary>
        /// ���̺� �̸� 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string GetSwitchTableName(eStringTable table) => table switch
        {
            eStringTable.UI => "StringDataUI",
            eStringTable.System => "StringDataSys",
            eStringTable.Object => "StringDataObj",
            _ => "StringDataUI"
        };

        /// <summary>
        /// ���� ��ħ
        /// </summary>
        public void Refresh()
        {
            _localizedStringEvent.RefreshString();
        }





        #region private & protected
        protected void OnSetKeyName()
        {
            _localizedStringEvent.StringReference.SetReference(GetSwitchTableName(_tableType), _keyName);            
        }


        protected void OnStringChanged(string strValue)
        {
            //Changed key value
            OnAfterStringChanged(strValue);
        }








        protected abstract void OnAfterStringChanged(string strValue);
    }
    #endregion  private & protected

}
