using UnityEngine;
using System.Collections;

public class simonMemory : MonoBehaviour {
    public GameObject greenOn;
    public GameObject redOn;
    public GameObject blueOn;
    public GameObject greenOff;
    public GameObject redOff;
    public GameObject blueOff;
    public Renderer renderman;
    GameObject clickedThing;
    float timer = 0;
    int phase = 1;
    int[] simonic = new int[10];

	// Use this for initialization
	void Start () {
        fillSimonic();
	}
	
	// Update is called once per frame
	void Update () {
        clickSimon();
        timeDelay();
        restart();
	}

    void fillSimonic()
    {
        for (int i = 0; i < simonic.Length; i++)
        {
            int randomizer = Random.Range(0,3);
            simonic[i] = randomizer;
   //         Debug.Log(randomizer);
        }
    }

    void glowSimon()
    {
        for (int i = 0; i < phase; i++)
        {
            if (simonic[i] == 0)
            {
                redOn.GetComponent<Renderer>().enabled = true;
            }
            else if (simonic[i] == 1)
            {
                greenOn.GetComponent<Renderer>().enabled = true;

            }
            else if (simonic[i] == 2)
            {
                blueOn.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    void clickSimon()
    {
        //Debug.Log(Input.mousePosition);		//check mouse position
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);	//provides mouse position in GAME coordinates
            Vector3 outGoing = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 1000f);
            Vector3 fromPlayer = Camera.main.ScreenToWorldPoint(outGoing);

            Debug.DrawRay(mousePos, fromPlayer, Color.red);			//visual of ray (only in scene or w/ gizmos, not in game)
            //           Debug.Log(Physics.Raycast(mousePos, fromPlayer, 1000f));	//check if ray collides with something
            Ray ray = new Ray(mousePos, fromPlayer);
            RaycastHit hit = new RaycastHit();
            if ((Physics.Raycast(ray, out hit, 1000f)))
            {
                renderman = hit.collider.gameObject.GetComponent<Renderer>();
                //          Debug.Log(hit.collider.gameObject.name);

                if ((hit.collider.gameObject.name == "redLight") || (hit.collider.gameObject.name == "greenLight") || (hit.collider.gameObject.name == "blueLight"))
                {
                    renderman.enabled = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            renderman.enabled = false;
        }
    }

    void timePass()
    {
        timer += Time.deltaTime;
    //    Debug.Log(timer);
    }

    IEnumerator timeDelay()
    {
        glowSimon();
        yield return new WaitForSeconds(2);
    }

    void restart()
    {
        //reset the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

}
