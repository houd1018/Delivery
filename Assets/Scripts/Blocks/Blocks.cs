using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public float speed;
    private float upEdge;

    private void Start()
    {
        upEdge = -(Camera.main.ScreenToWorldPoint(Vector3.one).y) + 1f;
    }
    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y > upEdge)
        {
            Destroy(gameObject);
        }
    }
}
