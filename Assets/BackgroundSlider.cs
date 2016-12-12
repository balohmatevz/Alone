using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BackgroundSlider : MonoBehaviour
{
    public float StartPos = 20.50f;
    public float EndPos = -20.50f;

    public float ScrollSpeed = 1f;
    public float FadeSpeed = 1f;
    public bool Initialized = false;

    public bool IntentOpenAbout = false;
    public bool IntentOpenMainMenu = false;
    public bool IntentOpenPlay = false;

    public bool ClosingAbout = false;
    public bool OpeningAbout = false;
    public bool ClosingMainMenu = false;
    public bool OpeningMainMenu = false;
    public bool OpeningPlay = false;


    public CanvasGroup MainMenuCG;
    public CanvasGroup AboutCG;
    public GameObject MainMenuGO;
    public GameObject AboutGO;

    public float MusicDelay = 2f;
    public bool MusicPlayed = false;
    public AudioSource MenuMusic;

    [SerializeField]
    private Image[] imgs;
    [SerializeField]
    private CanvasGroup[] buttons;
    [SerializeField]
    private Text title;
    private RectTransform rt;

    private bool FadingIn = false;

    public Image Fader;
    public CanvasGroup FaderCG;

    // Use this for initialization
    void Start()
    {
        rt = this.GetComponent<RectTransform>();

        //Initial Fade-in
        FadingIn = true;
        foreach (Image img in imgs)
        {
            img.color = Color.black;
        }

        foreach (CanvasGroup cg in buttons)
        {
            cg.alpha = 0;
        }

        title.color = new Color(86 / 255f, 86 / 255f, 86 / 255f, 0);

        MainMenuGO.SetActive(false);
        AboutGO.SetActive(false);
        Fader.gameObject.SetActive(false);
        FaderCG.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MusicPlayed)
        {
            MusicDelay -= Time.deltaTime;
            if (MusicDelay <= 0)
            {
                MusicPlayed = true;
                MenuMusic.Play();
            }
        }

        //Move background
        Vector2 pos = rt.anchoredPosition;
        pos.x -= ScrollSpeed * Time.deltaTime;
        rt.anchoredPosition = pos;

        if (FadingIn)
        {
            //Fading in
            foreach (Image img in imgs)
            {
                img.color = img.color + new Color(Time.deltaTime * FadeSpeed, Time.deltaTime * FadeSpeed, Time.deltaTime * FadeSpeed, 0);
                if (img.color.r > 0.99f)
                {
                    img.color = Color.white;
                    FadingIn = false;
                }
            }
        }

        //Fade in Title
        float clr = 0;
        foreach (Image img in imgs)
        {
            clr = img.color.r;
        }
        if (clr > (86 / 255f))
        {
            //Fade in Title
            Color c = title.color;
            c.a += Time.deltaTime * FadeSpeed;
            title.color = c;
        }
        if (clr > (200 / 255f))
        {
            if (!Initialized)
            {
                MainMenuGO.SetActive(true);
                Initialized = true;
            }
            //Fade in Buttons
            foreach (CanvasGroup cg in buttons)
            {
                cg.alpha += Time.deltaTime * FadeSpeed * 4;
            }
        }

        if (OpeningMainMenu)
        {
            MainMenuGO.SetActive(true);
            MainMenuCG.alpha += Time.deltaTime * 0.5f;
            if (MainMenuCG.alpha >= 0.99f)
            {
                MainMenuCG.alpha = 1;
                OpeningMainMenu = false;
            }
        }
        else
        {
            if (ClosingMainMenu)
            {
                MainMenuCG.alpha -= Time.deltaTime * 0.5f;
                if (MainMenuCG.alpha < 0.01f)
                {
                    MainMenuCG.alpha = 0;
                    ClosingMainMenu = false;
                    MainMenuGO.SetActive(false);

                    if (IntentOpenAbout)
                    {
                        OpeningAbout = true;
                        IntentOpenAbout = false;
                    }
                    if (IntentOpenMainMenu)
                    {
                        OpeningMainMenu = true;
                        IntentOpenMainMenu = false;
                    }
                }
            }
        }

        if (OpeningAbout)
        {
            AboutGO.SetActive(true);
            AboutCG.alpha += Time.deltaTime * 0.5f;
            if (AboutCG.alpha >= 0.99f)
            {
                AboutCG.alpha = 1;
                OpeningAbout = false;
            }
        }
        else
        {
            if (ClosingAbout)
            {
                AboutCG.alpha -= Time.deltaTime * 0.5f;
                if (AboutCG.alpha < 0.01f)
                {
                    AboutCG.alpha = 0;
                    ClosingAbout = false;
                    AboutGO.SetActive(false);

                    if (IntentOpenAbout)
                    {
                        OpeningAbout = true;
                        IntentOpenAbout = false;
                    }
                    if (IntentOpenMainMenu)
                    {
                        OpeningMainMenu = true;
                        IntentOpenMainMenu = false;
                    }
                }
            }
        }

        if (IntentOpenPlay)
        {
            MenuMusic.volume -= Time.deltaTime * 0.25f;
            Fader.gameObject.SetActive(true);
            FaderCG.alpha += Time.deltaTime * 0.25f;
            if (FaderCG.alpha > 0.99f)
            {
                SceneManager.LoadScene("Cell Block");
            }
        }

        if (pos.x < EndPos)
        {
            //Fading out
            foreach (Image img in imgs)
            {
                img.color = img.color - new Color(Time.deltaTime * FadeSpeed, Time.deltaTime * FadeSpeed, Time.deltaTime * FadeSpeed, 0);
                if (img.color.r < 0.01f)
                {
                    img.color = Color.black;
                    FadingIn = true;

                    //Move back to start
                    rt.anchoredPosition = new Vector2(StartPos, 0);
                }
            }
        }
    }

    public void INPUT_OpenAbout()
    {
        AboutCG.alpha = 0;
        ClosingMainMenu = true;
        IntentOpenAbout = true;
    }

    public void INPUT_CloseAbout()
    {
        ClosingAbout = true;
        IntentOpenMainMenu = true;
    }

    public void INPUT_OpenPlay()
    {
        ClosingMainMenu = true;
        IntentOpenPlay = true;
    }

    public void INPUT_Quit()
    {
        Application.Quit();
    }

    public void INPUT_OpenLiamLime()
    {
        Application.OpenURL("http://liamlime.com/");
    }

    public void INPUT_OpenLudumDare()
    {
        Application.OpenURL("http://ldjam.com/");
    }

    public void INPUT_OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/LiamLimeGames");
    }
}
