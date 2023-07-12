using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OGFB_EndingHandler : MonoBehaviour
{
    public void Start()
    {
        if (FX_ScreenFXManager.instance != null)
            FX_ScreenFXManager.instance.RunScreenFX("Flash");

        if (AudioManager.Instance != null)
            AudioManager.Instance.Play("OGFB_Death");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
