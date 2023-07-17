using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    
    public interface IStepChangeObject
    {
        public void StepOn();
        public void StepOff();
    }
    public class StepManager<T> where T : Enum
    {

        public delegate void ChangeStepDelegate(T oldStep, T newStep);

        private T _currentStep = default(T);
        public  T CurrentStep => _currentStep;

        private T _oldStep = default(T);
        private T OldStep => _oldStep;

        public event ChangeStepDelegate onChangedStep;


        public bool PrevStep()
        {
            
            int stepToInt = (int)Enum.Parse(typeof(T), _currentStep.ToString(), true);
            if (stepToInt == 0)
            {
                Debug.LogWarning("Prev step not exist");
                return false;
            }

            SetStep((T)(object)--stepToInt);
            return true;
        }

        public bool NextStep()
        {
            int stepToInt = (int)Enum.Parse(typeof(T), _currentStep.ToString(), true);
            ++stepToInt;

            if ((T)(object)stepToInt == null)
            {
                Debug.LogWarning("Next step not exist");
                return false;
            }

            SetStep((T)(object)stepToInt);
            return true;
        }

        public void SetStep(T step)
        {
            _oldStep = _currentStep;
            _currentStep = step;

            onChangedStep?.Invoke(_oldStep, _currentStep);
        }


    }
}
