using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float m_maxHealth;
    public float m_health;

    void Awake()
    {
        m_health = m_maxHealth;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
