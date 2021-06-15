using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : TriggerObject
{

    private Entity m_entity;
    public float m_boingStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnTriggerWithPlayer(Entity m_entity)
    {
        m_entity.Boing(m_boingStrength, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
