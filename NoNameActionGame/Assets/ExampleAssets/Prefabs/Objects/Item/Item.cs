using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : TriggerObject
{
    public    string       m_itemName;
    public    bool         m_expiresImmediately;
    public    int          m_expiresWithTime;
    public    GameObject   m_effect;
    public    AudioSource  m_sfx;

    private   float        m_counterTime = 0;
    private   bool         m_enterActionDone = false;

    protected   Player     m_player;

    public override void OnTriggerWithPlayer   (Player player)
    {
        m_player = player;

        if (m_sfx != null)
        {
            
        }

        if (m_effect != null)
        {

        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        m_counterTime = 0;
        m_enterActionDone = true;
    }

    public virtual void ExecuteAction ()
    {
        if (m_expiresImmediately)
        {
            ExitAction ();
        }
    }

    public virtual void ExitAction    ()
    {
        m_enterActionDone = false;

        if (m_sfx != null)
        {

        }

        if (m_effect != null)
        {

        }

        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (m_enterActionDone && !m_expiresImmediately && m_expiresWithTime > 0)
        {
            m_counterTime += Time.deltaTime;

            if (m_counterTime < m_expiresWithTime)
            {
                ExecuteAction();
            }
            else
            {
                ExitAction();
            }
        }
        else if (m_enterActionDone && m_expiresImmediately)
        {
            ExitAction();
        }
    }
}
