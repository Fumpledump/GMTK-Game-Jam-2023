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

        [SerializeField] private Animator uiAnim;

        //Reset references
        [SerializeField] private OGFB_Flight birdScript;
        [SerializeField] private OGFB_PipePooler pipePooler;
        [SerializeField] private OGFB_MoveGround moveGround;

        private bool gameRunning;
        private int score;

        private void Awake()
        {
            if(instance == null) instance = this;

            //Debug: Show for now
            uiAnim.SetBool("Show", true);
        }

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (ui_GameOverPage.activeInHierarchy)
                {
                    ResetGame();
                }
                else if (!gameRunning && uiAnim.GetBool("Show"))
                {
                    StartGame();
                }
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

        public int GetScore()
        {
            return score;
        }

        public void GameOver()
        {
            ui_GameOverPage.SetActive(true);
            pipePooler.StopPipes();
            moveGround.SetMoving(false);
        }

        public void StartGame()
        {
            gameRunning = true;
            birdScript.SetPhysicsActive(true);
            birdScript.Flap();
            pipePooler.SetIsSpawning(true);
        }

        public void ResetGame()
        {
            birdScript.ResetFlight();
            pipePooler.ResetPipes();
            ui_GameOverPage.SetActive(false);
            moveGround.SetMoving(true);
            ResetScore();
            gameRunning = false;
        }
    }
}
