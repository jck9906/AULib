using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace AULib
{
    public class UIShapeRenderer : Graphic
    {

        [SerializeField] private UIShapeRenderer _parentShapeRenderer;
        [ Min( 3 )]
        public int Sides = 3;
        public int Radius = 100;
        public float RotateAngle = 90;

        protected override void OnPopulateMesh( VertexHelper vh )
        {
            vh.Clear();

            if ( _parentShapeRenderer )
            {
                if ( _parentShapeRenderer.Sides != Sides
                    || _parentShapeRenderer.Radius != Radius
                    || _parentShapeRenderer.RotateAngle != RotateAngle )
                {
                    Sides = _parentShapeRenderer.Sides;
                    Radius = _parentShapeRenderer.Radius;
                    RotateAngle = _parentShapeRenderer.RotateAngle;
                }
            }

            if ( sideValueList.Count != Sides )
            {
                sideValueList.Clear();
                for ( int i = 0 ; i < Sides ; i++ )
                    sideValueList.Add( 1 );
            }


            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            float cx = width / 2;
            float cy = height / 2;

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            // vertex.position = new Vector3( cx , cy );
            vertex.position = new Vector3( 0 , 0 );
            vh.AddVert( vertex );

            Vector3[] points = GetCircumferencePoints( Sides , Radius );
            foreach ( var point in points )
            {
                vertex.position = point;// + new Vector3( cx , cy );
                vh.AddVert( vertex );
            }

            for ( int i = 0 ; i < Sides - 1 ; i++ )
                vh.AddTriangle( 0 , i + 2 , i + 1 );
            vh.AddTriangle( 0 , 1 , Sides );
        }



        private Vector3[] GetCircumferencePoints( int sides , float radius = 1 )
        {
            Vector3[] points = new Vector3[ sides ];
            float anglePerStep = 2 * Mathf.PI * ( ( float ) 1 / sides );
            float offset = Mathf.Deg2Rad * RotateAngle;
           
            for ( int i = 0 ; i < sides ; i++ )
            {
                Vector2 point = Vector2.zero;
                float angle = anglePerStep * i;
                float ratio = sideValueList[ i ];

                point.x = Mathf.Cos( angle + offset ) * ( radius * ratio );
                point.y = Mathf.Sin( angle + offset ) * ( radius * ratio );

                points[ i ] = point;
            }
            
            return points;
        }


        public int MaxValue = 100;
        public List<float> sideValueList = new();
        public void InitRadar( int maxValue )
        {
            MaxValue = maxValue;
            //for ( int i = 0 ; i < Sides ; i++ )
            //    sideValueList.Add( initValue );
        }

        public void SetSide( int sideIndex , int sideValue  , bool dirty = true )
        {
            sideValueList[ sideIndex ] = ( ( float ) ( sideValue ) / MaxValue );
            Debug.Log( $"SetSide = {sideValueList[ sideIndex ]}" );
            if( dirty )
                SetVerticesDirty();
        }

        private void Update()
        {
            if ( _parentShapeRenderer )
            {
                if ( _parentShapeRenderer.Sides != Sides
                    || _parentShapeRenderer.Radius != Radius
                    || _parentShapeRenderer.RotateAngle != RotateAngle )
                {
                    Sides = _parentShapeRenderer.Sides;
                    Radius = _parentShapeRenderer.Radius;
                    RotateAngle = _parentShapeRenderer.RotateAngle;

                    SetVerticesDirty();
                }
            }
        }
    }


    public class TestShapeRenderer
    {
        //[Range( 3 , 36 )]
        //[SerializeField] private int _polygonPoints = 3;

        //[Min( 0.1f )]
        //[SerializeField] private float _outerRadius = 3;

        //[Min( 0 )]
        //[SerializeField] private float _innnerRadius = 3;

        //[Min( 1 )]
        //[SerializeField] int _repeatCount;

        //private Vector3[] _vertices;
        //private int[] _indices;
        //private Vector2[] _uvs;


        //private int[] DrawFilledIndices( Vector3[] vertices )
        //{
        //    int triangleCount = vertices.Length - 2;
        //    List<int> indices = new List<int>();

        //    for ( int i = 0 ; i < triangleCount ; ++i )
        //    {
        //        indices.Add( 0 );
        //        indices.Add( i + 2 );
        //        indices.Add( i + 1 );
        //    }

        //    return indices.ToArray();
        //}

        //private int[] DrawHollowIndices( int sides )
        //{
        //    List<int> indices = new List<int>();

        //    for ( int i = 0 ; i < sides ; ++i )
        //    {
        //        int outerIndex = i;
        //        int innerIndex = i + sides;

        //        indices.Add( outerIndex );
        //        indices.Add( innerIndex );
        //        indices.Add( ( outerIndex + 1 ) % sides );

        //        indices.Add( innerIndex );
        //        indices.Add( sides + ( ( innerIndex + 1 ) % sides ) );
        //        indices.Add( ( outerIndex + 1 ) % sides );
        //    }

        //    return indices.ToArray();
        //}



        //private void DrawHollow( int sides , float outer , float inner )
        //{
        //    Vector3[] outerPoints = GetCircumferencePoints( sides , _outerRadius );
        //    Vector3[] innerPoints = GetCircumferencePoints( sides , _innnerRadius );

        //    List<Vector3> points = new List<Vector3>();
        //    points.AddRange( outerPoints );
        //    points.AddRange( innerPoints );

        //    _vertices = points.ToArray();
        //    _indices = DrawHollowIndices( _polygonPoints );

        //    //Mesh mesh = new Mesh();
        //    //mesh.vertices = _vertices;
        //    //mesh.triangles = _indices;

        //    //mesh.RecalculateBounds();
        //    //mesh.RecalculateNormals();


        //    // DrawPolygon


        //    // Edge
        //    //EdgeCollider2D edgeCollider2D = GetComponent<EdgeCollider2D>();            
        //    //List<Vector2> edgePoints = new List<Vector2>();
        //    //edgePoints.AddRange( GetEdgePoints( outerPoints ) );
        //    //edgePoints.AddRange( GetEdgePoints( innerPoints ) );
        //    //edgeCollider2D.points = edgePoints.ToArray();
        //}


        //// 3D
        //// repeatCount 횟수만큼 텍스쳐를 반복 출력( WrapMode = Repeat )
        //private Vector2[] GetUVPoints( Vector3[] vertices , float outerRadius , float repeatCount )
        //{
        //    Vector2[] points = new Vector2[ _vertices.Length ];

        //    for ( int i = 0 ; i < vertices.Length ; ++i )
        //    {
        //        Vector2 point = Vector2.zero;

        //        // -outerRadius ~ outerRadius => 0~1 의 값으로 연산
        //        point.x = vertices[ i ].x / outerRadius * 0.5f + 0.5f;
        //        point.y = vertices[ i ].y / outerRadius * 0.5f + 0.5f;

        //        // 0~1의 값을 0~repeatCount 값으로 연산
        //        point *= repeatCount;

        //        points[ i ] = point;
        //    }

        //    return points;
        //}


        //private Vector2[] GetEdgePoints( Vector3[] vertices )
        //{
        //    Vector2[] points = new Vector2[ vertices.Length + 1 ];

        //    for ( int i = 0 ; i < vertices.Length ; ++i )
        //    {
        //        points[ i ] = vertices[ i ];
        //    }

        //    points[ points.Length - 1 ] = vertices[ 0 ];

        //    return points;
        //}
    }

}
