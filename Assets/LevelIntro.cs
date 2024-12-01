using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    private static LevelIntro instance;
    private TextMeshProUGUI text;
    private float fade_time = 10000;

    private void Start()
    {
        instance = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        fade_time += Time.deltaTime;
        float percent = 0;
        if(fade_time < 5)
        {
            percent = 0;
        }
        else
        {
            percent = (fade_time - 5) / 3;
        }
        text.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white, Color.clear, percent));
    }

    public static void DisplayLevelText()
    {
        instance.fade_time = 0;
        instance.text.text = Level.GetCurrentLevel().intro_name + "\n" + Level.GetCurrentLevel().intro_description;
    }
}
