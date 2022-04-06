using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Shoot balls prefabs
    public GameObject redball;
    public GameObject yellowball;
    public GameObject greenball;
    public GameObject blueball;

    public GameObject rowsHandler;

    private GameObject currentBall;
    private GameObject previousBall;

    private float speed = 10.0f;
    private float previousTime = 0.0f;
    private float coolDown = 0.3f;
    private bool afterClickFlag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Spawns new ball after little delay to avoid attraction in start area
        if (Time.time - previousTime > coolDown && afterClickFlag)
        {
            previousBall = currentBall;
            currentBall = Instantiate(randomBall(), transform.position, Quaternion.Euler(Vector3.zero));
            currentBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            if (previousBall != null)
            {
                previousBall.transform.SetParent(rowsHandler.transform);
            }
            afterClickFlag = false;
        }
        // Shoots new ball with constant speed
        if (Input.GetMouseButtonDown(0) && (Time.time - previousTime > coolDown) && Input.mousePosition.y < Screen.height - Screen.height / 8)
        {
            currentBall.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Vector2 force = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            force.Normalize();
            currentBall.GetComponent<Rigidbody2D>().AddForce(force * speed, ForceMode2D.Impulse);
            previousTime = Time.time;
            afterClickFlag = true;
        }
    }

    // Generates ball with random color
    private GameObject randomBall()
    {
        int ballType = Random.Range(1, 5);
        if(ballType == 1)
        {
            return redball;
        }else if(ballType == 2)
        {
            return yellowball;
        }
        else if (ballType == 3)
        {
            return greenball;
        }
        else if (ballType == 4)
        {
            return blueball;
        }
        return redball;
    }
}
