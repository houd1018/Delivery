using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject blockPrefeb;
    [SerializeField] float spwanRate = 1f;
    [SerializeField] float minPos = -1f;
    [SerializeField] float maxPos = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spwanRate, spwanRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }
    private void Spawn()
    {
        GameObject block = Instantiate(blockPrefeb, transform.position, Quaternion.identity);
        block.transform.position += Vector3.right * Random.Range(minPos, maxPos);
    }
}
