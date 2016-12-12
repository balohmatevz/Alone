using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour
{

    float MoveSpeed = 20;
    RectTransform rt;

    // Use this for initialization
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        }

        Vector2 pos = rt.anchoredPosition;
        pos.x = Mathf.Clamp(pos.x, -680.7175f, 680.7175f);
        rt.anchoredPosition = pos;
    }
}
