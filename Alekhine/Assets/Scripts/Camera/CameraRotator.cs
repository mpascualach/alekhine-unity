using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed = 10.0f;

    private KeyCode[] rotateButtons = { KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow };

    void Update()
    {
        transform.Rotate(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
    }
}
