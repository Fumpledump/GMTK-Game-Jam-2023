using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OGFB
{
    public class OGFB_Flight : MonoBehaviour
    {
        private bool physicsActive;
        private bool isControllable = true;

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
            SetPhysicsActive(false);
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            if(physicsActive)
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationSpeed);
        }

        public void ResetFlight()
        {
            SetPhysicsActive(false);
            isControllable = true;
            transform.localPosition = localOrigin;
            anim.SetBool("Active", true);
        }

        public void Flap(bool force)
        {
            if (physicsActive && isControllable || force)
            {
                rb.velocity = Vector2.up * vel;
            }
        }

        public void SetPhysicsActive(bool set)
        {
            rb.bodyType = set ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            physicsActive = set;

            if (!set)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
                spriteRenderer.transform.rotation = Quaternion.identity;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "OGFB_Barrier")
                return;

            isControllable = false;
            anim.SetBool("Active", false);
            OGFB_GameManager.instance.GameOver();
        }
    }
}
