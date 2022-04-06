using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCheck : MonoBehaviour
{
    public string color = "red";
    public bool isShooted = false; // When true balls will disappear on touch

    private HashSet<GameObject> sameColorTouched = new HashSet<GameObject>(); // Stores balls of same color which in touch with this ball

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Adds touched ball in collection if its same colored. If ball also isShooted = true, starts removing operation.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (color.Equals(collision.gameObject.GetComponent<ColorCheck>().color))
        {
            sameColorTouched.Add(collision.gameObject);
            if (isShooted)
            {
                removeBalls(gameObject);
            }
        }
    }

    // Removes ball from collection if it detaches for some reasons
    private void OnTriggerExit2D(Collision2D collision)
    {
        if (color.Equals(collision.gameObject.GetComponent<ColorCheck>().color))
        {
            sameColorTouched.Remove(collision.gameObject);
        }
    }

    // Starts removing process. Firstly checks, are there 3 or more same colored balls in touch
    private void removeBalls(GameObject collisionBall)
    {
        if (removeOrNot(collisionBall))
        {
            remove(collisionBall, new HashSet<GameObject>());
        }
    }

    // Recursive method, checks all touched each other balls and removes them.
    private void remove(GameObject collisionBall, HashSet<GameObject> set)
    {
        HashSet<GameObject> except = collisionBall.GetComponent<ColorCheck>().sameColorTouched;
        except.ExceptWith(set); // Checks if this balls are not checked already, avoiding StackOverFlow
        set.UnionWith(except);
        foreach (GameObject g in except)
        {
            g.GetComponent<ColorCheck>().sameColorTouched.Remove(collisionBall);
            g.GetComponent<ColorCheck>().remove(g, set);
        }
        collisionBall.GetComponent<ColorCheck>().sameColorTouched.Clear();
        Destroy(collisionBall);
    }

    // Simple check for necessary amount of touched balls
    private bool removeOrNot(GameObject ball)
    {
        if(ball.GetComponent<ColorCheck>().sameColorTouched.Count > 1)
        {
            return true;
        }
        else
        {
            foreach(GameObject g in ball.GetComponent<ColorCheck>().sameColorTouched)
            {
                if(g.GetComponent<ColorCheck>().sameColorTouched.Count > 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
