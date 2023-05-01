using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterStats_SO : ScriptableObject
{
    private void OnValidate()
    {
        maxHealth = 1;
        currentHealth = 1;
    }
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
}
