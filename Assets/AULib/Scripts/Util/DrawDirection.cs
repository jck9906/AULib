using UnityEngine;


namespace AULib
{
    public class DrawDirection : MonoBehaviour
    {
        public eDirection _direction;
        public enum eDirection
        {
            LEFT,
            RIGHT,
            FOWARD,
            BACK,
            DOWN,
            UP
        }


        private void OnDrawGizmos()
        {
            var direction = transform.TransformDirection(GetDirection(_direction));
            Gizmos.DrawRay(transform.position, direction * 50f);
        }



        private Vector3 GetDirection(eDirection dir) => dir switch
        {
            eDirection.LEFT => Vector3.left,
            eDirection.RIGHT => Vector3.right,
            eDirection.FOWARD => Vector3.forward,
            eDirection.BACK => Vector3.back,
            eDirection.DOWN => Vector3.down,
            eDirection.UP => Vector3.up,
            _ => Vector3.zero
        };



    }
}