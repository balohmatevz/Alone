using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GC : MonoBehaviour
{

    public string[] Lines;
    public int CurrentLine = 0;

    public AudioSource audio;

    public Text ProgressionText;
    public Button NextButton;
    public float Timer = 2f;

    public float StartTimer = 1f;

    public bool FaderDefading = true;
    public bool FaderFading = false;
    public bool Init = false;

    public Image Fader;
    public CanvasGroup FaderCG;

    // Use this for initialization
    void Start()
    {
        NextButton.gameObject.SetActive(false);
        FaderCG.gameObject.SetActive(true);
        FaderCG.alpha = 1f;
        ProgressionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer -= Time.deltaTime;
        if (StartTimer > 0)
        {
            return;
        }

        if (FaderFading)
        {
            FaderCG.alpha += Time.deltaTime * 0.2f;
            audio.volume -= Time.deltaTime * 0.2f;
            if (FaderCG.alpha > 0.99f)
            {
                SceneManager.LoadScene("Play");
            }
        }

        if (FaderDefading)
        {
            FaderCG.alpha -= Time.deltaTime * 0.2f;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            if (FaderCG.alpha < 0.01f)
            {
                FaderDefading = false;
                FaderCG.alpha = 0;
                FaderCG.gameObject.SetActive(false);
            }
            return;
        }

        if (!Init)
        {
            Init = true;
            ProgressionText.text = Lines[0];
        }

        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            if (!DoorScroll.SceneEnd)
            {
                NextButton.gameObject.SetActive(true);
            }
        }
    }

    public void INPUT_NextText()
    {
        CurrentLine++;
        if (CurrentLine < Lines.Length)
        {
            ProgressionText.text = Lines[CurrentLine];
            NextButton.gameObject.SetActive(false);
            Timer = 2f;
        }

        if (CurrentLine == Lines.Length - 1)
        {
            DoorScroll.SceneEnd = true;
        }
    }

    public void INPUT_EnterCell()
    {
        FaderCG.gameObject.SetActive(true);
        FaderFading = true;
    }
}
