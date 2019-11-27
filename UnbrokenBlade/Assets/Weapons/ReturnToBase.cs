using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class ReturnToBase : MonoBehaviour
    { 
        private BladeFollow followObject;
        public float countdownTime = 3f;
        public bool countdownStarted = false;
        private float timer;

        private void Update()
        {
            CountDownTime();

            try
            {
                followObject = GetComponent<BladeFollow>();
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("ReturnToBase: " + gameObject.name + " FollowObject was not set in the inspector!");
            }


        }

        public void StartCountDown()
        {
            countdownStarted = true;
            timer = countdownTime;
            followObject.shouldFollow = false;
        }

        private void CountDownTime()
        {
            if (countdownStarted == true)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    countdownStarted = false;
                    followObject.shouldFollow = true;
                }
            } 
        }
    }

}