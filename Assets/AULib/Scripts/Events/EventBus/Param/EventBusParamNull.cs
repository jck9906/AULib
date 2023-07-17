

using UnityEngine;

namespace AULib
{
    

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public struct EventBusParamNull : IEventBusParam
    {
        public static EventBusParamNull GetDefaultParam()
        {
            EventBusParamNull result = new();
            return result;
        }
    }


   
}
