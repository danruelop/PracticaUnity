using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float m_maxHealth;
    public float m_health;

    void Start()
    {
        m_health = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
