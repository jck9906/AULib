using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    // 
    public class WidgetSwitcher : MonoBehaviour
    {
        public int ActivateOnSetIndex;
        private int _index;

        [SerializeField] private GameObject[] _entities;

        private void Awake()
        {
            SetOn( ActivateOnSetIndex );
        }

        public void SetOn( int index )
        {
            if ( _entities != null && _entities.Length <= index )
            {
                Debug.Log( $"invalid index:{index}" );
                return;
            }

            _index = Mathf.Clamp( index , 0 , _entities.Length - 1 );

            for ( int i = 0 ; i < _entities.Length ; i++ )
            {
                if ( i == index )
                    _entities[ i ].SetActive( true );
                else
                    _entities[ i ].SetActive( false );
            }
        }

        public void SetNext()
        {
            SetOn( _index++ );
        }

        public void SetPrev()
        {
            SetOn( _index-- );
        }
    }
}