using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public class UILineRendererAnimate : MonoBehaviour
    {
        public UILineRenderer[] lines;
        public float time = 1.0f;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }


        public void AnimateLines()
        {
            foreach( UILineRenderer line in lines )
            {
                AnimateLine( line );
            }
        }

        void AnimateLine( UILineRenderer line )
        {
            //List<Vector2> points = line.points.Clone();
            //Animate( line , points );
        }

        public void Animate( UILineRenderer line , List<Vector2> points )
        {
            line.points = new List<Vector2>();

            for( int i = 0 ; i<points.Count ; i++  )
            {
                int index = i;

            }
        }

        public void AnimatePoint( UILineRenderer line , int index , Vector2 start , Vector2 end )
        {
            //LeanTween.delayedCall( teim * index , () =>
            //{
            //    if( index > 0 )
            //    {
            //        start = line.points[ index - 1 ];
            //        line.points.Add( start );
            //    }
            //    else
            //    {
            //        line.points.Add( start );
            //    }

            //    LeanTween.value( gameObject , (ValueTuple) => 
            //    {
            //        line.points[index] = tweeningValue;
            //        line.SetVerticesDirty();
            //    } , start, end, time );
            //} );
        }
    }
}
