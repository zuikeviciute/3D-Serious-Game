using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjName : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int x, y, z;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Rotate(x, y, z);
    }
}
