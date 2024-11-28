using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LQParticleScaler : MonoBehaviour
{
    public float ScaleSize = 1.0f;

    void Awake()
    {
        // Save off all the initial scale values at start.
        ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.transform.setScaleXYZ(ScaleSize, ScaleSize,ScaleSize);
        }
    }

}