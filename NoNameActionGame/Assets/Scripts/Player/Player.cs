using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    public float m_stamina;
    public GameObject m_PokeBall;
    public Transform m_initial;
    public GameObject VisualPlayer;
    
    public void OnTriggerEnter(Collider other)
    {
        
    }

    

    public void AddHealth (int health)
    {
        m_health += health;
    }

    public void Throw()
    {
        GameObject Pokeball = Instantiate(m_PokeBall,m_initial.position, Quaternion.identity);
        Pokeball.transform.LookAt(VisualPlayer.transform);
    }
}




