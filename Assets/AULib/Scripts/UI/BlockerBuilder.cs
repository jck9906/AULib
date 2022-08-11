using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AULib
{


    /// <summary>
    /// Blocker 사용 시 구현해야 할 인터페이스
    /// </summary>
    public interface IBlockerUsable
    {
        void Close();
    }


    /// <summary>
    /// UI 배경 블럭(클릭 방지) 사용을 위한 빌더
    /// UnityEngine.UI의 DropDown에서 가져 옴
    /// </summary>
    public class BlockerBuilder 
    {
        private static GameObject m_Blocker;


        /// <summary>
        /// 블럭 생성
        /// </summary>
        /// <param name="rootCanvas"></param>
        /// <param name="targetCanvas"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static GameObject CreateBlocker(Canvas rootCanvas, Canvas targetCanvas, IBlockerUsable target = null)
        {
            m_Blocker = GetBlocker(rootCanvas, targetCanvas, target);
            return m_Blocker;
        }




        public static void Hide(IBlockerUsable target, bool closePop)
        {
            if (m_Blocker != null)
                DestroyBlocker(m_Blocker);

            m_Blocker = null;
            if (closePop)
            {
                target.Close();
            }
            
        }


        private static void DestroyBlocker(GameObject blocker)
        {
            GameObject.Destroy(blocker);
        }

        private static GameObject GetBlocker(Canvas rootCanvas, Canvas targetCanvas, IBlockerUsable target)
        {
            // Create blocker GameObject.
            GameObject blocker = new GameObject("Blocker");

            // Setup blocker RectTransform to cover entire root canvas area.
            RectTransform blockerRect = blocker.AddComponent<RectTransform>();
            blockerRect.SetParent(rootCanvas.transform, false);
            blockerRect.anchorMin = Vector3.zero;
            blockerRect.anchorMax = Vector3.one;
            blockerRect.sizeDelta = Vector2.zero;

            // Make blocker be in separate canvas in same layer as dropdown and in layer just below it.
            Canvas blockerCanvas = blocker.AddComponent<Canvas>();
            blockerCanvas.overrideSorting = true;
            //Canvas dropdownCanvas = m_Dropdown.GetComponent<Canvas>();
            //Canvas dropdownCanvas = target.GetComponentInParent<Canvas>();
            blockerCanvas.sortingLayerID = targetCanvas.sortingLayerID;
            blockerCanvas.sortingOrder = targetCanvas.sortingOrder - 1;

            // Find the Canvas that this dropdown is a part of
            Canvas parentCanvas = null;
            //Transform parentTransform = m_Template.parent;
            //while (parentTransform != null)
            //{
            //    parentCanvas = parentTransform.GetComponent<Canvas>();
            //    if (parentCanvas != null)
            //        break;

            //    parentTransform = parentTransform.parent;
            //}

            // If we have a parent canvas, apply the same raycasters as the parent for consistency.
            if (parentCanvas != null)
            {
                Component[] components = parentCanvas.GetComponents<BaseRaycaster>();
                for (int i = 0; i < components.Length; i++)
                {
                    Type raycasterType = components[i].GetType();
                    if (blocker.GetComponent(raycasterType) == null)
                    {
                        blocker.AddComponent(raycasterType);
                    }
                }
            }
            else
            {
                // Add raycaster since it's needed to block.
                blocker.AddComponent<GraphicRaycaster>();
                //GetOrAddComponent<GraphicRaycaster>(blocker);
            }


            // Add image since it's needed to block, but make it clear.
            Image blockerImage = blocker.AddComponent<Image>();
            blockerImage.color = Color.clear;

            if (target != null)
            {
                // Add button since it's needed to block, and to close the dropdown when blocking area is clicked.
                Button blockerButton = blocker.AddComponent<Button>();
                blockerButton.onClick.AddListener(() => { Hide(target, true); });
            }
            

            return blocker;
        }
    }
}