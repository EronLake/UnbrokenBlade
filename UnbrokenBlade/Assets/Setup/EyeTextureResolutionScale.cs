using UnityEngine;
using System.Collections;
using UnityEngine.XR;


namespace UnbrokenBlade {
    public class EyeTextureResolutionScale : MonoBehaviour
    {
        [SerializeField] private readonly float eyeTextureResolution = 1.5f;              //The render scale. Higher numbers = better quality, but trades performance

        void Start()
        {
            XRSettings.eyeTextureResolutionScale = eyeTextureResolution;
        }
    }
}
