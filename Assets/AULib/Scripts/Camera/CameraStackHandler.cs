using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AULib
{

    /// <summary>
    /// 베이스 카메라에 오버레이 카메라 스태킹 처리
    /// 베이스 카메라에 스태킹이 필요한 카메라에 컴퍼넌트를 추가한다.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraStackHandler : BaseBehaviour
    {
        //메인카메라 정보
        private static Camera _baseCamera;
        private static UniversalAdditionalCameraData _baseCameraData;

        //오버레이 카메라 정보
        private Camera _overlayCamera;
        private static UniversalAdditionalCameraData _overlayCameraData;

        protected override void Awake() 
    	{
    		base.Awake();


            _baseCamera = Camera.main;
            _overlayCamera = GetComponent<Camera>();

            _overlayCameraData = _overlayCamera.GetUniversalAdditionalCameraData();

            if (_overlayCameraData.renderType != CameraRenderType.Overlay)
            {
                Debug.LogWarning("현재 카메라의 렌더타입이 Overlay가 아닙니다.");
                return;
            }

            if (_baseCamera == null)
            {
                Debug.LogWarning("메인 카메라가 없습니다.");
                return;
            }

            _baseCameraData = _baseCamera.GetUniversalAdditionalCameraData();

            if (_baseCameraData.renderType != CameraRenderType.Base)
            {
                Debug.LogWarning("메인 카메라의 렌더타입이 Base가 아닙니다.");
                return;
            }
        }



        private void OnEnable()
        {
            if (_overlayCameraData.renderType == CameraRenderType.Overlay)
            {
                _baseCameraData.cameraStack.Add(_overlayCamera);
            }
            
        }


        private void OnDisable()
        {
            if (_overlayCameraData.renderType == CameraRenderType.Overlay)
            {
                _baseCameraData.cameraStack.Remove(_overlayCamera);
            }            
        }
    }
}
