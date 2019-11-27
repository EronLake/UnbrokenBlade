using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnbrokenBlade
{
    public class CollisionRecoil : MonoBehaviour
    {
        public float recoilVelocity = 10;
        public float recoilAngularVelocity = 10;
        public BladeFollow parentBladeFragment;
        public bool onlyFollowParent = true;

        private BladeFollow thisFollowObject;
        private Rigidbody thisRigidbody;
        private ReturnToBase retrunToBase;

        private void Start()
        {
            try
            {
                thisRigidbody = GetComponent<Rigidbody>();
            }
            catch (NullReferenceException)
            {
                Debug.LogError("CollisionRecoil: " + gameObject.name + " Rigidbody was not set in the inspector!");
            }

            try
            {
                thisFollowObject = GetComponent<BladeFollow>();
            }
            catch (NullReferenceException)
            {
                Debug.LogError("CollisionRecoil: " + gameObject.name + " FollowObject was not set in the inspector!");
            }

            try
            {
                retrunToBase = GetComponent<ReturnToBase>();
            }
            catch (NullReferenceException)
            {
                Debug.LogError("CollisionRecoil: " + gameObject.name + " ReturnToBase was not set in the inspector!");
            }

        }

        private void OnTriggerEnter(Collider other)
        {   //will need to change this check to use a unique identifier for this blade rather than check if return base is attached
            if (other.GetComponent<ReturnToBase>() == null && retrunToBase.countdownStarted == false && thisFollowObject.onBase == true)
            {
                ApplyShatterToThisFragment();
            }
            
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && onlyFollowParent == false)
            {
                ApplyShatterToThisFragment();
            }
     
            if (onlyFollowParent == true && retrunToBase.countdownStarted == false && parentBladeFragment.shouldFollow == false)
            {
                Debug.Log("I am trying to shatter right now");
                ApplyShatterToThisFragment();
            }
        }

        private void ApplyShatterToThisFragment()
        {
            thisRigidbody.velocity = new Vector3(UnityEngine.Random.Range(-recoilVelocity, recoilVelocity), 0, UnityEngine.Random.Range(-recoilVelocity, recoilVelocity));
            thisRigidbody.angularVelocity = new Vector3(UnityEngine.Random.Range(-recoilAngularVelocity, recoilAngularVelocity), 0, UnityEngine.Random.Range(-recoilAngularVelocity, recoilAngularVelocity));
            retrunToBase.StartCountDown();
        }


        public void OnCollisionEnter(Collision collision)
        {   
            Debug.Log(collision.relativeVelocity);
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }

            try
            {
                Debug.Log("we in der dog");
                Rigidbody thisRigidbody = GetComponent<Rigidbody>();
                thisRigidbody.velocity = -collision.relativeVelocity * recoilVelocity;
                retrunToBase.StartCountDown();
            }
            catch (NullReferenceException)
            {
                Debug.LogError("CollisionRecoil: " + gameObject.name + " Rigid body was not set in the inspector!");
            }
        }
    }
}