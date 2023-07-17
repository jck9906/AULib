using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    public class BackgroundBlock : BaseBehaviour
    {
        private IPopup _currentPop;

        protected override void Awake() 
    	{
    		base.Awake();
    	}
	
    	protected override void Start() 
    	{
    		base.Start();
		
    	}









        public void Show(IPopup pop)
        {
            _currentPop = pop;
            base.Show();
        }


        public void Hide(IPopup pop)
        {
            _currentPop = null;
            base.Hide();
        }
    }
}
