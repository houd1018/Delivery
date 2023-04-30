using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlock1 : BaseBlock
{
    float m_disappearTime = 1;
    float m_appearTime = 1;
    bool m_isDestroying;
    bool m_isDestroyed;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        checkPlayerStand();
        checkBlockDestroy();
        checkBlockAppear();
    }

    void checkPlayerStand()
    {
        if (IsPlayerStand) Debug.Log("stand");
        if (IsPlayerStand&&!m_isDestroying)
        {
            m_isDestroying = true;
        }
    }
    void checkBlockDestroy()
    {
        if (m_isDestroying)
        {
            m_disappearTime -= Time.deltaTime;
            if (m_disappearTime <= 0)
            {
                m_isDestroyed = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    void checkBlockAppear()
    {
        if (m_isDestroyed)
        {
            m_appearTime -= Time.deltaTime;
            if (m_appearTime <= 0)
            {
                m_isDestroyed = true;
                m_isDestroying = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                m_disappearTime = 1;
                m_appearTime = 1;
            }
        }
    }


}
