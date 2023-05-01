using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    [SerializeField] float fireDistance = 2f;
    [SerializeField] int damageValue = 1;
    private GameObject bat;
    bool hasDamaged;
    Ray2D ray;
    private void Awake()
    {
        bat = FindObjectOfType<BatController>().gameObject;
        ray = new Ray2D(bat.transform.position, bat.transform.up);
    }

    private void OnEnable()
    {
        hasDamaged = false;
    }
    private void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * fireDistance, Color.yellow);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction * fireDistance);
        if (raycastHit2D && !hasDamaged)
        {
            GameObject hitObject = raycastHit2D.collider.gameObject;
            if (hitObject.CompareTag("Player"))
            {
                // Debug.Log("Bat Hit object: " + hitObject.name);
                hitObject.GetComponent<CharacterStats>().CurrentHealth -= damageValue;
                hasDamaged = true;
            }
        }
    }
}
