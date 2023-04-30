using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounceBlock : BaseBlock
{
    protected override void Start()
    {
        base.Start();
        PhysicsMaterial2D material2D = Resources.Load<PhysicsMaterial2D>("Material/Bounce");
        Collider2D collider = GetComponent<Collider2D>();
        collider.sharedMaterial = material2D;
    }
    protected override void Update()
    {
        base.Update();
    }



}
