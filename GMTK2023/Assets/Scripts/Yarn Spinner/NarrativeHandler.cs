using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class NarrativeHandler : MonoBehaviour
{
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
