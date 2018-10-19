using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSToU.Core;
using System;
using DG.Tweening;
using CSToU.UI;

namespace CSToU.Units
{
    public abstract class UnitBase : MonoBehaviour, IMovable
    {

        
        #region variables declaration
        public enum Class
        {
            Cylinder = 1,
            Cube = 2,
            Sphere = 3,
        }

        public Class CurrentClass;
        public string CurrentFaction = "";

        private int life = 5;

        private List<string> classList = new List<string> { "Cube", "Cylinder", "Sphere"};

        #endregion


        private void OnEnable()
        {
            GlobalEventManager.OnPostSetUp += MandatorySetUp;
            classList.Remove(CurrentClass.ToString());
            Debug.Log("Sperone" + classList[0]);
            Debug.Log("Sperone" + classList[1]);
        }

        #region SetUp
        private void MandatorySetUp()
        {

            CreateGraphic();
            SetUp();
            CSToUGameManager.I.MngUnit.UnitReady(this);
            SetFactionSetUp();
            StateMachineSetUp();
            GetComponentInChildren<PanelColorController>().SetUpPanel();
        }

        protected abstract void SetUp();
        

        
        protected virtual void CreateGraphic()
        {
           GameObject graphicPrefab = CSToUGameManager.I.MngUnit.GetPrefab(CurrentClass);
            if (graphicPrefab == null)
            {
                Debug.LogError("PrefaB" + CurrentClass + "NOT FOUND");
            }
            GameObject graphicInstance = Instantiate<GameObject>(graphicPrefab, transform);

        }
        


        protected void SetFactionSetUp()
        {
            GetComponentInChildren<Renderer>().material.color = CSToUGameManager.I.MngUnit.GetColor(CurrentFaction);

        }
        #endregion

        #region StateMachine Controller
        UnitStateMachine SM;

        public void StateMachineSetUp()
        {
            SM = GetComponent<UnitStateMachine>();
            SM.SetUp(this);
            if (SM == null)
                Debug.LogError("SM not found");

            else
                SM.CurrentState = UnitState.idle;
        }

        #endregion

        protected void Update()
        {
            SM = GetComponent<UnitStateMachine>();
            //GetComponentInChildren<TextBox>().SetText(SM.CurrentState.ToString());
        }
        

        #region Debug

        private void OnMouseDown()
        {
            switch (SM.CurrentState)
            {

                case UnitState.not_ready:
                    SM.CurrentState = UnitState.idle;
                    break;
                case UnitState.idle:
                    SM.CurrentState = UnitState.walk;
                    break;
                case UnitState.walk:
                    SM.CurrentState = UnitState.idle;
                    break;
                default:
                    break;
            }
           
        }
        #endregion

        #region Moviments

        void IMovable.MoveTo(Vector3 targetPosition)
        {
            Sequence movementSequence = DOTween.Sequence();
            movementSequence.Append(transform.DOLookAt(targetPosition, UnityEngine.Random.Range(1f, 2f)));
            movementSequence.Append(transform.DOMove(targetPosition, UnityEngine.Random.Range(1f, 2f))).OnComplete(() => { SM.CurrentState = UnitState.idle; });
        }

        void IMovable.Wait(float timeToWait)
        {
            transform.DOMove(transform.position, timeToWait).OnComplete(() => { SM.CurrentState = UnitState.walk; });
        }
        #endregion

        #region collision
        

        private void OnTriggerEnter(Collider other)
        {
            classList.Remove(CurrentClass.ToString());

            if (other.gameObject.tag == "Sperone" + classList[0] || other.gameObject.tag == "Sperone" + classList[1]) 
            {
                Debug.Log("Sperone" + classList[0]);
                Debug.Log("Sperone" + classList[1]);
                if (life == 1)
                {
                    
                    Destroy(gameObject);
                }
                if (life > 1)
                    life--;
            }
        }
        #endregion

        private void OnDisable()
        {
            GlobalEventManager.OnPostSetUp -= SetUp;
        }

        public Vector3 AvoidPosition()
        {
            return gameObject.transform.position;
        }
    }
    
    
    
}
