using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Cut
{
    public Sprite image;
    public string text;
}

public class IntroCutscene : Cutscene
{
    [SerializeField]
    protected List<Cut> scenes;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
