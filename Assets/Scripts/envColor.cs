using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envColor : MonoBehaviour
{
    public List<GameObject> ground = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> walls2 = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in ground)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0.63f, 0.59f, 0.59f, 1);
        }

        foreach (GameObject obj in walls)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0.66f, 0.53f, 0.56f, 1);
        }

        foreach (GameObject obj in walls2)
        {
            obj.GetComponent<Renderer>().material.color = new Color(0.72f, 0.76f, 0.67f, 1);
        }
    }
}
