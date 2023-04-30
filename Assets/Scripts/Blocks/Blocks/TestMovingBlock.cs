using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingBlock : BaseBlock
{
    [SerializeField] float moveSpeed = 5f;
    bool isHorizontal;
    int isRight;
    protected override void Start()
    {
        base.Start();
        isHorizontal = Random.value > 0.5f;
        isRight = (Random.Range(0, 2) * 2) - 1;
    }
    protected override void Update()
    {
        base.Update();
        checkPlayerStand();
    }

    void checkPlayerStand()
    {
        if (IsPlayerStand)
        {
            Move();
        }
    }

    void Move()
    {
        if (isHorizontal)
        {
            Vector3 delta = isRight * transform.right * moveSpeed * Time.deltaTime;
            transform.position += delta;
        }
        else
        {
            Vector3 delta = -transform.up * moveSpeed * Time.deltaTime;
            transform.position += delta;
        }
    }
}
