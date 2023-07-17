using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AULib;
using UnityEngine.Rendering.Universal;

namespace AULib
{
    /// <summary>
    /// ĵ���� ���� ����
    /// </summary>
    
    [RequireComponent(typeof(Canvas))]
    public class CanvasSetter : BaseBehaviour
    {
        [Tooltip("ĵ���� ���ÿ� ������ ����")]
        [SerializeField] CanvasSetData _canvasData;

        [Tooltip("ĵ���� Sort order offset")]
        [SerializeField] int _sortOrderOffset = 0;

        private Camera _UICamera;

        private Canvas _canvas;

        protected override void Awake() 
    	{
    		base.Awake();
            _canvas = GetComponent<Canvas>();
            _UICamera = GetComponentInChildren<Camera>();
        }
	
    	protected override void Start() 
    	{
    		base.Start();

            CanvasSet();
        }

      




        public void CanvasSet()
        {
            _canvas.renderMode = _canvasData.RenderMode;
          
            if (_canvasData.RenderMode == RenderMode.ScreenSpaceOverlay)
            {

            }
            else if (_canvasData.RenderMode == RenderMode.ScreenSpaceCamera)
            {
                _canvas.sortingLayerID = _canvasData.SortingLayerID;
                _canvas.worldCamera = _UICamera;
            }
            else
            {
                Debug.LogWarning("ĵ���� ������尡 World �Դϴ�.");
                return;
            }

            _canvas.pixelPerfect = _canvasData.PixelPerfect;
            _canvas.sortingOrder = _canvasData.SortOrder + _sortOrderOffset;
        }
    }
}
