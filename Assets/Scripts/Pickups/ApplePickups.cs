using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickups : MonoBehaviour
{
    [SerializeField] int healValue;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // update health in SO objects
            var playerStates = other.GetComponent<CharacterStats>();
            playerStates.TakeApple(playerStates, healValue);

            Destroy(gameObject);
        }
    }

}
