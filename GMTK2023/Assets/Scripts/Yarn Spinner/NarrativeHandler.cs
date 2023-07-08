using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
using OGFB;

public class NarrativeHandler : MonoBehaviour
{
    public string gender;
    public int flappyPoints = 5;

    [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

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
    }

    // Converts Functions to Yarn Commands
    private void AddYarnCommands()
    {
        dialogueRunner.AddCommandHandler("YarnTest", () => YarnTest());
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
        return oGFB_GameManager.GetScore();
    }
}
