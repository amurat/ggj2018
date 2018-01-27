using UnityEngine;
using System.Collections;

public class moveMain : MonoBehaviour {

	public GameObject platform;
	public AudioClip disintegrate;
	public AudioClip sproing;

	// Use this for initialization
	void Start () {
	/*	LineRenderer lineRenderer = gameObject.AddComponent("LineRenderer") as LineRenderer;
		lineRenderer.material = new Material(Shader.Find ("Particles/Additive"));
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetVertexCount (2);*/
	}
	
	// Update is called once per frame
	void Update () {
		//movement controls for left, right, and jumping
		if ((Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A))){	
			transform.position += new Vector3(-1f, 0f, 0f) * 10*Time.deltaTime;
			transform.rotation = Quaternion.Euler (0, 90f, 0);

		}
		if ((Input.GetKey (KeyCode.RightArrow)) || (Input.GetKey (KeyCode.D))){
			transform.rotation = Quaternion.Euler (0, -90f, 0);
			transform.position += new Vector3(1f, 0f, 0f) * 10*Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.DownArrow))||(Input.GetKey (KeyCode.S))){
			transform.rotation = Quaternion.Euler (0,0,0);
		}
		if ((Input.GetKeyDown (KeyCode.UpArrow)) || (Input.GetKeyDown (KeyCode.W))){	//jump limited by a raycast downward
		//	transform.position += new Vector3(0f, 1f, 0f) * 10*Time.deltaTime;
			Vector3 downward = transform.TransformDirection (Vector3.down);
			GetComponent<AudioSource>().PlayOneShot (sproing);
			if(Physics.Raycast(transform.position, downward, 2f)){
			//	Debug.Log ("YES jump");
				GetComponent<Rigidbody>().AddForce (0f, 500f, 0f);

				//AudioSource.PlayClipAtPoint (sproing, thePlayer.transform.position);
			}
			else{
			//	Debug.Log ("NO jump");
			}
		}
		//reset the level
		if (Input.GetKeyDown (KeyCode.R)){		
			Application.LoadLevel (Application.loadedLevel);
		}
		//destruction ray is short range, involves a raycast between the robot and the mouse position, activated with left mouse button
//THIS IS ALL CODE ATTEMPTS AT GETTING THE DAMN DESTRUCTION RAY WORKING!!! 
		//Debug.Log (Input.mousePosition);		//check mouse position
		if (Input.GetMouseButton (0)){
			//Vector3 toMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);	//provides mouse position in GAME coordinates, doesn't collide because it comes out on Z despite it also being 0(?)
			Vector3 toMouse = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2, Input.mousePosition.z);	//provides mouse position in SCREEN coordinates, doesn't seem to be quite right either
			Vector3 fromBody = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
			Debug.DrawRay (transform.position, toMouse, Color.red);			//visual of ray (only in scene or w/ gizmos, not in game)
			Debug.Log (Physics.Raycast (transform.position, toMouse, 7f));	//check if ray collides with something
		/*	LineRenderer lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.SetColors(Color.red, Color.red);
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition (1, toMouse);
			Destroy (lineRenderer);*/
		//	Ray ray = new Ray(thePlayer.transform.position, (toMouse - thePlayer.transform.position).normalized);		//create a ray, origin at player and direction calculated by 
			Ray ray = new Ray(fromBody,(toMouse - transform.position).normalized);
			RaycastHit hit = new RaycastHit();
			//Debug.Log (hit.collider.name);			//check the name of the object, constantly throws NullReferenceException
			if ((Physics.Raycast(ray, out hit, 7f))){//&&(hit.collider.gameObject.Equals (platform)))

				Destroy (hit.collider.gameObject);		//Destroys anything it touches, including walls and exit

				//audio.PlayOneShot(disintegrate, 1);
				//AudioSource.PlayClipAtPoint (disintegrate, thePlayer.transform.position);
			}
		}
	}
}

		/*
		Ray ray = new Ray(transform.position, (toMouse-transform.position)/((toMouse-transform.position).magnitude));

		Debug.Log (hit.collider.tag);


		if (Input.GetMouseButtonDown (0)){

			//if (Physics.Raycast (ray, out hit, 5f)){
				//if (hit.collider.gameObject.name == "platformBase(Clone)"){
					Destroy(gameObject);
				//}
			//}
		}

		if(Input.GetKeyDown (KeyCode.Mouse0)){
			Debug.Log ("X: "+Input.mousePosition.x+", Y: "+Input.mousePosition.y+", Z: "+Input.mousePosition.z);
		}
	if (Physics.Raycast (transform.position, toMouse, 5f) && (platform.transform.position.x == Input.mousePosition.x) && (platform.transform.position.y == Input.mousePosition.y)){
			//transform.LookAt (rayHit.point);
			//Debug.Log ("Platform disintegrated");
			Destroy (platform);
		}
*/