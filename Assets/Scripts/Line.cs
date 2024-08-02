using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] LineRenderer line_renderer;
    [SerializeField] EdgeCollider2D edge_collider;

    [SerializeField] Material goodMaterial;
    [SerializeField] Material badMaterial;
    [SerializeField] float fadeSpeed = 3f;

    List<Vector2> collider_points = new List<Vector2>();

    void Start()
    {
        edge_collider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos))
            return;

        collider_points.Add(pos);
        line_renderer.positionCount++;
        line_renderer.SetPosition(line_renderer.positionCount - 1, pos);
        edge_collider.points = collider_points.ToArray();
    }
    
    public void Vanish(bool wasSuccessful)
    {
        Material materialToAssing = wasSuccessful ? goodMaterial : badMaterial;

        line_renderer.material = materialToAssing;
        edge_collider.enabled = false;
        StartCoroutine(FadeLineRenderer());
    }


    bool CanAppend(Vector2 pos)
    {
        if(line_renderer.positionCount == 0)
        {
            return true;
        }
        return Vector2.Distance(line_renderer.GetPosition(line_renderer.positionCount - 1), pos) > DrawingManager.LINE_QUALITY;
    }

    IEnumerator FadeLineRenderer()
    {
        Gradient lineRendererGradient = new Gradient();
        float timeElapsed = 0f;
        float alpha = 1f;

        while (timeElapsed < fadeSpeed)
        {
            alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeSpeed);

            lineRendererGradient.SetKeys
            (
                line_renderer.colorGradient.colorKeys,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
            );
            line_renderer.colorGradient = lineRendererGradient;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
