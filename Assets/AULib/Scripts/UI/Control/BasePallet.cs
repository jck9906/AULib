using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace AULib
{
    public class BasePallet<T> : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] protected ScrollRect scrollRect;
        protected RectTransform contents;
        //[SerializeField] protected Toggle togglePrefab;
        [SerializeField] protected AssetReference toggleRef;
        protected ToggleGroup group;
        protected List<Toggle> listToggle;
        public int nInitToggleIndex = 0;

        protected Dictionary<int , T> _infoTable;

        public void SetToggleOn( int nIndex )
        {
            nInitToggleIndex = nIndex;
            if ( listToggle == null )
                return;
            for ( int i = 0 ; i < listToggle.Count ; i++ )
            {
                if ( i == nIndex )
                    listToggle[ i ].SetIsOnWithoutNotify( true );
                else if ( listToggle[ i ].isOn == true )
                    listToggle[ i ].SetIsOnWithoutNotify( false );
            }
        }

        public void RemoveAll()
        {

            Toggle[] allToggles = GetComponentsInChildren<Toggle>();
            for ( int i = 0 ; i < allToggles.Length ; i++ )
            {
                GameObject.Destroy( allToggles[ i ].gameObject );
            }

            listToggle?.Clear();
        }
    }
}