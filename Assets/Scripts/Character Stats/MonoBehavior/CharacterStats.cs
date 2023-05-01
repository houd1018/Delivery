using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterData;
    [SerializeField] AudioClip damageSFX;

    #region Read from Data_SO
    public int MaxHealth
    {
        get
        {
            if (characterData != null)
            {
                return characterData.maxHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            characterData.maxHealth = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
            {
                return characterData.currentHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            AudioSource.PlayClipAtPoint(damageSFX, Camera.main.transform.position);
            characterData.currentHealth = value;
        }
    }
    #endregion

    public void TakeApple(CharacterStats player, int healValue)
    {
        CurrentHealth += healValue;
        MaxHealth += healValue;
        // TODO: Update UI
    }
    public void TakeHeart(CharacterStats player, int healValue)
    {
        CurrentHealth += healValue;
    }

}
