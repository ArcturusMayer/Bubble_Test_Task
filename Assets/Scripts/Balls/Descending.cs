using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descending : MonoBehaviour
{
    private float speed = 0.1f;
    private float borderSpeed = 2.0f;
    private Vector2 screenSize;

    // Start is called before the first frame update
    void Start()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        // Moves all balls down, useful for big waves, not in use now
        if (transform.position.y > screenSize.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        // Keeps all balls between screen sides
        if(transform.position.x > screenSize.x)
        {
            transform.Translate(Vector2.left * Time.deltaTime * borderSpeed);
        }
        if (transform.position.x < -screenSize.x)
        {
            transform.Translate(Vector2.right * Time.deltaTime * borderSpeed);
        }
    }
}
