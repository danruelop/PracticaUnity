using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private float m_spawnMaxCooldown = 3.0f;
    private float m_spawnActualCooldown;

    public float m_spawnForce = 5.0f;
    public GameObject m_ball;
    public Transform m_pointOfSpawn;

    void Awake()
    {
        ResetCD();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_spawnActualCooldown > 0)
        {
            m_spawnActualCooldown -= Time.deltaTime;
        }
        else
        {
            SpawnBall();
            ResetCD();
        }
    }

    void SpawnBall()
    {
        GameObject tempBall = Instantiate(m_ball, m_pointOfSpawn.position, Quaternion.identity);
        Vector3 forceVector = Vector3.zero;
        forceVector.y = m_spawnForce;
        tempBall.gameObject.GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);
    }

    void ResetCD()
    {
        m_spawnActualCooldown = m_spawnMaxCooldown;
    }
}
