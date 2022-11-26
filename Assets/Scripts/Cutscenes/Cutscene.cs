using System;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    protected List<Action> actions;
    protected bool stepComplete = true;
    protected int currentStep = 0;

    public void Start()
    {
        RunAction(actions[0]);
    }

    public bool IsProgressKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
    }

    public void RunAction(Action action)
    {
        stepComplete = false;
        action();
    }

    public void ProgressThroughCutscene() {
        if (IsProgressKeyPressed() && stepComplete) 
        {
            currentStep++;
            RunAction(actions[currentStep]);
        }
    }
}
