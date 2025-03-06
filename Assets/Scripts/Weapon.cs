using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Enemy Contact;
    public ParticleSystem ParticleSystem;
    void OnTriggerStay(Collider collider)
    {
        Contact = collider.gameObject.GetComponent<Enemy>();
    }

    void OnTriggerExit(Collider collider)
    {
        Contact = null;
    }

    public void OnEnemyHit()
    {
        ParticleSystem.Play();
    }
}
