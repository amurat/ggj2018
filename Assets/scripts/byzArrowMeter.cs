using UnityEngine;
using System.Collections;

public class byzArrowMeter : MonoBehaviour {

    private Vector2 offset;

    private Vector2 size;

    public Texture2D backgroundTexture;
    private GUIStyle textureStyle;

    public float percent = 100.0f;

    void Start () {
        if (!backgroundTexture) {
            backgroundTexture = Texture2D.whiteTexture;
        }
        textureStyle = new GUIStyle {normal = new GUIStyleState { background = backgroundTexture } };
        offset.x = 350;
        offset.y = 270;
        size.x = 100;
        size.y = 15;
    }

    void OnGUI () {
        var byzShoot = GetComponent<byzShoot>();
        if (byzShoot) {
            percent = byzShoot.currentCapacity / byzShoot.projectileCapacity;
        }
         bool redZone = (byzShoot.currentCapacity < 1.0f);
         Color meterColor = GUI.color;
         if (redZone) {
             meterColor = Color.red;
         } else {
             meterColor = Color.white;
         }
         var backgroundColor = GUI.backgroundColor;
         GUI.backgroundColor = meterColor;
         offset.y = Screen.height - 20;
         offset.x = Screen.width/2.0f;
         GUI.Box(new Rect(offset.x, offset.y, percent * size.x, size.y), "", textureStyle);
         GUI.backgroundColor = backgroundColor;
    }
}