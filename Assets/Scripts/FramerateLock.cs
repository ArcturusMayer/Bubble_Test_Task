using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLock : MonoBehaviour
{
    public int targetFrameRate = 30;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
