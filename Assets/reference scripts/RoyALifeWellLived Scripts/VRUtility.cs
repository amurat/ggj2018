using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using UnityEngine.VR;   //you always need this to use special VR functions 

public class VRUtility : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 0.50f;     //adjust rendering quality. Higher quality results in lower framerates, lower quality moves more smoothly and quickly
        UnityEngine.XR.InputTracking.Recenter();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.XR.InputTracking.Recenter();       //recenters the definition of "forward" for VR
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
