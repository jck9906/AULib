namespace AULib
{
    /// <summary>
    /// 
    /// </summary>
    public class UIObjectRegister : BaseBehaviour
    {

        public bool IsOverRay_Dynamic = false;

        protected override void Awake() 
    	{
    		base.Awake();
		
            
		
    	}

        private void OnEnable()
        {
            var registerObjects = GetComponentsInChildren<IUIObject>(true);

            foreach (var item in registerObjects)
            {
                UIManager.i.RegisterUIObject(item);
            }

        }



        private void OnDisable()
        {
            var registerObjects = GetComponentsInChildren<IUIObject>(true);

            foreach (var item in registerObjects)
            {
                UIManager.i.UnRegisterUIObject(item);
            }            
        }

        protected override void Start()
        {

            if ( IsOverRay_Dynamic )
            {
                UIManager.i.UIStackAdd( this.gameObject );
            }
        }

        private void OnDestroy()
        {
            if ( IsOverRay_Dynamic )
            {
                UIManager.i.UIStackDel( this.gameObject );
            }
        }
    }
}
