using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    /// <summary>
    /// 토글버튼 기능 추가
    /// </summary>    
    [RequireComponent(typeof(Toggle))]
    public class ToggleEx : BaseBehaviour
    {
        public Graphic[] graphics;
        public GameObject actvieToggle;
        public GameObject deActvieToggle;
        public Color colorDefault = Color.white;
        public Color colorSelect = new Color(0.647f, 0.529f, 0.921f);
        public eToggleType toggleType = eToggleType.ColorChange;




        private Toggle toggle;

        protected override void Start()
        {
            base.Start();
            toggle = GetComponent<Toggle>();

            toggle.onValueChanged.AddListener(delegate
            {
                HandleToggleValueChanged(toggle.isOn);
            });

            HandleToggleValueChanged(toggle.isOn);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="isOn"></param>
        public void SetToggleOn(bool isOn)
        {
            toggle.isOn = isOn;
        }








        private void HandleToggleValueChanged(bool isOn)
        {
            
            switch (toggleType)
            {
                case eToggleType.ColorChange:
                    foreach (Graphic item in graphics)
                    {
                        if (isOn)
                        {
                            item.color = colorSelect;
                        }
                        else
                        {
                            item.color = colorDefault;
                        }
                        
                    }
                    break;

                case eToggleType.SwapGameObject:
                    actvieToggle.SetActive(isOn);
                    deActvieToggle.SetActive(!isOn);
                    break;

                default:
                    break;
            }

            

            
        }


        /// <summary>
        /// 토글 버튼 형태
        /// ColorChange - On/Off 시 색상 변경
        /// SwapGameObject - On/Off 오브젝트 변경
        /// </summary>
        public enum eToggleType
        {
            None = 0,
            ColorChange = 1,
            SwapGameObject = 4
        }
    }
}