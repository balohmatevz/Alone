using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CloudMove : MonoBehaviour
{
    public RectTransform rt;
    public float Speed = 1f;

    // Use this for initialization
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        Vector2 pos = rt.anchoredPosition;
        pos.y = Random.Range(1.24f, 4.66f);
        rt.anchoredPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        rt.Translate(Speed * Time.deltaTime, 0, 0);
        if (rt.localPosition.x > 200.3f)
        {
            rt.Translate(-300, 0, 0);
            Vector2 pos = rt.anchoredPosition;
            pos.y = Random.Range(1.24f, 4.66f);
            rt.anchoredPosition = pos;
        }
    }
}
