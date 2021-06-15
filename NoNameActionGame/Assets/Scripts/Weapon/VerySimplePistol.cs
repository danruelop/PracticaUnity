using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePistol : MonoBehaviour
{
	public int m_maxAmmo = 20;
	private int m_ammo = 20;
	private Animator m_animator;

	[Header("Timers")]
	private float m_timeBetweenShots = 0.2f;
	private float m_shootTimer = 0.0f;

	[Header("Parameters")]
	public  float     m_damage        = 80.0f;				
	public  float     m_forceToApply  = 20.0f;				
	public  float     m_weaponRange   = 9999.0f;						

	[Header("VFX & SFX")]
	public Texture2D m_crosshairTexture;
	public AudioClip m_fireSound;
	public AudioClip m_reloadSound;
	public ParticleSystem m_partycleSystem;

	[Header("References")]
	public Transform m_raycastSpot;
	public Camera m_cameraRef;
	public Canvas m_canvasRef;

	private void Start()
    {
		m_animator = gameObject.GetComponent<Animator>();
		// Update canvas to max ammo
		m_canvasRef.GetComponent<MenuCanvasController>().UpdateAmmo(m_maxAmmo);
		MusicManager.Instance.PlayBackgroundMusic(AppSounds.MAIN_TITLE_MUSIC);
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

			Vector2 center = new Vector2((Screen.width / 2)-10, (Screen.height / 2)+10);


			Ray ray = m_cameraRef.ScreenPointToRay(center);

			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, m_weaponRange))
			{
				ParticleSystem t_partycleSystem=Instantiate(m_partycleSystem,hit.point,Quaternion.identity);
				t_partycleSystem.Play();
				Destroy(t_partycleSystem.gameObject,0.5f);

				Entity outE;

				if(hit.transform.gameObject.TryGetComponent<Entity>(out outE))
                {
					hit.transform.gameObject.GetComponent<HealthComponent>().ApplyDamage(m_damage);
                }
				
			
				/*
				Debug.Log("Hit " + hit.transform.name);
				if (hit.rigidbody)
				{
					hit.rigidbody.AddForce(ray.direction * m_forceToApply);
					Debug.Log("Hit");
				}
				*/
			}
			MusicManager.Instance.PlaySound(AppSounds.FIRE_SFX);
			// Update canvas 
			m_canvasRef.GetComponent<MenuCanvasController>().UpdateAmmo(m_ammo-1);
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
			// Update ammo in canvas
			m_canvasRef.GetComponent<MenuCanvasController>().UpdateAmmo(m_ammo);
			MusicManager.Instance.PlaySound(AppSounds.RELOAD_SFX);
		}
	}

	void BackToIdle()
    {
		m_animator.SetTrigger("Idle");
	}
}
