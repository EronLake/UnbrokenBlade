using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class BladeFollow : MonoBehaviour
    {
        //public GameObject followTarget;
        private Rigidbody thisRigidbody;
        public bool shouldFollow = true;
        public bool shouldInstantFollow = false;

        public bool position = true;
        public float postionFollowSpeed = 100;
        public float postionReturnSpeed = 50;

        public bool rotation = true;
        public float rotationFollowSpeed = 100;
        public float rotationReturnSpeed = 5;
        public float positionOffsetY;
        public float rotationOffsetX;

        private float activePostionFollowSpeed;
        private float activeRotationFollowSpeed;
        public bool onBase = false;
        public float onBaseDistance = .005f;

        public Vector3 followTargetPosition;
        public Quaternion followTargetRotation;

        private void Start()
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody)
            {
                thisRigidbody = rigidbody;

                activePostionFollowSpeed = postionReturnSpeed;
                activeRotationFollowSpeed = rotationReturnSpeed;

            }
            else
            {
                throw new System.NullReferenceException("Could not find rigidbody component on this object!");
            }

            followTargetPosition = transform.localPosition;
            followTargetRotation = transform.localRotation;
        }

        // Update is called once per frame
        private void Update()
        {

            Debug.Log("Lets see who get's called last: " + gameObject.name);
            SetIfOnBase();

            if (thisRigidbody == null)
            {
                throw new System.NullReferenceException("Rigidbody not assigned to FollowObject script");
            }

            if (shouldFollow == true)
            {
                StopCurrentVelocity();
                if (shouldInstantFollow == true)
                {
                    InstantFollow();
                }
                else
                {
                    LerpFollow();
                }
            }
        }

        private void StopCurrentVelocity()
        {
            if (thisRigidbody.velocity.magnitude > 0 || thisRigidbody.angularVelocity.magnitude > 0)
            {
                thisRigidbody.velocity = new Vector3(0, 0, 0);
                thisRigidbody.angularVelocity = new Vector3(0, 0, 0);
            }
        }

        private void InstantFollow()
        {
            Debug.Log("this is instant follow!");
            if (position)
            {
                transform.localPosition = followTargetPosition;
            }

            if(rotation)
            {
                transform.localRotation = followTargetRotation;
                transform.Rotate(rotationOffsetX, 0, 0, Space.Self);
            }
           
        }

        private void LerpFollow()
        {
            if (position)
            {
                float postionLerpFraction = .01f * activePostionFollowSpeed;
                // Set our position as a fraction of the distance between the markers.
                transform.localPosition = Vector3.Lerp(transform.localPosition, followTargetPosition, postionLerpFraction);
            }

            if (rotation)
            {
                float rotationLerpFraction = .01f * activeRotationFollowSpeed;
                // Set our position as a fraction of the distance between the markers.

                transform.localRotation = Quaternion.Lerp(transform.localRotation, followTargetRotation, rotationLerpFraction);
                transform.Rotate(rotationOffsetX, 0, 0, Space.Self);
            }
        }

        private void SetIfOnBase()
        {
            Debug.Log("I am here!");
            if (Mathf.Abs((transform.localPosition - followTargetPosition).magnitude) > onBaseDistance &&
                Mathf.Abs((transform.localRotation.eulerAngles - followTargetRotation.eulerAngles).magnitude) > onBaseDistance)
            {
                activePostionFollowSpeed = postionReturnSpeed;
                activeRotationFollowSpeed = rotationReturnSpeed;
                onBase = false;
                Debug.Log("Not on Base!");
            }
            else
            {
                Debug.Log("on Base!");

                activePostionFollowSpeed = postionFollowSpeed;
                activeRotationFollowSpeed = rotationFollowSpeed;
                onBase = true;

            }
        }
    }
}