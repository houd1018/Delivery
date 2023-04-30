using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingBlock : BaseBlock
{
    [SerializeField] float moveSpeed = 5f;
    bool isHorizontal;
    int isRightOrUp;
    protected override void Start()
    {
        base.Start();
        isHorizontal = Random.value > 0.5f;
        isRightOrUp = (Random.Range(0, 2) * 2) - 1;
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

    void test()
    {
        Vector3 delta = isRightOrUp * transform.up * moveSpeed * Time.deltaTime;
        transform.position += delta;
    }
    void Move()
    {
        if (isHorizontal)
        {
            Vector3 delta = isRightOrUp * transform.right * moveSpeed * Time.deltaTime;
            transform.position += delta;
        }
        else
        {
            Vector3 delta = isRightOrUp * transform.up * moveSpeed * Time.deltaTime;
            transform.position += delta;
        }
    }
}
