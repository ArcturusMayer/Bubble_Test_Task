using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBalls : MonoBehaviour
{
    private float power = 5.0f;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // attracts balls touched by triggers
    private void OnTriggerStay2D(Collider2D collision)
    {
        body.AddForce((collision.transform.position - gameObject.transform.position).normalized * power);
    }
}
