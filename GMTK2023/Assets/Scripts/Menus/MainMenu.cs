using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    public void Start()
    {
        audioManager = AudioManager.Instance;
    }

    public void LoadLevel(string levelName)
    {
    }
}
