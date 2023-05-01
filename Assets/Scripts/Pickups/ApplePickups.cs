using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickups : MonoBehaviour
{
    [SerializeField] int healValue;
    [SerializeField]
    private CharacterStats_SO m_playerData;

    private bool m_isEat;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && m_playerData.maxHealth<3&&!m_isEat)
        {
            m_isEat = true;
            // update health in SO objects
            var playerStates = other.GetComponent<CharacterStats>();
            playerStates.TakeApple(playerStates, healValue);
            SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_apple);

            Destroy(gameObject);
        }
        else if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
