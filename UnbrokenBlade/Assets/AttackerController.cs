using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerController : MonoBehaviour
{
    public float attackSpeed = .1f;

    private Vector3 startingPosition;

    // Update is called once per frame

    private void Start()
    {
        startingPosition = transform.position; 
    }

    void Update()
    {
        OVRInput.Update();
        if (Input.GetKey(KeyCode.Space) || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            transform.position = startingPosition;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - attackSpeed);
    }
}
