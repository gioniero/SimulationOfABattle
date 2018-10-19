using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSToU.Core;
namespace CSToU.Units
{

    public class UnitStateMachine : MonoBehaviour
    {
        public delegate void OnChangeStatus();
        public OnChangeStatus ChangeStatusToMove;
        public OnChangeStatus ChangeStatusToIdle;

        private UnitState _currentState = UnitState.not_ready;
     

        Animator anim;

        public UnitState CurrentState {
            get {return _currentState; } 
            set {
                if (_currentState != value)
                {
                    UnitState oldState = _currentState;
                    _currentState = value;
                    StateChanged(oldState);
                   
                }
                else
                {
                    _currentState = value;
                }
            } }
        #region SetUp IMovable
        IMovable movable;

        public void SetUp(IMovable _movable)
        {
            movable = _movable;
            anim = GetComponentInChildren<Animator>();
        }
        #endregion

       

        private void StateChanged(UnitState oldState)
        {
            switch (CurrentState)
            {
                case UnitState.not_ready:
                    break;
                case UnitState.idle:
                    anim.SetBool("Ready", true);
                    movable.Wait(UnityEngine.Random.Range(1f, 2f));
                    if (ChangeStatusToMove != null)
                    {
                        
                        ChangeStatusToMove();
                    }
                    break;
                case UnitState.walk:
                    anim.SetBool("Pressed", true);
                    movable.MoveTo(CSToUGameManager.I.GetRandomSpawnPosition());
                    if (ChangeStatusToIdle != null)
                    {
                       
                        ChangeStatusToIdle();
                    }
                    break;
                default:
                    break;
            } 
        }

    }

    

    public enum UnitState
    {
        not_ready = 0,
        idle = 1,
        walk = 2,
    }
    
}
