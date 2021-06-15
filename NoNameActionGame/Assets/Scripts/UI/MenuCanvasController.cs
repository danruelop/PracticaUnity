using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_options;
    public GameObject m_paused;
    public Slider m_musicSlider;
    public Slider m_sfxSlider;
    public Text m_ammo;

    public bool isPaused;

    public GameObject player;

    private void Start()
    {
        isPaused = false;
    }
    private void Update()
    {
        if (Input.GetButtonUp("Pause") && m_paused)
        {
            OnPauseToggle();
        }

    }

    public void OnPauseToggle()
    {
        if (isPaused)
        {
            m_paused.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            player.GetComponent<FPSCharacterController>().enabled = true;
            /*Locks Cursor*/
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            m_paused.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            player.GetComponent<FPSCharacterController>().enabled = false;
            /*Locks Cursor*/
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnUnpause()
    {
        m_paused.SetActive(false);
    }

    public void OnStartLevel()
    {
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(AppScenes.MAIN_SCENE);
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }


    public void OnOptions(bool isOptions)
    {
        if (m_mainMenu) m_mainMenu.SetActive(!isOptions);
        if (m_paused) m_paused.SetActive(!isOptions);
        m_options.SetActive(isOptions);

        if (!isOptions)
        {
            MusicManager.Instance.MusicVolumeSave = m_musicSlider.value;
            MusicManager.Instance.SfxVolumeSave = m_sfxSlider.value;
        }
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }
    public void OnExit()
    {
        Application.Quit();
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }

    public void OnMusicValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_musicSlider.value;
    }

    public void OnSfxValueChanged()
    {
        MusicManager.Instance.SfxVolume = m_sfxSlider.value;
    }

    public void UpdateAmmo(int _ammo)
    {
        m_ammo.text = _ammo.ToString();
    }

}