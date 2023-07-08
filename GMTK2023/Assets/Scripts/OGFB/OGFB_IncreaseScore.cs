using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace OGFB
{
    public class OGFB_IncreaseScore : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OGFB_GameManager.instance.IncrementScore();
        }
    }
}
