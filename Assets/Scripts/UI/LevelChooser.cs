using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
    private static Generation generation;
    private static string playerPrefName = "";
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentsInChildren<Text>()[1].text = PlayerPrefs.GetInt(gameObject.GetComponentsInChildren<Text>()[0].text).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        Type myType = Type.GetType(gameObject.GetComponentsInChildren<Text>()[0].text + "Generation", false, false);
        generation = (Generation)Activator.CreateInstance(myType);
        playerPrefName = gameObject.GetComponentsInChildren<Text>()[0].text;
        SceneManager.LoadScene("Main");
    }

    // generation getter
    public static Generation getGeneration()
    {
        return generation;
    }

    // playerPrefName getter
    public static string getPrefName()
    {
        return playerPrefName;
    }
}
