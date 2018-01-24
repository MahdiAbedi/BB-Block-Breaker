using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public BallScript ballCtr;
    public bool bricksCanMoveDown = false;
    public static Ground instance;
    // Use this for initialization
    void Start () {
        if (instance==null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D target)
    {

        if (target.gameObject.tag == "Brick")
        {
            Time.timeScale = 0f;
            Debug.Log("game Over");
        }
    }




}
