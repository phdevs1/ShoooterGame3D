using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public Transform startPos, endPost;
    public bool repeatable = false;
    public float speed = 1.0f;
    public float duration = 3.0f;
    float startTime, totalDistance;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos.position, endPost.position);
        while (repeatable)
        {
            yield return RepeatLearp(startPos.position, endPost.position, duration);
            yield return RepeatLearp(endPost.position, startPos.position, duration);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!repeatable)
        {
            float currentDuration = (Time.time - startTime) * speed;
            float journetFranction = currentDuration / totalDistance;
            this.transform.position = Vector3.Lerp(startPos.position, endPost.position, journetFranction);
        }
    }

    public IEnumerator RepeatLearp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i<1.0f)
        {
            i += Time.deltaTime * rate;
            this.transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
