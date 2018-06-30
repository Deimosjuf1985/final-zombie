using UnityEngine;

public class FPS : MonoBehaviour
{
    public float velo = 1f;
    public void Movement(float speed)
    {
        if (Input.GetKey(KeyCode.W))
        { // define la dirección y velocidad
            transform.position += transform.forward * (velo * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * (velo * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        { 
            transform.position -= transform.right * (velo * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        { 
            transform.position += transform.right * (velo * Time.deltaTime);   
        }
    }
}