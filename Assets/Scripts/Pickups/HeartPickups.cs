using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickups : MonoBehaviour
{
    [SerializeField] int healValue;
    [SerializeField]
    private CharacterStats_SO m_playerData;

    private bool m_isEat;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && m_playerData.currentHealth < m_playerData.maxHealth && !m_isEat)
        {
            m_isEat = true;
            // update health in SO objects
            var playerStates = other.GetComponent<CharacterStats>();
            playerStates.TakeHeart(playerStates, healValue);
            SoundManager.Instance.PlaySound(SoundDefine.SFX_Battle_Player_LevelUp);

            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
