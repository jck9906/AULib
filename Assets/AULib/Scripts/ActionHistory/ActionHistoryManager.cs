using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{

    public interface IHistoryable
    {
        //public static IHistoryable Get(IHistoryable info) => GetHistoryable(info);

        public IHistoryable GetInstance(IHistoryable info);
    }

  

    public class ActionHistoryManager<T> where T : IHistoryable
    {
        private int _historyMax = 10;

        private T emptyInfo;//  = new ALCharacterInfo();
        public T EmptyInfo => emptyInfo;

        public Stack<T> actionStack = new Stack<T>();
        private Stack<T> undoActionStack = new Stack<T>();


        public ActionHistoryManager()
        {

        }

     
        public void Init()
        {
            actionStack.Clear();
            undoActionStack.Clear();
        }

        public void SetInitInfo(T actionInfo)
        {
            emptyInfo = (T)actionInfo.GetInstance(actionInfo);
        }

        public void AddAction(T actionInfo)
        {
            // Debug.Log( "AddAction = " + actionStack .Count );
            T info = (T)actionInfo.GetInstance(actionInfo);

            actionStack.Push(info);
            undoActionStack.Clear();

            if (actionStack.Count > _historyMax)
            {
                T[] temp = actionStack.ToArray();
                actionStack.Clear();

                SetInitInfo(temp[temp.Length - 1]);
                for (int i = 0; i < temp.Length - 1; i++)
                    actionStack.Push(temp[temp.Length - 2 - i]);
            }
        }

        public T ActionUndo()
        {
            if (actionStack.Count > 0)
                undoActionStack.Push(actionStack.Pop());

            if (actionStack.Count == 0)
                return emptyInfo;

            return actionStack.Peek();
        }

        public T ActionRedo()
        {
            if (undoActionStack.Count == 0)
                return default(T);

            actionStack.Push(undoActionStack.Pop());

            return actionStack.Peek();
        }

        public bool IsUndo()
        {
            //return true;
            return actionStack.Count != 0 ? true : false;
        }
        public bool IsRedo()
        {
            //return true;
            return undoActionStack.Count != 0 ? true : false;
        }
    }

    
}
