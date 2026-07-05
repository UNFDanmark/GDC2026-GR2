using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnedArrow : MonoBehaviour
{
    [SerializeField] float speed = 2; //must be a multiple of the fixed update time
    public Transform spawn;
    public Transform target;
    public Transform tomb;
    public float t;
    public float t2;

    void Start()
    {
        t = 0;
        t2 = 0;
    }

    void Update()
    {
        t += Time.deltaTime * 1/3 * speed;
        
        //sætter position til et punkt mellem spawn og target variablet baseret på t variablet
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(spawn.position.y, target.position.y, t), Mathf.Lerp(spawn.position.z, target.position.z, t));
        if (t > 1)
        {
            onFinalDestination();
        }
    }

    private void onFinalDestination()
    {
        t2 += Time.deltaTime * 1.02054f * speed;
        
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(target.position.y, tomb.position.y, t2), Mathf.Lerp(target.position.z, tomb.position.z, t2));
        
        if (t2 > 1)
        {
            //Give negative score
            Destroy(this.gameObject);
        }
    }
}
