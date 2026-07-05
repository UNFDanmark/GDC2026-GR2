using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnedArrow : MonoBehaviour
{
    [SerializeField] float speed = 2; //must be a multiple of the fixed update time
    public Transform spawn;
    public Transform target;
    float t;

    void Start()
    {
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime * 1/3 * speed;
        
        //sætter position til et punkt mellem spawn og target variablet baseret på t variablet
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(spawn.position.y, target.position.y, t), transform.position.z);
        if (t > 1)
        {
            //Destroy(this.gameObject);
        }
    }
}
