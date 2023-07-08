using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OGFB
{
    public class OGFB_GameManager : MonoBehaviour
    {
        public static OGFB_GameManager instance;

        //UI
        [SerializeField] private GameObject ui_GameOverPage;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;

        //Reset references
        [SerializeField] private OGFB_Flight birdScript;
        [SerializeField] private OGFB_PipePooler pipePooler;
        [SerializeField] private OGFB_MoveGround moveGround;

        private int score;

        private void Awake()
        {
            if(instance == null) instance = this;
        }

        private void Update()
        {
            if(ui_GameOverPage.activeInHierarchy)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                    ResetGame();
            }
        }

        public void IncrementScore()
        {
            score++;
            scoreText.text = score.ToString();
        }

        public void ResetScore()
        {
            score = 0;
            scoreText.text = score.ToString();
        }

        public void GameOver()
        {
            ui_GameOverPage.SetActive(true);
            pipePooler.StopPipes();
            moveGround.SetMoving(false);
        }

        public void ResetGame()
        {
            birdScript.ResetFlight();
            pipePooler.ResetPipes();
            ui_GameOverPage.SetActive(false);
            moveGround.SetMoving(true);
            ResetScore();
        }
    }
}
