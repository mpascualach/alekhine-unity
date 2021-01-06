using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public static Opponent instance;
    public Player playerInfo;

    private void Awake()
    {
        instance = this;
    }
}
