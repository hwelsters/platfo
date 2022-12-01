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

// SOMEBODY TOUCHA MA SPAGHETT, THIS CODE IS SPAGHETT
public class IntroCutscene : Cutscene
{
    private const float FADE_DURATION = 2f;
    private const float FADE_WAIT_DURATION = 1f;
    private const float FADE_NUMBER_OF_STEPS = 5f;
    private const float FADE_STEP_DURATION = FADE_DURATION / FADE_NUMBER_OF_STEPS;
    private const float FADE_UPDATE_DURATION = 1f / FADE_NUMBER_OF_STEPS;

    [SerializeField] protected List<Cut> scenes = new List<Cut>();
    [SerializeField] protected Image introImage;
    [SerializeField] protected Image fadeImage;
    [SerializeField] protected TextMeshProUGUI introText;
    [SerializeField] protected string nextScene;

    private Coroutine textCoroutine;

    protected override void Start()
    {
        for(int i = 0; i < scenes.Count; i++) this.actions.Add(() => RunCut());
        this.actions.Add(()=>FadeToBlack());
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
        StartCoroutine(FadeCoroutine());
    }

    // I want a chunky old school fade
    private IEnumerator FadeCoroutine ()
    {
        float alpha = 0f;
        while (alpha < FADE_DURATION + FADE_WAIT_DURATION)
        {
            alpha += FADE_UPDATE_DURATION;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            yield return new WaitForSeconds(FADE_STEP_DURATION);
        }
        SetAutoProgress();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
