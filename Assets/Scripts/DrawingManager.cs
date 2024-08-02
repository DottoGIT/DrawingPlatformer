using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public const float LINE_QUALITY = .01f;

    Camera cam;
    [SerializeField] Transform linesParent;
    [SerializeField] Line linePrefab;
    [SerializeField] GameObject DrawingSound;

    Outline[] outlines;
    Line currentLine;
    bool isDrawing = false;

    void Start()
    {
        cam = Camera.main;
        outlines = FindObjectsOfType<Outline>();
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
            currentLine.transform.SetParent(linesParent);
        }

        if(Input.GetMouseButton(0))
        {
            isDrawing = true;
            DrawingSound.SetActive(true);
            currentLine.SetPosition(mousePos);
        }
        else if(isDrawing == true)
        {
            isDrawing = false;
            bool smthWasCompleted = false;
            foreach(var obj in outlines)
            {
                if(obj.isCompleted())
                {
                    currentLine.Vanish(true);
                    obj.SpawnPlatform();
                    smthWasCompleted = true;
                }
            }
            if(smthWasCompleted == false)
            {
                currentLine.Vanish(false);
            }
            DrawingSound.SetActive(false);
        }
    }
}
