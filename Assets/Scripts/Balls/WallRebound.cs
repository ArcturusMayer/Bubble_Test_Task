using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRebound : MonoBehaviour
{
    private float previousTime = 0.0f;
    private float coolDown = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Reflects ball velocity after touching wall

        Vector2 ballPosition = Camera.main.WorldToScreenPoint(transform.position);
        //right wall bounce
        if(ballPosition.x >= Screen.width && Time.time - previousTime > coolDown)
        {
            previousTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        //left wall bounce
        if(ballPosition.x <= 0 && Time.time - previousTime > coolDown)
        {
            previousTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        //top bounce
        if(ballPosition.y >= Screen.height && Time.time - previousTime > coolDown)
        {
            previousTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        //bottom bounce
        if (ballPosition.y <= 0 && Time.time - previousTime > coolDown)
        {
            previousTime = Time.time;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
