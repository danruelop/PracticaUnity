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
    public GameObject m_deathMenu;
    public Slider m_musicSlider;
    public Slider m_sfxSlider;
    public Text m_ammo;
    public Text m_scoreText;
    public int m_score = 0;

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
           
            /*Locks Cursor*/
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            m_paused.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            /*Enables Cursor*/
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void OnDeath()
    {
        m_deathMenu.SetActive(true);
        /*Enables Cursor*/
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStartLevel()
    {
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
    }

    public void OnMainMenu()
    {
        MusicManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene(AppScenes.MAIN_SCENE);
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
    }
    public void OnExit()
    {
        Application.Quit();
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
    public void UpdateScore(int _score)
    {
        m_score += _score;
        m_scoreText.text = m_score.ToString();
    }
}