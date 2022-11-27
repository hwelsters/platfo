using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[Serializable]
public struct Cut
{
    public Sprite image;
    [TextArea] public string text;
}


public class IntroCutscene : Cutscene
{
    [SerializeField] protected List<Cut> scenes = new List<Cut>();
    [SerializeField] protected Image introImage;
    [SerializeField] protected Image fadeImage;
    [SerializeField] protected TextMeshProUGUI introText;
    [SerializeField] protected string nextScene;

    private Coroutine textCoroutine;

    protected override void Start()
    {
        for(int i = 0; i < scenes.Count; i++) this.actions.Add(() => RunCut());
        this.actions.Add(()=>ChangeScene());
        base.Start();
    }

    private IEnumerator ShowText () {
        string shownText = "";
        int index = 0;

        introText.text = "";
        if (IsProgressKey()) { while (!IsProgressKeyUp()) yield return null; }

        while (index < scenes[currentStep].text.Length && !IsProgressKey())
        {
            shownText += this.scenes[currentStep].text[index++];
            this.introText.text = shownText;
            yield return new WaitForSeconds(0.1f);
        }

        introText.text = this.scenes[currentStep].text;
        while (!IsProgressKeyUp()) yield return null;

        this.stepComplete = true;
    }

    private void RunCut()
    {
        introImage.sprite = scenes[currentStep].image;

        if (textCoroutine != null) StopCoroutine(textCoroutine);
        textCoroutine = StartCoroutine(ShowText());
    }

    private void FadeToBlack()
    {
        
    }

    private IEnumerator FadeCoroutine ()
    {
        
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
