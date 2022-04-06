using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : Generation
{

    //Generation implementation
    public GameObject getBall(GameObject redball, GameObject yellowball, GameObject greenball, GameObject blueball)
    {
        return randomBall(redball, yellowball, greenball, blueball);
    }

    // Generates ball with random color
    private GameObject randomBall(GameObject redball, GameObject yellowball, GameObject greenball, GameObject blueball)
    {
        int ballType = Random.Range(1, 5);
        if (ballType == 1)
        {
            return redball;
        }
        else if (ballType == 2)
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
