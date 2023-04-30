using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseBlock : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_layerMask;

    float offsetX= 0;
    float offsetY=0.12f;
    float distance=2.88f;
    Vector3 offsetPos;

    private bool m_isPlayerStand;
    protected bool IsPlayerStand
    {
        get => m_isPlayerStand;
        private set
        {
            m_isPlayerStand = value;
        }
    }

    private float m_playerStandTime;
    protected float PlayerStandTime
    {
        get => m_playerStandTime;
        private set
        {
            m_playerStandTime = value;
        }
    }
    protected virtual void Start()
    {
        m_layerMask = LayerMask.GetMask("Player");
        offsetPos = transform.position + new Vector3(offsetX, offsetY, 0);
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        CheckPlayerStand();
        checkPlayerStandTime();
    }
    private void CheckPlayerStand()
    {
        var hit = Physics2D.Raycast(offsetPos, Vector2.right, distance * transform.localScale.x, m_layerMask);
        if (hit.collider != null)
        {
            IsPlayerStand = true;
            return;
        }
        hit = Physics2D.Raycast(offsetPos, Vector2.left, distance * transform.localScale.x, m_layerMask);
        if (hit.collider != null)
        {
            IsPlayerStand = true;
            return;
        }

        IsPlayerStand = false;
    }
    private void checkPlayerStandTime()
    {
        if (IsPlayerStand)
        {
            PlayerStandTime += Time.deltaTime;
        }
        else
        {
            PlayerStandTime = 0;
        }
    }
}
