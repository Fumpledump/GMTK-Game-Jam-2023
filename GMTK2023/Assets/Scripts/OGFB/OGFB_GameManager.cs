using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

namespace OGFB
{
    public class OGFB_GameManager : MonoBehaviour
    {
        public static OGFB_GameManager instance;

        [SerializeField] private DialogueRunner dialogueRunner; // Yarn Spinner Dialogue Runner

        //UI
        [SerializeField] private GameObject ui_GameOverPage;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;

        [SerializeField] private Animator uiAnim;

        //Reset references
        [SerializeField] private OGFB_Flight birdScript;
        [SerializeField] private OGFB_PipePooler pipePooler;
        [SerializeField] private OGFB_MoveGround moveGround;

        private bool gameRunning;
        private bool allowControl;

        private int score;

        private void Awake()
        {
            if(instance == null) instance = this;

            if(dialogueRunner != null)
                dialogueRunner.AddCommandHandler<bool>("SetPhoneShown", SetPhoneShown);
        }

        private void Update()
        {
            //Show/Hide Phone (Shouldnt be available when allow control is off)
            if(allowControl && Mouse.current.rightButton.wasPressedThisFrame 
                || !allowControl && uiAnim.GetBool("Show"))
            {
                SetPhoneShown(!uiAnim.GetBool("Show"));
            }
        }

        public void IncrementScore()
        {
            score++;
            scoreText.text = score.ToString();
            if (AudioManager.Instance != null) AudioManager.Instance.Play("OGFB_Point");
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
            if (!ui_GameOverPage.activeInHierarchy && AudioManager.Instance != null) AudioManager.Instance.Play("OGFB_Death");
            ui_GameOverPage.SetActive(true);
            pipePooler.StopPipes();
            moveGround.SetMoving(false);
        }

        public void StartGame()
        {
            if (!gameRunning)
                ResetGame();

            gameRunning = true;
            birdScript.SetPhysicsActive(true);
            birdScript.Flap(true);
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

        //Phone interaction
        public void SetPhoneShown(bool set)
        {
            if (!set)
                ResetGame();

            uiAnim.SetBool("Show", set);
        }

        public void OnPhonePress()
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

        public void SetAllowControl(bool set)
        {
            allowControl = set;
        } 
    }
}
