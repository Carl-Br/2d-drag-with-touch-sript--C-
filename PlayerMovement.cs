 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float fingerMargin=0.8f;// is an area around the Touch
    
    public double fingerCheck= 0.00000000000001; //Checks if he uses an other finger every [fingerCheck] seconds,
    private double fingerCheckTimer;
    Vector2 touchPosDelay;//The Touche positionin [fingerCheck] seconds before.
    private float deltaX, deltaY;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        fingerCheckTimer = fingerCheck;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            
            Touch touch1 = Input.GetTouch(0);
            
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch1.position);
            Debug.DrawLine(new Vector3(touchPos.x - fingerMargin,touchPos.y+fingerMargin,0), new Vector3(touchPos.x + fingerMargin, touchPos.y + fingerMargin, 0),Color.red); // Draws a line, where the user touches the screen (look scene view)
            Debug.DrawLine(new Vector3(touchPos.x - fingerMargin, touchPos.y - fingerMargin, 0), new Vector3(touchPos.x + fingerMargin, touchPos.y - fingerMargin, 0),Color.red);// Draws a line, where the user touches the screen (look scene view)
            Debug.DrawLine(new Vector3(touchPos.x - fingerMargin, touchPos.y + fingerMargin, 0), new Vector3(touchPos.x - fingerMargin, touchPos.y - fingerMargin, 0), Color.red);//Draws a line, where the user touches the screen (look scene view)
            Debug.DrawLine(new Vector3(touchPos.x + fingerMargin, touchPos.y + fingerMargin, 0), new Vector3(touchPos.x + fingerMargin, touchPos.y - fingerMargin, 0), Color.red);//Draws a line, where the user touches the screen (look scene view)

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;

                case TouchPhase.Canceled:
                    rb.velocity = Vector2.zero;
                    break;

            }

            if (fingerCheckTimer > 0) //Timer
            {
                fingerCheckTimer -= Time.deltaTime;//timer
            }
            if (fingerCheckTimer <= 0)//Timer
            {
                

                if ((touchPosDelay.x + fingerMargin < touchPos.x )||( touchPosDelay.y + fingerMargin < touchPos.y) || (touchPosDelay.x - fingerMargin > touchPos.x )||( touchPosDelay.y - fingerMargin > touchPos.y)) //if the the finger position is out of the range of the finger margin, so the user placed another finger while touching
                {
                    
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;

                }
                touchPosDelay = Camera.main.ScreenToWorldPoint(touch1.position); // the position of the touch [fingerCheck] seconds before
                
                fingerCheckTimer = fingerCheck;//Timer
            }
            
            

        }
        
    }
}
