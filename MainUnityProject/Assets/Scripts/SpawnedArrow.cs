using System;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnedArrow : MonoBehaviour
{
    [SerializeField] float speed = 2; //must be a multiple of the fixed update time
    public Transform spawn;
    public Transform target;
    public Transform tomb;
    float t;

    void Start()
    {
        t = 0;
    }

    void Update()
    {
        t += Time.deltaTime * 1/3 * speed;
        
        //sætter position til et punkt mellem spawn og target variablet baseret på t variablet
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(spawn.position.y, target.position.y, t), Mathf.Lerp(spawn.position.z, target.position.z, t));
    }

    private void onFinalDestination()
    {
        t += Time.deltaTime * 2 * speed;

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(target.position.y, tomb.position.y, t),
            Mathf.Lerp(target.position.z, tomb.position.z, t));
        
        if (t > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
