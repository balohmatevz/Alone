using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{

    public float DayProgress = 0f;
    public float DaySpeed = 0.1f;

    public Image EyeOfGod;
    public Image WallLight;
    public Image FloorLight;

    public Image Bed;
    public Image Bucket;
    public Image Door;

    public Image Person;

    public CanvasGroup FaderCG;
    public AudioSource Sound;

    // Use this for initialization
    void Start()
    {
        Sound.volume = 0;
        FaderCG.alpha = 1;
        FaderCG.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        DayProgress += DaySpeed * Time.deltaTime;
        float light = (Mathf.Sin(DayProgress) * 0.5f) + 0.5f;

        EyeOfGod.color = new Color(1, 1, 1, light * 154 / 256f);
        WallLight.color = new Color(1, 1, 1, light);
        FloorLight.color = new Color(1, 1, 1, light);

        float furnitureColor = light * 0.8f + 0.2f;

        Bed.color = new Color(furnitureColor, furnitureColor, furnitureColor, 1);
        Bucket.color = new Color(furnitureColor, furnitureColor, furnitureColor, 1);
        Door.color = new Color(furnitureColor, furnitureColor, furnitureColor, 1);

        Person.color = new Color(light, light, light, 1);

        FaderCG.alpha -= Time.deltaTime * 0.2f;
        if (FaderCG.alpha <= 0.01f)
        {
            FaderCG.alpha = 0;
            FaderCG.gameObject.SetActive(false);
        }
        Sound.volume += Time.deltaTime * 0.2f * 0.2f;
        Sound.volume = Mathf.Min(Sound.volume, 0.2f);
    }
}
