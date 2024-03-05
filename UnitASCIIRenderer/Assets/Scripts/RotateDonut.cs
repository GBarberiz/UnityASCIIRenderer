using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDonut : MonoBehaviour
{
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    public float rotationSpeed = 50f;
    private float elapsedTime = 0f;
    private const float fullRotation = 360f;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Random.rotation;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / fullRotation);

        if (elapsedTime >= fullRotation)
        {
            elapsedTime = 0f;
            initialRotation = transform.rotation;
            targetRotation = Random.rotation;
        }
    }
}
