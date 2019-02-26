using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnbrokenBlade
{
    public class KeyBoardMovement : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 position = this.transform.position;
                position.x--;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 position = this.transform.position;
                position.y++;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 position = this.transform.position;
                position.y--;
                this.transform.position = position;
            }
        }

    }
}