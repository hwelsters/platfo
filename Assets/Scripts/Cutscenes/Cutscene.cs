using System;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    protected List<Action> actions = new List<Action>();
    protected bool stepComplete = true;
    protected int currentStep = 0;

    private List<KeyCode> progressKeys = new List<KeyCode>() {
        KeyCode.Space,
        KeyCode.Return,
        KeyCode.Z,
        KeyCode.X
    };

    private bool autoProgress = false;

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
        int index = progressKeys.FindIndex(progressKey => Input.GetKeyDown(progressKey));
        return index >= 0;
    }

    public bool IsProgressKey()
    {
        int index = progressKeys.FindIndex(progressKey => Input.GetKey(progressKey));
        return index >= 0;
    }

    public bool IsProgressKeyUp()
    {
        int index = progressKeys.FindIndex(progressKey => Input.GetKeyUp(progressKey));
        return index >= 0;
    }

    public void RunAction(Action action)
    {
        stepComplete = false;
        action();
    }

    public void ProgressThroughCutscene() {
        if (IsProgressKeyPressed() && stepComplete || GetAutoProgress()) 
        {
            currentStep = (int) Mathf.Clamp(currentStep + 1, 0, actions.Count - 1);
            RunAction(actions[currentStep]);
        }
    }

    protected bool GetAutoProgress()
    {
        if (autoProgress)
        {
            autoProgress = false;
            return true;
        }
        return false;
    }

    protected void SetAutoProgress()
    {
        this.autoProgress = true;
    }
}
