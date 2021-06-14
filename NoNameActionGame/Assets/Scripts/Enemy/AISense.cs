using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISense : MonoBehaviour
{
    private Transform m_playerTransform;
    private Player    m_playerBehaviour;

    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_playerBehaviour = m_playerTransform.GetComponent<Player>();
    }

    float GetDistance ()
    {
        float noise = 0.0f /*m_playerBehaviour.GetLevelNoise()*/;

        if (noise == 0)
        {
            return 0;
        }
        else if (noise < 40)
        {
            return 10;
        }
        else
        {
            return float.MaxValue;
        }
    }

    void Update()
    {
        float listeningDistance = GetDistance();
        
        if (Vector3.Distance(transform.position, m_playerTransform.position) < listeningDistance)
        {
            // Go to player
        }
    }
}
