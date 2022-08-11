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
    /// 로컬라이즈를 위한 Text 핸들러    
    /// LocalizeStringEvent보다 먼저 Awake 되어야 해서, 
    /// 이 클래스의 하위 클래스는 Script excute order에 등록 해야 함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LocalizeText<T> : BaseBehaviour
    {
        //텍스트 필드
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

        //테이블 타입
        [SerializeField] protected eStringTable _tableType = eStringTable.UI;
        public eStringTable TableType
        {
            get => _tableType;
            set => _tableType = value;
        }

      

        //테이블 키 이름
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
        /// 테이블 이름 
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
        /// 새로 고침
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
