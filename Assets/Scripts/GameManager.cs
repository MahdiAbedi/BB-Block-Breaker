using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public Transform[] SpawnHolders;
    public GameObject squreBrick,trianlgeBrick,extraBall,ball;
    public int bricksNeeded=1;
    private List<GameObject> bricks = new List<GameObject>();
    private List<GameObject> balls= new List<GameObject>();
    public bool bricksCanMoveDown=false;

    // Use this for initialization
    void Awake () {
        MakeBrickes();
        CreateBalls();
        if (!instance)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
      
        if (bricksCanMoveDown)
        {
            MoveBrickesDown();
            CreateBalls();
            bricksCanMoveDown = false;
            RepostionBricks();
        }
   
    }

    void RepostionBricks()
    {
        foreach (var ball in balls)
        {
            ball.transform.position = new Vector2(0,-4);
        }
    }
    public void CheckAllBallsGrounded()
    {
        foreach (var ball in balls)
        {
            if (ball.GetComponent<BallScript>().currentBallState==BallScript.BallState.fired)
            {
                Debug.Log(ball.GetComponent<BallScript>().currentBallState);
                bricksCanMoveDown = false;
                return;
            }
            else
            {
                bricksCanMoveDown = true;
                
            }
            
        }
        

    }
    public void MoveBrickesDown()
    {
        
            foreach (var brick in bricks)
            {
                //if brick is destroyed don't work with it
                if (brick)
                {
                    brick.transform.position = new Vector2(brick.transform.position.x, brick.transform.position.y - 1);

                }
            }
            Ground.instance.bricksCanMoveDown = false;
          
            MakeBrickes();
        
        
    }
    void MakeBrickes()
    {
        bricksNeeded = Random.Range(1,3);
        shuffle(SpawnHolders);
        int createdBrickes = 0;
    
        for (int i = 0; i < SpawnHolders.Length; i++)
        {
            float rnd = Random.Range(0, 2);
            
            if (rnd == 0 && createdBrickes < bricksNeeded)
            {
               bricks.Add(Instantiate(squreBrick, new Vector3(SpawnHolders[i].position.x, SpawnHolders[i].position.y, 0), Quaternion.identity));
                createdBrickes++;
            }
            else if (rnd == 1 && createdBrickes < bricksNeeded)
            {
                bricks.Add(Instantiate(trianlgeBrick, new Vector3(SpawnHolders[i].position.x, SpawnHolders[i].position.y, 0), Quaternion.identity));
                createdBrickes++;
            }
        }//end of for
        bricks.Add(Instantiate(extraBall, new Vector3(SpawnHolders[SpawnHolders.Length-1].position.x, SpawnHolders[SpawnHolders.Length-1].position.y, 0), Quaternion.identity));


    }



    void shuffle(Transform[] spawner)
    {
       
        for (int i = 0; i < spawner.Length; i++)
        {
            Transform temp = spawner[i];
            int rnd = Random.Range(i,spawner.Length);
            spawner[i] = spawner[rnd];
            spawner[rnd] = temp;

        }
    }



    public void CreateBalls()
    {
        int ballScore = 1;
        if (BallScript.instance)
        {
             ballScore = BallScript.instance.ballScore;
        }
        for (int i = balls.Count; i <ballScore; i++)
        {
            GameObject myBall = Instantiate(ball, new Vector2(0, -4), Quaternion.identity);
           balls.Add(myBall);
            if (i<0)
            {
                myBall.SetActive(false);
            }
            
            
        }
    }
}
