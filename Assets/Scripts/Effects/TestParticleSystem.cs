using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleSystem : MonoBehaviour
{
    public void PlayParticle()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
