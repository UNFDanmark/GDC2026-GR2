using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    float rotateT;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
        
    }
}
