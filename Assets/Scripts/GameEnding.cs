using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    public AudioSource exitAudio;
    public GameObject player;
    public GameObject firstLevelLost;
    public GameObject firstLevelWon;

    public CanvasGroup exitBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_HasAudioPlayed;
    float m_Timer;

    private void Start()
    {
        firstLevelLost.SetActive(false);
        firstLevelWon.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_HasAudioPlayed = false;
            m_IsPlayerAtExit = true;
        }
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        if (!m_HasAudioPlayed)
        {
            exitAudio.Play();
            m_HasAudioPlayed = true;
        }
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            firstLevelWon.SetActive(true);
        }
    }

}
