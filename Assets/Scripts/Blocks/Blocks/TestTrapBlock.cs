using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapBlock : BaseBlock
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageRate = 1f;
    private bool hasDamaged;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        checkPlayerStand();
    }

    void checkPlayerStand()
    {
        if (IsPlayerStand && !hasDamaged)
        {
            player.GetComponent<CharacterStats>().CurrentHealth -= damage;
            hasDamaged = true;
        }
        if (PlayerStandTime == 0)
        { 
            hasDamaged = false;
        }
    }
    IEnumerator WaitForNextDamage()
    {
        yield return new WaitForSecondsRealtime(damageRate);
        hasDamaged = false;
    }

    // player.GetComponent<CharacterStats>().CurrentHealth -= damage;

}
