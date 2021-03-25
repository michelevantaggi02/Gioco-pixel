using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Process processo = Process.GetCurrentProcess();
        processo.Refresh();
        GetComponent<Text>().text = "FPS: " + Mathf.RoundToInt(1.0f / Time.deltaTime);
    }
}
