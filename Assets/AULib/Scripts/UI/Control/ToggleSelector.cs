using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class ToggleSelector : MonoBehaviour
    {
        [SerializeField]
        private Toggle[] toggles;

        public void SetOn( int index )
        {
            for ( int i = 0 ; i < toggles.Length ; i++ )
            {
                if ( i == index )
                {
                    if ( toggles[ i ].IsActive() )
                        toggles[ i ].isOn = true;
                }
                else
                {
                    if ( toggles[ i ].IsActive() )
                        toggles[ i ].isOn = false;
                }
            }
        }

        public void SetVisible( int index , bool visible )
        {
            if ( index >= toggles.Length )
                return;

            toggles[ index ].gameObject.SetActive( visible );
        }
    }
}