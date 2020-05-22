using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public ParticleSystem flames;
    public ParticleSystem hurt;

    private void Awake()
    {
        hurt.Stop();
        flames.Stop();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(this.gameObject);
        Debug.Log("Enemy die");
    }

    public void HurtPart()
    {
        hurt.Play();
    }

    public void FlamePart()
    {
        flames.Play();
    }
}
