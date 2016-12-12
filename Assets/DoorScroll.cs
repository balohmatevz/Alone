using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DoorScroll : MonoBehaviour
{

    float Speed = 70f;
    RectTransform rt;

    public static float SpeedModifier = 1f;
    public static bool SceneEnd = false;
    public static bool DoorChosen = false;

    public Button btn;

    // Use this for initialization
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        btn = this.GetComponent<Button>();
        btn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneEnd)
        {
            if (rt.anchoredPosition.x > 0 && rt.anchoredPosition.x < 20)
            {
                DoorChosen = true;
                btn.interactable = true;
            }
        }

        if (DoorChosen)
        {
            SpeedModifier -= Time.deltaTime * 0.2f;
        }

        SpeedModifier = Mathf.Max(SpeedModifier, 0);

        this.transform.Translate(-Speed * SpeedModifier * Time.deltaTime, 0, 0);
        if (rt.anchoredPosition.x < -1000f)
        {
            Vector2 pos = rt.anchoredPosition;
            pos.x += 2500;
            rt.anchoredPosition = pos;
        }
    }
}
