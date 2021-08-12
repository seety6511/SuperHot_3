using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SH_EffectSpeedScaler : MonoBehaviour
{
    public List<ParticleSystem> particles;
    private void Start()
    {
        var p = GetComponents<ParticleSystem>().ToList();
        var p2 = GetComponentsInChildren<ParticleSystem>().ToList();
        particles = new List<ParticleSystem>();
        particles.AddRange(p);
        particles.AddRange(p2);
    }

    private void Update()
    {
        foreach(var p in particles)
        {
            var m = p.main;
            m.simulationSpeed = SH_TimeScaler.TimeScale;
        }
    }
}
