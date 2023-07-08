using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OGFB
{
    public class OGFB_MovePipe : MonoBehaviour
    {
        [SerializeField] private float speed = 0.65f;

        private bool active = true;

        private void Update()
        {
            if(active)
                transform.position += Vector3.left * speed * Time.deltaTime;
        }

        public void SetMoving(bool set)
        {
            active = set;
            foreach(BoxCollider2D collider in GetComponentsInChildren<BoxCollider2D>())
            {
                collider.enabled = set;
            }
        }
    }
}

