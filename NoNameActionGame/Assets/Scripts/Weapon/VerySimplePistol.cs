using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePistol : MonoBehaviour
{
	public int m_maxAmmo = 20;
	private int m_ammo = 20;

	private float m_timeBetweenShots = 0.2f;
	private float m_shootTimer = 0.0f;

	public  Transform m_raycastSpot;					
	public  float     m_damage        = 80.0f;				
	public  float     m_forceToApply  = 20.0f;				
	public  float     m_weaponRange   = 9999.0f;						
	public  Texture2D m_crosshairTexture;					
    public  AudioClip m_fireSound;
	public AudioClip m_reloadSound;
	public ParticleSystem m_partycleSystem;
    //private bool      m_canShot = true;
	private Animator  m_animator;
	private int iidle=0;
	


	private void Start()
    {
		m_animator = gameObject.GetComponent<Animator>();
    }

	private void Update()
	{
		m_shootTimer += Time.deltaTime;

		if(m_ammo <= 0 || Input.GetButtonUp("Fire1"))
        {
			BackToIdle();
		}
	}

	private void OnGUI()
	{
		Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
		Rect auxRect = new Rect(center.x - 20, center.y - 20, 20, 20);
		GUI.DrawTexture(auxRect, m_crosshairTexture, ScaleMode.StretchToFill);
	}


	public void Shot()
	{
		if (m_ammo > 0 && m_shootTimer >= m_timeBetweenShots-0.05)
        {
			m_animator.Play("Idle");
		}
		if (m_ammo > 0 && m_shootTimer >= m_timeBetweenShots)
        {
			m_animator.Play("Fire");
			m_partycleSystem.Play();
			
			

			Ray ray = new Ray(m_raycastSpot.position, m_raycastSpot.forward);

			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, m_weaponRange))
			{
				ParticleSystem t_partycleSystem=Instantiate(m_partycleSystem,hit.point,Quaternion.identity);
				t_partycleSystem.Play();
				Destroy(t_partycleSystem.gameObject,0.5f);
				/*
				Debug.Log("Hit " + hit.transform.name);
				if (hit.rigidbody)
				{
					hit.rigidbody.AddForce(ray.direction * m_forceToApply);
					Debug.Log("Hit");
				}
				*/
			}

			GetComponent<AudioSource>().PlayOneShot(m_fireSound);
			m_ammo--;
			m_shootTimer = 0.0f;
		}

		
	}

	public void Reload()
    {
		if(m_ammo < m_maxAmmo)
        {
			m_animator.Play("Reload");
			m_ammo = m_maxAmmo;
			GetComponent<AudioSource>().PlayOneShot(m_reloadSound);
		}
	}

	void BackToIdle()
    {
		m_animator.SetTrigger("Idle");
	}
}
