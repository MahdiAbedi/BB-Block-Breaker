using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bricks : MonoBehaviour {

    public static Bricks instansiate;
    public Gradient gradian;
    public SpriteRenderer breakSprite;
    private Text scoreTxt;
    public static int score=3;
    private int breakHealth;
   

    void Awake()
    {
        if (instansiate==null)
        {
            instansiate = this;
        }
    }
	// Use this for initialization
	void Start () {
        breakHealth = score;
        breakSprite = GetComponent<SpriteRenderer>();
        breakSprite.color = gradian.Evaluate(Random.Range(0, 1));
        scoreTxt = GetComponentInChildren<Text>();
        scoreTxt.text =""+ breakHealth;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag=="Ball")
        {
            breakHealth--;
            if (breakHealth<=0)
            {
                Destroy(gameObject);
                return;
            }
            scoreTxt.text = "" + breakHealth;
        }

    }
}
