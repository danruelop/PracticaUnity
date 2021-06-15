using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem m_partycleSystem;
    public GameObject Player;
    public float explosionDistance;
    public float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position + (this.gameObject.transform.forward.normalized*0.1f);

    }
    public void OnTriggerEnter(Collider other)
    {
        ParticleSystem t_partycleSystem = Instantiate(m_partycleSystem, gameObject.transform.position, Quaternion.identity);
        t_partycleSystem.Play();
        Destroy(t_partycleSystem.gameObject, 0.5f);
        Destroy(this.gameObject);
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < explosionDistance)
        {
            Player.GetComponent<HealthComponent>().ApplyDamage(damage);
        }
    }
}

