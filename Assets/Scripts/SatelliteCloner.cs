using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class SatelliteCloner : MonoBehaviour
{
    [Header("Line Renderers")]
    public Gradient colorGradient;
    public Material material;
    public float width;
    [Header("Satellite Prefabs")]
    public Vector3 scale;
    public GameObject[] satellitePrefabs;
    public string[] names;
    [Header("Ellipses & Line Renderers")]
    public Transform point;
    public float[] speedMoves;
    public float[] radius;
    [Header("Rotations")]
    public float[] speedRotations;
    [Header("Text")]
    public GameObject textPrefab;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < satellitePrefabs.Length; i++)
        {
            float ellipseRotation = Random.Range(-360f, 360f);
            float direction = Random.Range(-360f, 360f);
            Vector3 rotation = new Vector3(Random.Range(-360, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));

            var goClone = new GameObject();
            var textClone = Instantiate(textPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            var satelliteClone = Instantiate(satellitePrefabs[i], new Vector3(0f, 0f, 0f), Quaternion.identity);
            var lineRendererClone = new GameObject().AddComponent<LineRenderer>();

            goClone.transform.parent = this.transform;
            textClone.transform.parent = goClone.transform;
            satelliteClone.transform.parent = goClone.transform;
            lineRendererClone.transform.parent = goClone.transform;
            goClone.name = names[i];
            textClone.name = "Floating Text";
            satelliteClone.name = "Satellite";
            lineRendererClone.name = "Line Renderer";

            if (goClone.GetComponent<Ellipse>() == null) goClone.AddComponent<Ellipse>();
            if (goClone.GetComponent<Rotation>() == null) goClone.AddComponent<Rotation>();
            if (textClone.GetComponent<FloatingText>() == null) textClone.AddComponent<FloatingText>();
            if (lineRendererClone.GetComponent<LineRendererEllipse>() == null) lineRendererClone.AddComponent<LineRendererEllipse>();

            goClone.GetComponent<Ellipse>().point = this.transform;
            goClone.GetComponent<Ellipse>().direction = direction;
            goClone.GetComponent<Ellipse>().radius = radius[i];
            goClone.GetComponent<Ellipse>().speed = speedMoves[i];
            goClone.GetComponent<Ellipse>().ellipseRotation = ellipseRotation;

            goClone.GetComponent<Rotation>().rotation = rotation;
            goClone.GetComponent<Rotation>().speed = speedRotations[i];

            textClone.GetComponent<FloatingText>().offset = offset;
            textClone.GetComponent<FloatingText>().text = names[i];

            satelliteClone.transform.localScale = scale;

            lineRendererClone.GetComponent<LineRendererEllipse>().point = this.transform;
            lineRendererClone.GetComponent<LineRendererEllipse>().radius = radius[i];
            lineRendererClone.GetComponent<LineRendererEllipse>().ellipseRotation = ellipseRotation;
            lineRendererClone.material = material;
            lineRendererClone.colorGradient = colorGradient;
            lineRendererClone.startWidth = width;
            lineRendererClone.endWidth = width;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = point.position;
    }
}
