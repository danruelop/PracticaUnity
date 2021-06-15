using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float m_destroyTimer = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_destroyTimer -= Time.deltaTime;
        if(m_destroyTimer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
