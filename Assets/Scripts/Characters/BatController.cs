using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatController : MonoBehaviour
{
    [SerializeField] float fireRate = 2f;
    [SerializeField] float enableBatDifficulty = 0.5f;
    [SerializeField] private InfiniteScrollData m_infiniteScrollData;
    private GameObject fire;

    private void Start()
    {
        fire = transform.GetChild(1).gameObject;
        Debug.Log(fire);
        StartCoroutine(CastRay());
    }

    IEnumerator CastRay()
    {
        while (true)
        {
            if (m_infiniteScrollData.Difficulty > enableBatDifficulty)
            {
                fire.SetActive(!fire.activeSelf);
            }
            yield return new WaitForSecondsRealtime(fireRate);
        }
    }

}
