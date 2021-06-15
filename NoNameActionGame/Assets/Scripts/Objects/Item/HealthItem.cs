using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item
{
    public int m_health;

    public override void OnTriggerWithPlayer   (Player player)
    {
        base.OnTriggerWithPlayer(player);

        player.AddHealth(m_health);
    }

    public override void ExecuteAction ()
    {
        base.ExecuteAction();
    }

    public override void ExitAction    ()
    {
        base.ExitAction();
        
    }
}
