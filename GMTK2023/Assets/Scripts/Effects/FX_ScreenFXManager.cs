using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FX_ScreenFXManager : MonoBehaviour
{
    public static FX_ScreenFXManager instance;

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

    private Animator anim;

    public enum FX_ScreenFXType { None, Suprised, Shock, Confused, Extreme_Shock, Angry, Cutesy, Love, Saucy, Heartbeat, DeskSlam, Doritos };
    [Header("Debug")]
    [SerializeField] private FX_ScreenFXType debugType;

    private void Awake()
    {
        if (instance == null) instance = this;
        anim = GetComponent<Animator>();
        cameraShake = GetComponent<FX_CameraShake>();
    }

    private void OnValidate()
    {
        if(EditorApplication.isPlaying)
            RunScreenFX(debugType);
    }

    private void RunScreenFX(FX_ScreenFXType fxType)
    {
        bool resetColor = true;
        bool resetIris = true;
        bool resetSteam = true;
        anim.SetBool("ShowRoses", fxType == FX_ScreenFXType.Love);

        switch(fxType)
        {
            case FX_ScreenFXType.Suprised:
                exclaimationFX.Play();
                suprisedFX.Play();
                cameraShake.SetShake(0.1f, 2);
                break;
            case FX_ScreenFXType.Shock:
                sparksFX.Play();
                suprisedFX.Play();
                cameraShake.SetShake(0.1f, 4);
                anim.Play("FX_Color_Flash");
                resetColor = false;
                break;
            case FX_ScreenFXType.Confused:
                questionFX.Play();
                break;
            case FX_ScreenFXType.Extreme_Shock:
                sparksFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.3f, 6);
                anim.Play("FX_Color_DoubleFlash");
                resetColor = false;
                break;
            case FX_ScreenFXType.Angry:
                animeTwinkleFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.1f, 2);
                anim.Play("FX_Iris_RedStill");
                anim.Play("FX_Color_Flash");
                resetColor = false;
                resetIris = false;
                break;
            case FX_ScreenFXType.Cutesy:
                flowersFX.Play();
                anim.Play("FX_Iris_PinkStill");
                anim.Play("FX_Color_PinkTint");
                resetColor = false;
                resetIris = false;
                break;
            case FX_ScreenFXType.Love:
                rosesFX.Play();
                anim.Play("FX_Iris_PinkStill");
                anim.Play("FX_Color_PinkTint");
                resetColor = false;
                resetIris = false;
                break;
            case FX_ScreenFXType.Saucy:
                steamFX.Play();
                anim.Play("FX_Iris_RedPulsate");
                resetIris = false;
                resetSteam = false;
                break;
            case FX_ScreenFXType.Heartbeat:
                heartSingleFX.Play();
                impactFX.Play();
                cameraShake.SetShake(0.1f, 2);
                anim.Play("FX_Iris_RedPulsateFast");
                resetIris = false;
                break;
            case FX_ScreenFXType.DeskSlam:
                deskSlamFX.Play();
                cameraShake.SetShake(0.1f, 4);
                anim.Play("FX_Color_Flash");
                resetColor = false;
                break;
            case FX_ScreenFXType.Doritos:
                doritosFX.Play();
                break;
            default:
                break;
        }

        if (resetColor) anim.Play("FX_Color_Hidden");
        if (resetIris) anim.Play("FX_Iris_Hidden");
        if (resetSteam) steamFX.Stop();
    }
}
