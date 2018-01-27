using UnityEngine;
using System.Collections;

public class flowingDown : MonoBehaviour {
    public GameObject movingBG;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0f, -2f, 0f) * 10 * Time.deltaTime;
        movingStreet();
    }

    void movingStreet()
    {
        float bgDimension = movingBG.GetComponent<Collider>().bounds.size.y;
        Vector3 generationPoint = new Vector3(movingBG.transform.position.x, bgDimension, 0f);

        if ((this.transform.position.y < 0)&&(this.transform.position.y>-1))
        {
         //   Debug.Log("Street generated");
            Instantiate(movingBG, generationPoint, Quaternion.Euler(0, 0, 90f));
        }
        if (this.transform.position.y < -19){
            Destroy(this);
        }
    }
}
