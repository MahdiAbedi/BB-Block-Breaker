using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallScript : MonoBehaviour {

    public static BallScript instance;
    public float ballSpeed = 12f;
    public Rigidbody2D myBody;
    private Vector2 mouseStartPosition, mouseEndPosition;
    private Vector2 ballVelocity;
    public GameObject arrow;
    public GameObject ballScoreText;
    public int ballScore=1;


    public enum BallState
    {
        aim,fired,waiting
    }

    public BallState currentBallState; 

    void Start()
    {
        currentBallState = BallState.aim;
        //ballScoreText.GetComponent<Text>().text = "*" + ballScore;
    }
    // Use this for initialization
    void Awake () {
        myBody = GetComponent<Rigidbody2D>();
        if (!instance)
        {
            instance = this;
        }
        else
        {
           // Destroy(instance);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (currentBallState)
        {
            case BallState.aim:
                if (ballScore>1)
                {
                    ballScoreText.GetComponent<Text>().text = "*" + ballScore;
                    ballScoreText.SetActive(true);
                }
                
                if (Input.GetMouseButton(0))
                {
                    MouseHoldDown();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    MouseLeftClicked();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    MouseReleased();
                }
                break;
            case BallState.fired:
                ballScoreText.SetActive(false);
                break;
            case BallState.waiting:
                break;
            default:
                break;
        }
    }


    void MouseHoldDown()
    {
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX = (mouseStartPosition.x - tempMousePosition.x);
        float diffY = (mouseStartPosition.y - tempMousePosition.y);

        if (diffY <= 0)
        {
            diffY = 0.01f;
        }

        float theta = Mathf.Rad2Deg* Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    void MouseLeftClicked()
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentBallState = BallState.aim;
    }

    void MouseReleased()
    {
        arrow.SetActive(false);
        mouseEndPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ballVelocity.x = (mouseStartPosition.x - mouseEndPosition.x);
        ballVelocity.y= (mouseStartPosition.y - mouseEndPosition.y);
        myBody.velocity = ballVelocity.normalized*ballSpeed;
        if (myBody.velocity==Vector2.zero)
        {
            return;
        }
        currentBallState = BallState.fired;

    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
          myBody.velocity = Vector2.zero;
          currentBallState = BallState.aim;
            GameManager.instance.CheckAllBallsGrounded();
        }
    }
    
}
