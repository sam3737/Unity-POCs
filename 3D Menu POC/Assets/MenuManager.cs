using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviour
{
    private Vector3 defaultCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        defaultCameraPos = Camera.main.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position
        float freedom = 6;
        float maxX = Screen.width;
        float midX = maxX / 2;
        float mouseX = Mathf.Max(Mathf.Min(Input.mousePosition.x, maxX), 0);
        float rotation = freedom * Mathf.Sign(mouseX - midX) * Mathf.Pow(Mathf.Abs(((mouseX - midX) / midX)), 2);
        Camera.main.transform.eulerAngles = defaultCameraPos + new Vector3(0f, rotation, 0f);
    }
}
