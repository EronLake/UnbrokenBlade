using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class AvatarRotation : MonoBehaviour
    {
        public int rotationSpeed = 5;

        // Update is called once per frame
        void Update()
        {
            //turning left
            if(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0)
            {
               Debug.Log("turning right");
               transform.Rotate(Vector3.up, rotationSpeed);
            }

            //turning right
            if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < 0)
            {
                Debug.Log("turning left");
                transform.Rotate(Vector3.up, -rotationSpeed);
            }
        }
    }
}