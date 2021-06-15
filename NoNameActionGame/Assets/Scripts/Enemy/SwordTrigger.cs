using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger  : TriggerObject
{
    public override void OnTriggerWithPlayer(Entity _entity)
    {
        throw new System.NotImplementedException();
    }
    //public int damage;

    //public override void OnTriggerWithPlayer(Player player)
    //{
    //    player.AddHealth(-damage);
    //}

    public void EnableCollider (bool enable)
    {
       GetComponent<BoxCollider>().enabled = enable;
    }
}

