using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    void Start()
    {
        originalRotation = gameObject.transform.rotation;
    }

    [Header("Object Assignment")]
    [SerializeField]
    private GameObject gameObject;

    [Header("Display Settings")]
    [SerializeField]
    private float rotationTime = 30f;
    [SerializeField]
    private float sineRange = 90f;
    [SerializeField]
    private bool useSinWave = true;
    [SerializeField]
    private bool rotateAroundX = false;
    [SerializeField]
    private bool rotateAroundY = true;
    [SerializeField]
    private bool rotateAroundZ = false;

    private Quaternion originalRotation;
    void Update()
    {
        if (rotationTime > float.Epsilon)
        {
            float x = originalRotation.eulerAngles.x, 
                  y = originalRotation.eulerAngles.y, 
                  z = originalRotation.eulerAngles.z;

            if (rotateAroundX && useSinWave) x = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundX) x = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            if (rotateAroundY && useSinWave) y = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundY) y = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            if (rotateAroundZ && useSinWave) z = Mathf.Sin(Time.timeSinceLevelLoad) * sineRange;
            else if (rotateAroundZ) z = (Time.timeSinceLevelLoad * 1000) / rotationTime;

            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(x, y, z), Time.deltaTime);
        }
    }

    public void UpdateScaling()
    {
       
    }
}
