using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class ToggleActivator : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private List<GameObject> _toggleObjs = new List<GameObject>();

        private void Awake()
        {
            _toggle.onValueChanged.AddListener( HandleOnValueChanged );
        }

        private void Start()
        {
            //_toggle = GetComponent<Toggle>();

            //if ( _toggle == null )
            //{
            //    Debug.Log( "[ALToggleActivator] Toggle is null" );
            //    return;
            //}

            //_toggle.onValueChanged.AddListener(
            //    ( bool on ) =>
            //    {
            //        SetActivate( on );
            //    }
            //);


            HandleOnValueChanged( _toggle.isOn );
        }

        private void HandleOnValueChanged( bool on )
        {
            foreach( var go in _toggleObjs )
                go.SetActive( on );
        }

        public void AddGameObject( GameObject go )
        {
            _toggleObjs.Add( go );
        }

        public void DelGameObject( GameObject go )
        {
            _toggleObjs.Remove( go );
        }
    }
}