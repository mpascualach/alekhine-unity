using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 10;
    private KeyCode[] moveButtons = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.Q, KeyCode.X };

    void Update()
    {
        if (Input.GetKey(moveButtons[0])) {
            transform.position += Vector3.left * Time.deltaTime;
        }
        else if (Input.GetKey(moveButtons[1]))
        {
            transform.position += Vector3.back * Time.deltaTime;
        }
        else if (Input.GetKey(moveButtons[2]))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
        else if (Input.GetKey(moveButtons[3]))
        {
            transform.position += Vector3.forward * Time.deltaTime;
        }
        else if (Input.GetKey(moveButtons[4]))
        {
            transform.position += Vector3.up * Time.deltaTime;
        }
        else if (Input.GetKey(moveButtons[5]))
        {
            transform.position += Vector3.down * Time.deltaTime;
        }

    }
}
