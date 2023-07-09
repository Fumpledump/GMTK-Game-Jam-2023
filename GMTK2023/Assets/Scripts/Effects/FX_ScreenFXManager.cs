using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yarn.Unity;

public class FX_ScreenFXManager : MonoBehaviour
{
    public static FX_ScreenFXManager instance;
    [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

    [SerializeField] private FX_CameraShake cameraShake;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem suprisedFX;
    [SerializeField] private ParticleSystem flowersFX;
    [SerializeField] private ParticleSystem deskSlamFX;
    [SerializeField] private ParticleSystem impactFX;
    [SerializeField] private ParticleSystem exclaimationFX;
    [SerializeField] private ParticleSystem sparksFX;
    [SerializeField] private ParticleSystem heartSingleFX;
    [SerializeField] private ParticleSystem heartMutlipleFX;
    [SerializeField] private ParticleSystem rosesFX;
    [SerializeField] private ParticleSystem steamFX;
    [SerializeField] private ParticleSystem animeTwinkleFX;
    [SerializeField] private ParticleSystem questionFX;
    [SerializeField] private ParticleSystem doritosFX;
    [SerializeField] private ParticleSystem sadFX;

    private Animator anim;

    //NOTE: Look here for screen fx types
    //public enum FX_ScreenFXType { None, Suprised, Shock, Confused, Extreme_Shock, Angry, Cutesy, Love, Saucy, Heartbeat, DeskSlam, Doritos, Sad, ShowSepia, HideSepia, ShowBlack, HideBlack, ShowWhite, HideWhite };

    private void Awake()
    {
        if (instance == null) instance = this;
        anim = GetComponent<Animator>();
        cameraShake = GetComponent<FX_CameraShake>();

        if(dialogueRunner != null)
            dialogueRunner.AddCommandHandler<string>("RunScreenFX", RunScreenFX);
    }

    private void RunScreenFX(string fxType)
    {
        bool resetColor = true;
        bool resetSepia = true;
        bool resetIris = true;
        bool resetSteam = true;
        anim.SetBool("ShowRoses", fxType == "Love");

        switch(fxType)
        {
            case "Surprised":
                exclaimationFX.Play();
                suprisedFX.Play();
                cameraShake.SetShake(0.1f, 2);
                break;
            case "Shock":
                sparksFX.Play();
                suprisedFX.Play();
                cameraShake.SetShake(0.1f, 4);
                anim.Play("FX_Color_Flash");
                resetColor = false;
                break;
            case "Confused":
                questionFX.Play();
                break;
            case "Extreme_Shock":
                sparksFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.3f, 6);
                anim.Play("FX_Color_DoubleFlash");
                resetColor = false;
                break;
            case "Angry":
                animeTwinkleFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.1f, 2);
                anim.Play("FX_Iris_RedStill");
                anim.Play("FX_Color_Flash");
                resetColor = false;
                resetIris = false;
                break;
            case "Cutesy":
                flowersFX.Play();
                anim.Play("FX_Iris_PinkStill");
                anim.Play("FX_Color_PinkTint");
                resetColor = false;
                resetIris = false;
                break;
            case "Love":
                rosesFX.Play();
                anim.Play("FX_Iris_PinkStill");
                anim.Play("FX_Color_PinkTint");
                resetColor = false;
                resetIris = false;
                break;
            case "Saucy":
                steamFX.Play();
                anim.Play("FX_Iris_RedPulsate");
                resetIris = false;
                resetSteam = false;
                break;
            case "Heartbeat":
                heartSingleFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.1f, 2);
                anim.Play("FX_Iris_RedPulsateFast");
                resetIris = false;
                break;
            case "DeskSlam":
                deskSlamFX.Play();
                cameraShake.SetShake(0.1f, 4);
                anim.Play("FX_Color_Flash");
                resetColor = false;
                break;
            case "Doritos":
                doritosFX.Play();
                break;
            case "Sad":
                sadFX.Play();
                break;
            case "ShowSepia":
                anim.Play("Sepia_Show");
                resetSepia = false;
                break;
            case "HideSepia":
                anim.Play("Sepia_Hide");
                resetSepia = false;
                break;
            case "ShowBlack":
                anim.Play("FX_Color_FadeInBlack");
                resetColor = false;
                break;
            case "HideBlack":
                anim.Play("FX_Color_FadeOutBlack");
                resetColor = false;
                break;
            case "ShowWhite":
                anim.Play("FX_Color_FadeInWhite");
                resetColor = false;
                break;
            case "HideWhite":
                anim.Play("FX_Color_FadeOutWhite");
                resetColor = false;
                break;
            case "IncrementEnding":
                int endingIndex = anim.GetInteger("EndingIndex");
                anim.SetInteger("EndingIndex", endingIndex + 1);

                if(endingIndex == 2)
                {
                    anim.Play("FX_Iris_RedPulsateFast");
                    resetIris = false;
                }
                else if(endingIndex >= 3)
                {
                    anim.Play("FX_Color_FadeInWhite");
                    resetColor = false;
                }

                break;
            default:
                break;
        }

        if (resetColor) anim.Play("FX_Color_Hidden");
        if (resetIris) anim.Play("FX_Iris_Hidden");
        if (resetSepia) anim.Play("Sepia_Hidden");
        if (resetSteam) steamFX.Stop();
    }
}
