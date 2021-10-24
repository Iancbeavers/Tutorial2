using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private float speed = 3.0f;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fractionOfJourney, 1));
    }
}
