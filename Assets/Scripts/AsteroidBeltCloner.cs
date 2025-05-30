using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class AsteroidBeltCloner : MonoBehaviour
{
    [Header("Amount")]
    public int amount = 0;
    [Header("Asteroid Prefabs")]
    public float minScale;
    public float maxScale;
    public GameObject[] asteroidPrefabs;
    public string name;
    [Header("Ellipse")]
    public Transform point;
    public float minSpeedMove;
    public float maxSpeedMove;
    public float minRadius;
    public float maxRadius;
    [Header("Rotation")]
    public float minSpeedRotation;
    public float maxSpeedRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            float direction = Random.Range(-360f, 360f);
            float scale = Random.Range(minScale, maxScale);
            float radius = Random.Range(minRadius, maxRadius);
            float speedMove = Random.Range(minSpeedMove, maxSpeedMove);
            float speedRotation = Random.Range(minSpeedRotation, maxSpeedRotation);
            int numberPrefab = Random.Range(0, asteroidPrefabs.Length);
            Vector3 rotation = new Vector3(Random.Range(-360, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));

            var asteroidClone = Instantiate(asteroidPrefabs[numberPrefab], new Vector3(0f, 0f, 0f), Quaternion.identity);

            asteroidClone.transform.parent = this.transform;
            asteroidClone.name = name;

            if (asteroidClone.GetComponent<Ellipse>() == null) asteroidClone.AddComponent<Ellipse>();
            if (asteroidClone.GetComponent<Rotation>() == null) asteroidClone.AddComponent<Rotation>();

            asteroidClone.GetComponent<Ellipse>().point = this.transform;
            asteroidClone.GetComponent<Ellipse>().direction = direction;
            asteroidClone.GetComponent<Ellipse>().radius = radius;
            asteroidClone.GetComponent<Ellipse>().speed = speedMove;

            asteroidClone.GetComponent<Rotation>().rotation = rotation;
            asteroidClone.GetComponent<Rotation>().speed = speedRotation;


            asteroidClone.transform.localScale = new Vector3(scale, scale,scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = point.position;
    }
}
