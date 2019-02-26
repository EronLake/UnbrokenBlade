using UnityEngine;
using static UnityEngine.Input;

public class Example : MonoBehaviour
{
    // Prints a joystick name if movement is detected.

    void Update()
    {
        // requires you to set up axes "Joy0X" - "Joy3X" and "Joy0Y" - "Joy3Y" in the Input Manger
        for (int i = 0; i < 2; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2)
            {
                
                Debug.Log(Input.GetJoystickNames()[i] + " is moved");
            }
        }
    }
}