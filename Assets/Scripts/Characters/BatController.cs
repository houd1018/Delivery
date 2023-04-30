using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField] float fireRate = 2f;
    private GameObject fire;

    private void Start()
    {
        fire = FindObjectOfType<Fire>().gameObject;
        StartCoroutine(CastRay());
    }

    IEnumerator CastRay()
    {
        while (true)
        {
            fire.SetActive(!fire.activeSelf);
            yield return new WaitForSecondsRealtime(fireRate);
        }
    }
}
