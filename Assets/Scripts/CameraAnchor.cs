using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    const float cameraSpeed = 0.5f;
    [SerializeField] Transform PlayerSpawner;
    [SerializeField] bool isInitialSpawner = false;

    private void Start()
    {
        if(isInitialSpawner)
        {
            Player.respawnPosition = PlayerSpawner;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(moveCamera());
            Player.respawnPosition = PlayerSpawner;
        }
    }

    IEnumerator moveCamera()
    {
        Vector3 desiredPos = new Vector3(transform.position.x, transform.position.y, -10);
        while (Vector3.Distance(Camera.main.transform.position, desiredPos) > 0.1f)
        {
            Vector3 dest = Vector3.Lerp(Camera.main.transform.position, desiredPos, cameraSpeed);
            Camera.main.transform.position = dest;
            yield return new WaitForFixedUpdate();
        }
    }
}
