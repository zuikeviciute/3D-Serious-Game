using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions
{
    public static Action OnCodeOpen;
    public static Action OnCodeClose;
    public static Action OnFollowCorrect;
    public static Action OnFollowToggle;
    public static Action OnLookAtCorrect;
}

[System.Serializable]
public class CodeableObject
{
    public string name;
    public string code;
    public string userCode;
    public string objective;
    public GameObject _gameObject;
    public bool incorrect = false, c1 = false, c2 = false, c3 = false;
    public float x = 0, y = 0, z = 0;
    public Vector3 position = new Vector3(0, 0, 0);
    public Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
    public Color correctColor = Color.yellow;
    public float correctX, correctY, correctZ;

    public CodeableObject()
    {
        userCode = "";
    }
}
