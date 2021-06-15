using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Entity m_owner;

    void Start()
    {
        m_owner = gameObject.GetComponent<Entity>();       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ApplyDamage(float _damage)
    {
        m_owner.m_health -= _damage;
        if(m_owner.m_health <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float _healQuantity)
    {
        m_owner.m_health += _healQuantity;
        m_owner.m_health = Mathf.Clamp(m_owner.m_health, 0, m_owner.m_maxHealth);
    }

    void Die()
    {
        Destroy(m_owner.gameObject);
    }
}
