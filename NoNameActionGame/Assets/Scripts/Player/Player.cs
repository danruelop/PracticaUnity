using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<TriggerObject>().OnTriggerWithPlayer(this);
    }

}




