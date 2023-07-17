using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    // 
    public class WidgetSwitcher : BaseBehaviour
    {
        public int ActivateOnSetIndex;

        [SerializeField] private bool _autoOnAwake = true;
        [SerializeField] private int _index;
        [SerializeField] public GameObject[] _entities;

        public int Index { get => _index; }

        protected override void Awake()
        {
            base.Awake();

            if(_autoOnAwake)
                SetOn(ActivateOnSetIndex);

        }

        public GameObject SetOn( int index )
        {
            if ( _entities != null && _entities.Length <= index )
            {
                Debug.Log( $"invalid index:{index}" );
                return null;
            }

            _index = Mathf.Clamp( index , 0 , _entities.Length - 1 );

            for ( int i = 0 ; i < _entities.Length ; i++ )
            {
                if ( i == index )
                    _entities[ i ]?.SetActive( true );
                else
                    _entities[ i ]?.SetActive( false );
            }

            return _entities[index];
        }

        public void SetNext()
        {
            SetOn( _index + 1 );
        }

        public void SetPrev()
        {
            SetOn( _index - 1 );
        }
    }
}