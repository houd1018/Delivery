using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConveyBlock : BaseBlock
{
    [SerializeField] float conveySpeed = 1f;
    int isRight; // random value for left convey or right convey

    protected override void Start()
    {
        base.Start();
        isRight = (Random.Range(0, 2) * 2) - 1;
    }
    protected override void Update()
    {
        base.Update();
        checkBlockConvey();
    }

    void checkBlockConvey()
    {
        if (IsPlayerStand)
        {
            Vector3 delta = isRight * transform.right * conveySpeed * Time.deltaTime;
            player.transform.position += delta;
        }
    }

}
