﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    public float m_stamina;
    
    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TriggerObject>().OnTriggerWithPlayer(this);
    }

    public void SetInvencibility (bool enabled)
    {

    }

    public void AddHealth (int health)
    {
        m_health += health;
    }
}




