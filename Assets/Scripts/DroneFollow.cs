using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;
    Rigidbody r;
    bool lookAtComplete, followComplete, followToggle;

    private void OnEnable()
    {
        Actions.OnLookAtCorrect += LookAtCompleted;
        Actions.OnFollowCorrect += FollowCompleted;
        Actions.OnFollowToggle += FollowToggle;
    }

    private void OnDisable()
    {
        Actions.OnLookAtCorrect -= LookAtCompleted;
        Actions.OnFollowCorrect -= FollowCompleted;
        Actions.OnFollowToggle -= FollowToggle;
    }

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        followToggle = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lookAtComplete && followComplete & followToggle) Follow();
        else if (lookAtComplete) LookAt();
    }

    void LookAtCompleted()
    {
        lookAtComplete = true;
    }

    void FollowCompleted()
    {
        followComplete = true;
    }

    void LookAt()
    {
        transform.LookAt(target, Vector3.left);
    }

    void Follow()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        r.MovePosition(pos);
        transform.LookAt(target);
    }

    void FollowToggle()
    {
        if (followToggle == true) followToggle = false;
        else followToggle = true;
    }
}
