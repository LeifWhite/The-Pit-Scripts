using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Target : MonoBehaviour
{
    //object health
    public float maxHealth = 5f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }
    //When damage is taken, decrease health
    public bool TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            return true;
            //Die();
        }
        return false;
    }
    //Die
    void Die()
    {
        gameObject.SetActive(false);
    }

}