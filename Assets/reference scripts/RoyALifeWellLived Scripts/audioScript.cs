using UnityEngine;
using System.Collections;

public class audioScript : MonoBehaviour {
    public GameObject player;
    public AudioSource soundSource;
    public AudioClip silencio;
    public AudioClip startLoop;
    public AudioClip sceneABed;
    public AudioClip sceneBSchool;
    public AudioClip sceneCAStadium;
    public AudioClip sceneDMarriage;
    public AudioClip sceneEDiag;
    public AudioClip sceneFCancer;
    public AudioClip sceneGSurvive;
    public AudioClip sceneHEndCarpetStore;
    AudioClip prevClip;
    bool soundLimit = false;
    Vector3 prevPos;
    Vector3 posStart = new Vector3(0f, 3.581f, 0f); //player position at start screen
    Vector3 posSceneA = new Vector3(-14.344f, 4.77f, 50.135f); //player position in childhood bed
    Vector3 posSceneB = new Vector3(4.2f, 2f, -79.5f); //player position in classroom as student
    Vector3 posSceneC_A = new Vector3(-370.8f, 2f, -20.5f); //player position at stadium
    

	// Use this for initialization
	void Start () {
        //soundSource.PlayOneShot(startLoop);
	}

	// Update is called once per frame
	void Update () {
        

           if (prevPos != player.transform.position)
           {
               if (soundLimit == false)
               {
                   if (player.transform.position == posStart)
                   {
                       soundSource.PlayOneShot(startLoop);
                   }
                   if (player.transform.position == posSceneA)
                   {
                       if (soundSource.isPlaying == true)
                       {
                           soundSource.clip = sceneABed;
                           soundSource.Play();
                       }
                   }
                   if (player.transform.position == posSceneB)
                   {
                       if (soundSource.isPlaying == true)
                       {
                           soundSource.clip = sceneBSchool;
                           soundSource.Play();
                       }
                   }
                   if ((player.transform.position.x >= -370.8f)&&(player.transform.position.x <= -337.39f))
                   {
                       if (soundSource.isPlaying == true)
                       {
                           soundSource.clip = sceneCAStadium;
                           soundSource.Play();
                       }
                       else
                       {
                           soundSource.clip = sceneCAStadium;
                           soundSource.Play();
                       }
                   }

               }
           }
           prevClip = soundSource.clip;
           prevPos = player.transform.position;
           if (soundSource.isPlaying == false)
           {
               soundSource.clip = silencio;
               soundSource.Play();
           }
	}
}
