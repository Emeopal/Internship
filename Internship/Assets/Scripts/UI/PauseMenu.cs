using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject settings_Back;

    public void OnEnable()
    {
        settings.SetActive(true);
        if (settings_Back.activeSelf == true)
            settings_Back.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnDisable()
    {
        settings.SetActive(false);
        settings_Back.SetActive(true);
        Time.timeScale = 1;
    }
}
