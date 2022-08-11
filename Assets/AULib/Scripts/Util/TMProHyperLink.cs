using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

namespace AULib
{
    public class TMProHyperLink : BaseBehaviour, IPointerClickHandler
    {
        TextMeshProUGUI m_TextMeshPro;
        Camera m_Camera;
        Canvas m_Canvas;


        public event Action<string> OnClickLInk;


        protected override void Start()
        {
            base.Start();

            m_Camera = Camera.main;
            m_Canvas = gameObject.GetComponentInParent<Canvas>();
            if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                m_Camera = null;
            else
                m_Camera = m_Canvas.worldCamera;

            m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
            m_TextMeshPro.ForceMeshUpdate();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Mouse.current.position.ReadValue(), m_Camera);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[linkIndex];
                OnClickLInk?.Invoke(linkInfo.GetLinkID());
                //Application.OpenURL(linkInfo.GetLinkID());
            }
        }
    }
}