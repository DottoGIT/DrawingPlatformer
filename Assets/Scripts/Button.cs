using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Sprite ButtonDown;
    [SerializeField] Transform MyGate;
    [SerializeField] float GateHeight;
    [SerializeField] float OpenSpeed;
    AudioSource myAudio;
    SpriteRenderer myRenderer;
    float gateYDestination;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myRenderer = GetComponent<SpriteRenderer>();
        gateYDestination = MyGate.position.y - GateHeight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            myAudio.Play();
            myRenderer.sprite = ButtonDown;
            StartCoroutine(OpenGate());
        }
    }

    IEnumerator OpenGate()
    {
        while(MyGate.position.y > gateYDestination)
        {
            MyGate.position += Vector3.down * OpenSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
