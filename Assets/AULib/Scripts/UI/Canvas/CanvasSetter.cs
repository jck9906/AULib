using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AULib;
using UnityEngine.Rendering.Universal;

namespace AULib
{
    /// <summary>
    /// 캔버스 정보 셋팅
    /// </summary>
    
    [RequireComponent(typeof(Canvas))]
    public class CanvasSetter : BaseBehaviour
    {
        [Tooltip("캔버스 셋팅용 데이터 파일")]
        [SerializeField] CanvasSetData _canvasData;

        [Tooltip("캔버스 Sort order offset")]
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
                Debug.LogWarning("캔버스 렌더모드가 World 입니다.");
                return;
            }

            _canvas.pixelPerfect = _canvasData.PixelPerfect;
            _canvas.sortingOrder = _canvasData.SortOrder + _sortOrderOffset;
        }
    }
}
