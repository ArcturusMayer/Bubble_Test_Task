using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMainLayout : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Calculating necessary offset
        float offset = Screen.height / 2 + canvas.GetComponentsInChildren<RectTransform>()[1].sizeDelta.y / 2 - canvas.GetComponentsInChildren<RectTransform>()[1].anchoredPosition.y;
        // Shift all buttons by offset
        foreach (Button g in canvas.GetComponentsInChildren<Button>())
        {
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(g.GetComponent<RectTransform>().anchoredPosition.x, g.GetComponent<RectTransform>().anchoredPosition.y + offset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
