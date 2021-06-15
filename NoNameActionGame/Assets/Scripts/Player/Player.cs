using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
   
    public GameObject m_PokeBall;
    public Transform m_initial;
    public GameObject VisualPlayer;

    public void SetInvencibility(bool enabled)
    {

    }


    public void Throw()
    {
        GameObject Pokeball = Instantiate(m_PokeBall, m_initial.position, Quaternion.identity);
        Pokeball.transform.LookAt(VisualPlayer.transform);
    }


}




