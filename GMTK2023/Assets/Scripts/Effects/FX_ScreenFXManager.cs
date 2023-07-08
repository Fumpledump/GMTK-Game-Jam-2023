using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_ScreenFXManager : MonoBehaviour
{
    public static FX_ScreenFXManager instance;

    private Animator screenAnim;

    public enum FX_ScreenFXType { Suprised, Shock, Extreme_Shock, Angry, Cutesy, Saucy, Heartbeat, DeskSlam };

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void RunScreenFX(FX_ScreenFXType fxType)
    {
        switch(fxType)
        {
            case FX_ScreenFXType.Suprised:

                break;
            case FX_ScreenFXType.Shock:

                break;
            case FX_ScreenFXType.Extreme_Shock:

                break;
            case FX_ScreenFXType.Angry:

                break;
            case FX_ScreenFXType.Cutesy:

                break;
            case FX_ScreenFXType.Saucy:

                break;
            case FX_ScreenFXType.Heartbeat:

                break;
            case FX_ScreenFXType.DeskSlam:

                break;
        }
    }
}
