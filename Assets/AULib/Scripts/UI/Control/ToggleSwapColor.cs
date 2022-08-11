using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AULib
{
    public class ToggleSwapColor : MonoBehaviour
    {
        [SerializeField] Color default_color = Color.white;
        [SerializeField] Color selected_color = new Color(0.647f, 0.529f, 0.921f);

        public Graphic[] graphics;
        //public Image icon;

        Toggle toggle;


        private void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            if (toggle == null)
                return;

            toggle.onValueChanged.AddListener(delegate
            {
                ToggleValueChanged(toggle.isOn);
            });

            ToggleValueChanged(toggle.isOn);
        }

        public void ToggleValueChanged(bool toggleOn)
        {
            if (toggleOn)
            {
                foreach (Graphic item in graphics)
                {
                    item.color = selected_color;
                }
            }
            else
            {
                foreach (Graphic item in graphics)
                {
                    item.color = default_color;
                }
            }
        }
    }
}