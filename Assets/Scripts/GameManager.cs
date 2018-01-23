using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform[] SpawnHolders;
    public GameObject squreBrick,trianlgeBrick;
    public int createdBrickes=0, bricksNeeded=3;
    
	// Use this for initialization
	void Start () {
        for (int i = 0; i < SpawnHolders.Length; i++)
        {
            float rnd = Random.Range(0,2);
            if (rnd==0 && createdBrickes<bricksNeeded)
            {
                Instantiate(squreBrick, new Vector3(SpawnHolders[i].position.x, SpawnHolders[i].position.y, 0), Quaternion.identity);
                createdBrickes++;
            }
            else if (rnd==1 && createdBrickes < bricksNeeded)
            {
                Instantiate(trianlgeBrick, new Vector3(SpawnHolders[i].position.x, SpawnHolders[i].position.y, 0), Quaternion.identity);
                createdBrickes++;
            }
                    

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
