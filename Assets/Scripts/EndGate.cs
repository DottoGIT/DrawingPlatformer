using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject EndAnim;
    [SerializeField] GameObject Message;
    Player plr;

    private void Start()
    {
        plr = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Vector2.Distance(plr.transform.position, transform.position) < distanceFromPlayer)
        {
            Message.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                EndAnim.SetActive(true);
            }
        }
        else
        {
            Message.SetActive(false);
        }
    }
}
