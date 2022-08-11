using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class ToggleActivator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _toggleObjs = new List<GameObject>();

        private Toggle _toggle;

        private void Start()
        {
            _toggle = GetComponent<Toggle>();

            if ( _toggle == null )
            {
                Debug.Log( "[ALToggleActivator] Toggle is null" );
                return;
            }

            _toggle.onValueChanged.AddListener(
                ( bool on ) =>
                {
                    SetActivate( on );
                }
            );


            SetActivate( _toggle.isOn );
        }

        private void SetActivate( bool on )
        {
            foreach( var go in _toggleObjs )
                go.SetActive( on );
        }
    }
}