using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
using OGFB;
using UnityEngine.SceneManagement;

public class NarrativeHandler : MonoBehaviour
{
    public string gender;

    [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

    public UnityEvent<string> onGenderSelected;

    private static NarrativeHandler instance; // Singleton for the Narrative Handler

    public static NarrativeHandler Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Make NarrativeHandler a Singleton
        if (instance != null)
        {
            Debug.LogError("Found more than one NarrativeHandler in the scene!");
        }
        else
        {
            instance = this;
        }

        AddYarnCommands();
    }

    public void StartDialogue(string node)
    {
        dialogueRunner.StartDialogue(node);
    }

    public void SetGender(string gender)
    {
        this.gender = gender;
        onGenderSelected?.Invoke(gender);
    }

    public void TriggerGoToEnding()
    {
        Invoke("GoToEndingScene", 2f);
    }

    public void GoToEndingScene()
    {
        SceneManager.LoadScene(2);
    }

    // Converts Functions to Yarn Commands
    private void AddYarnCommands()
    {
        if(dialogueRunner != null)
        {
            dialogueRunner.AddCommandHandler("YarnTest", () => YarnTest());
            dialogueRunner.AddCommandHandler("TriggerEnding", TriggerGoToEnding);
        }
    }
    

    private void YarnTest()
    {
        Debug.Log("Test");
    }
}

public static class AdderFunction
{
    public static NarrativeHandler narrativeHandler = NarrativeHandler.Instance;
    private static OGFB.OGFB_GameManager oGFB_GameManager = OGFB.OGFB_GameManager.instance;

    [YarnFunction("Name")]
    public static string Name()
    {
        string name = "";
        if (narrativeHandler.gender == "T")
        {
            name = "Pipe-Chan";
        }
        else
        {
            name = "Pipe-Kun";
        }
        return name;
    }

    [YarnFunction("FlappyPoints")]
    public static int FlappyPoints()
    {
        Debug.Log(oGFB_GameManager.GetScore());
        return oGFB_GameManager.GetLastScore();
    }
}
