using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class FollowObject : MonoBehaviour
    {
        public GameObject followTarget;
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
        public float onBaseDistance = .1f;

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
        }

        // Update is called once per frame
        private void Update()
        {
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
                } else
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
            Vector3 followPosition = followTarget.transform.position + followTarget.transform.up * positionOffsetY;
            transform.position = followPosition;

            transform.rotation = followTarget.transform.rotation;
            transform.Rotate(rotationOffsetX, 0, 0, Space.Self);
        }

        private void LerpFollow()
        {
            if (position)
            {
                float postionLerpFraction = .01f * activePostionFollowSpeed;
                //offset the position of the object we are following 
                Vector3 followPosition = followTarget.transform.position + followTarget.transform.up * positionOffsetY;
                // Set our position as a fraction of the distance between the markers.
                transform.position = Vector3.Lerp(transform.position, followPosition, postionLerpFraction);
            }

            if (rotation)
            {
                float rotationLerpFraction = .01f * activeRotationFollowSpeed;
                // Set our position as a fraction of the distance between the markers.

                transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.transform.rotation, rotationLerpFraction);
                transform.Rotate(rotationOffsetX, 0, 0, Space.Self);
            }
        }

        private void SetIfOnBase()
        {
            if (Mathf.Abs((transform.position - followTarget.transform.position).magnitude) > onBaseDistance)
            {
                activePostionFollowSpeed = postionReturnSpeed;
                activeRotationFollowSpeed = rotationReturnSpeed;
                onBase = false;
            } else
            {
                activePostionFollowSpeed = postionFollowSpeed;
                activeRotationFollowSpeed = rotationFollowSpeed;
                onBase = true;
            }
        }
    }
}