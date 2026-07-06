using System;
using System.Collections;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public float t;
    Transform spawn;
    public bool lookingForAnchor;
    public Transform destination;
    public float zoom;

    void Awake()
    {
        t = 0;
        lookingForAnchor = true;
        spawn = transform;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!lookingForAnchor)
        {
            print("hillo");
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, destination.position.x, t), Mathf.Lerp(transform.position.y, destination.position.y, t), transform.position.z);
            t += Time.deltaTime * zoom;
        }
    }
}
