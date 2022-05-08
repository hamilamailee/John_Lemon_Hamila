using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GargoyleDetection : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public float detection_range = 1f;

    public AudioSource caughtAudio;
    public AudioSource collsision_possible;
    public GameObject player;
    public GameObject loseButtons;
    public CanvasGroup exitBackgroundImageCanvasGroup;

    bool m_playerLost;
    bool m_HasAudioPlayed;
    float m_Timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_HasAudioPlayed = false;
            m_playerLost = true;
        }
    }

    void Update()
    {
        if (m_playerLost)
        {
            EndLevel();
        }
        else
        {
            if (Vector3.Magnitude(GetComponent<Collider>().transform.position - player.transform.position) < detection_range)
            {
                if (!collsision_possible.isPlaying)
                {
                    collsision_possible.Play();
                }
            }
            else
            {
                collsision_possible.Stop();
            }
        }
    }

    void EndLevel()
    {
        if (!m_HasAudioPlayed)
        {
            caughtAudio.Play();
            m_HasAudioPlayed = true;
        }
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            loseButtons.SetActive(true);
        }
    }

}
