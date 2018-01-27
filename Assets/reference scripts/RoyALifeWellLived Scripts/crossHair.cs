using UnityEngine;
using System.Collections;

public class crossHair : MonoBehaviour {
    public Texture2D crossEye;
    public GameObject reticle;
    Rect crosshairRect;

	// Use this for initialization
	void Start () {
        //crossEye.width = crossEye.width / 10;
       // crossEye.height = crossEye.height / 10;
	}
	
	// Update is called once per frame
	void Update () {

      //  crosshairRect = new Rect((Screen.width - crossEye.width) / 2.0f, (Screen.height - crossEye.height) / 2.0f, crossEye.width, crossEye.height);
        reticle.transform.position = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * 1f;
        reticle.transform.LookAt(Camera.main.transform.position);
        reticle.transform.Rotate(0f, 0f, 0f);
	}

    void OnGUI()
    {
     //   GUI.DrawTexture(crosshairRect, crossEye);
    }
}
