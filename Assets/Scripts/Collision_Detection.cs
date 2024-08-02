using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Detection : MonoBehaviour
{
    public string whatToDetect;
    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(whatToDetect))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(whatToDetect))
        {
            isColliding = false;
        }    
    }
}
