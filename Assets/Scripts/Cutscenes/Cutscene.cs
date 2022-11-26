using System;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    protected List<Action> actions = new List<Action>();
    protected bool stepComplete = true;
    protected int currentStep = 0;

    protected virtual void Start()
    {
        RunAction(actions[0]);
    }

    protected virtual void Update()
    {
        ProgressThroughCutscene();
    }

    public bool IsProgressKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);
    }

    public bool IsProgressKey()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return);
    }

    public bool IsProgressKeyUp()
    {
        return Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return);
    }

    public void RunAction(Action action)
    {
        stepComplete = false;
        action();
    }

    public void ProgressThroughCutscene() {
        if (IsProgressKeyPressed() && stepComplete) 
        {
            currentStep = (int) Mathf.Clamp(currentStep + 1, 0, actions.Count - 1);
            RunAction(actions[currentStep]);
        }
    }
}
