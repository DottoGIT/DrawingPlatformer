using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] Collision_Detection[] badColliders;
    [SerializeField] Collision_Detection[] goodColliders;
    [SerializeField] SpriteRenderer myPlatformSprite;
    [SerializeField] Collider2D myPlatformCollider;
    [SerializeField] float fadeInSpeed;
    protected AudioSource myAudio;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        Player.onPlayerDeath += DestroyPlatforms;
    }

    virtual public void SpawnPlatform()
    {
        StartCoroutine(spawnPlatformOverTime());
        myAudio.Play();
    }

    public bool isCompleted()
    {
        foreach(var obj in badColliders)
        {
            if(obj.isColliding)
            {
                return false;
            }
        }

        foreach (var obj in goodColliders)
        {
            if(!obj.isColliding)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator spawnPlatformOverTime()
    {
        float colorAlpha = myPlatformSprite.color.a;
        while(colorAlpha <= 1)
        {
            colorAlpha += Time.deltaTime * fadeInSpeed;
            myPlatformSprite.color = new Color(255, 255, 255, colorAlpha);
            yield return new WaitForEndOfFrame();
        }
        myPlatformCollider.enabled = true;
    }


    virtual public void DestroyPlatforms()
    {
        myPlatformSprite.color = new Color(255, 255, 255, 0);
        myPlatformCollider.enabled = false;

    }
}
