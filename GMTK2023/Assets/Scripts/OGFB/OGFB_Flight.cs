using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OGFB
{
    public class OGFB_Flight : MonoBehaviour
    {
        private bool started;
        private bool active = true;

        [SerializeField] private float vel = 1.5f;
        [SerializeField] private float rotationSpeed = 10f;

        public Vector3 localOrigin;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Animator anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            localOrigin = transform.localPosition;
            SetStarted(false);
        }

        private void Update()
        {
            if (active && Mouse.current.leftButton.wasPressedThisFrame)
            {
                rb.velocity = Vector2.up * vel;
                if (!started) SetStarted(true);
            }
        }

        private void FixedUpdate()
        {
            if(started)
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
        }

        public void ResetFlight()
        {
            SetStarted(false);
            active = true;
            transform.localPosition = localOrigin;
            anim.SetBool("Active", true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            active = false;
            anim.SetBool("Active", false);
            OGFB_GameManager.instance.GameOver();
        }

        private void SetStarted(bool set)
        {
            //rb.bodyType = set ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            started = set;
        }
    }
}
