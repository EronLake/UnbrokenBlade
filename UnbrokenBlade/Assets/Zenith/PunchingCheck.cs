using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingCheck : MonoBehaviour { 

public float punchMagnifier;
    public float minthreashold;
    public GameObject avatar;
    public float maxPunchPower;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided -----------------------");

        Rigidbody actorRidgedBody = GetComponent<Rigidbody>();
        if (actorRidgedBody == null)
        {
            throw new System.NullReferenceException("The actor does not have a rigidBody attached!");
        }

        float punchPower = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTrackedRemote).magnitude;
        //Vector3 punchDirection = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTrackedRemote).normalized;
        Vector3 punchDirection = avatar.transform.forward;
        Debug.Log("punchPower" + punchPower);
        Debug.Log("punchDirection" + punchPower);

        Rigidbody targetRidgedBody = collision.gameObject.GetComponent<Rigidbody>();
        Debug.Log("target " + collision.gameObject);
        if (targetRidgedBody != null)
        {
            if (punchPower > minthreashold)
            {
                Debug.Log("Punched");
                targetRidgedBody.velocity += punchDirection * Mathf.Min((punchPower * punchMagnifier), maxPunchPower);

                BreakMovingObject breakableObject = collision.gameObject.GetComponent<BreakMovingObject>();
                breakableObject.SetLaucherForThisObject(gameObject);
            }
        }
        else
        {
            throw new System.NullReferenceException("The target does not have a rigidBody attached! " + collision.gameObject);
        }
        Debug.Log("done -----------------------");
    }

}
