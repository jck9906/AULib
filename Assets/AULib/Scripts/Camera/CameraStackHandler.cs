using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AULib
{

    /// <summary>
    /// ���̽� ī�޶� �������� ī�޶� ����ŷ ó��
    /// ���̽� ī�޶� ����ŷ�� �ʿ��� ī�޶� ���۳�Ʈ�� �߰��Ѵ�.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraStackHandler : BaseBehaviour
    {
        //����ī�޶� ����
        private static Camera _baseCamera;
        private static UniversalAdditionalCameraData _baseCameraData;

        //�������� ī�޶� ����
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
                Debug.LogWarning("���� ī�޶��� ����Ÿ���� Overlay�� �ƴմϴ�.");
                return;
            }

            if (_baseCamera == null)
            {
                Debug.LogWarning("���� ī�޶� �����ϴ�.");
                return;
            }

            _baseCameraData = _baseCamera.GetUniversalAdditionalCameraData();

            if (_baseCameraData.renderType != CameraRenderType.Base)
            {
                Debug.LogWarning("���� ī�޶��� ����Ÿ���� Base�� �ƴմϴ�.");
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
