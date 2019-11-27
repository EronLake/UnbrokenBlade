using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class KeyBoardMovement : MonoBehaviour
    {
        public float speed;
        public Transform avatarView;
        public int jumpHeight = 5;
        public int dashSpeed = 5;

        private Rigidbody avatarRigidbody;
        private int dashMagnifier = 1;


        public float countdownTime = 3f;
        public bool countdownStarted = false;
        private float timer;


        // Update is called once per frame

        private void Start()
        {
            if (avatarView == null)
            {
                throw new System.NullReferenceException("avatarView was not set!");
            }

            avatarRigidbody = GetComponent<Rigidbody>();
            if(avatarRigidbody == null)
            {
                throw new System.NullReferenceException("Avatar's Rigidbody component was not set!");
            }
        }

        void Update()
        {
            //dash
            CountDownTime();
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Debug.Log("Dash!!!");
                StartCountDown();
            }

            //Left
            if (Input.GetKey(KeyCode.A) || OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x < 0)
            {
                Debug.Log("left");
                Vector3 position = this.transform.position;
                Vector3 movementVector = -avatarView.transform.right * speed/100 * dashMagnifier * Mathf.Abs(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x);
                this.transform.position = position + movementVector;
            } 
            //right
            if (Input.GetKey(KeyCode.D) || OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x > 0)
            {
                Debug.Log("right");
                Vector3 position = this.transform.position;
                Debug.Log("position: " + position);
                Vector3 movementVector = avatarView.transform.right * (speed / 100 * dashMagnifier * Mathf.Abs(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x));
                Debug.Log("movmentVector: " + movementVector);
                this.transform.position = position + movementVector;
            } 
            //forward
            if (Input.GetKey(KeyCode.W) || OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y > 0)
            {
                Debug.Log("forward");
                Vector3 position = this.transform.position;
                Vector3 movementVector = avatarView.transform.forward * speed / 100 * dashMagnifier * Mathf.Abs(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y);
                this.transform.position = position + movementVector;
            } 
            //backward
            if (Input.GetKey(KeyCode.S) || OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y < 0)
            {
                Debug.Log("backward");
                Vector3 position = this.transform.position;
                Vector3 movementVector = -avatarView.transform.forward * speed / 100 * dashMagnifier * Mathf.Abs(OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y);
                this.transform.position = position + movementVector;
            }

            //jump
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                Debug.Log("Jump!!!");
                avatarRigidbody.AddForce(Vector3.up * jumpHeight);
            }
        }


        public void StartCountDown()
        {
            countdownStarted = true;
            timer = countdownTime;
            dashMagnifier = dashSpeed;
        }

        //need to make an object for this
        private void CountDownTime()
        {
            if (countdownStarted == true)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    countdownStarted = false;
                    dashMagnifier = 1;
                }
            }
        }
    }
}