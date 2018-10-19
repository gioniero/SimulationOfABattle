using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSToU.Units;
using UnityEngine.UI;

public class PanelColorController : MonoBehaviour {

    public Color WaitColor;
    public Color MoveColor;
   
    private UnitStateMachine SM;

    public void SetUpPanel()
    {
        
        SM = GetComponentInParent<UnitStateMachine>();
        
        GetComponent<Image>().color = WaitColor;
        SM.ChangeStatusToMove += ChangeColorToMove;
        SM.ChangeStatusToIdle += ChangeColorToIdle;
    }

    public void OnEnable()
    {
        
    }

    public void ChangeColorToMove()
    {
        GetComponent<Image>().color = MoveColor;
    }

    public void ChangeColorToIdle()
    {
        GetComponent<Image>().color = WaitColor;
    }

    public void OnDisable()
    {
        SM.ChangeStatusToMove -= ChangeColorToMove;
        SM.ChangeStatusToIdle -= ChangeColorToIdle;
    }



}
