using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMovingObject : MonoBehaviour
{
    public float breakThreshold;
    private GameObject launcher;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == launcher)
        {
            //do not break because this object launched me.
            return;
        }

        Debug.Log("collided /////////////////////////");

        Rigidbody projectileRidgedBody = GetComponent<Rigidbody>();
        if (projectileRidgedBody == null)
        {
            throw new System.NullReferenceException("The actor does not have a rigidBody attached!");
        }

        if (projectileRidgedBody.velocity.magnitude > breakThreshold)
        {
            //play effect here!
            Debug.Log("projectileRidgedBody.velocity.magnitude" + projectileRidgedBody.velocity.magnitude);
            Debug.Log("This broke me!" + collision.gameObject);
            Debug.Log("I am Broken!");
            BreakThisObject();
        }
        Debug.Log("/////////////////////////");
    }

    //you need to call this function if you hit or throw this object so that the object initializing velocity does not break this object 
    //before it has a chance to travel through the air.
    public void SetLaucherForThisObject(GameObject launcher)
    {
        this.launcher = launcher;
    }

    private void BreakThisObject()
    {
        Destroy(gameObject);
    }
}
