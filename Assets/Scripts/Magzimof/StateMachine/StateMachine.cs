using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<Estate> : MonoBehaviour where Estate : Enum
{
    
    #region Fields

    protected Dictionary<Estate, BaseState<Estate>> States = new Dictionary<Estate, BaseState<Estate>>();
    protected BaseState<Estate> CurrentState;
    private bool _isTransitioningState = false;
    
    #endregion
    
    
    #region MonoBehaviour
    
        void Start()
        {
            CurrentState.EnterState();
        }
        
        void Update()
        {
            Estate nextStateKey = CurrentState.GetNextState();
            if (!_isTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }
            else if (!_isTransitioningState)
            {
                TransitionToState(nextStateKey);
            }
        }
    

        private void OnTriggerEnter(Collider other)
        {
            CurrentState.OnTriggerEnter(other);
        }
        
        private void OnTriggerStay(Collider other)
        {
            CurrentState.OnTriggerStay(other);
        }
        private void OnTriggerExit(Collider other)
        {
            CurrentState.OnTriggerExit(other);
        }

        #endregion
    
    
    #region Methods
    private void TransitionToState(Estate stateKey)
    {
        _isTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        _isTransitioningState = false;
    }  
    #endregion
    
}
