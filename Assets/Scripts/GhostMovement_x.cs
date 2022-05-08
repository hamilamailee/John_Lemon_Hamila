using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement_x : MonoBehaviour
{
    public float ghost_speed = 1f;
    public float fadeDuration = 1f;
    public float move_duration = 1f;
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
    float m_FadeTime;

    Animator m_Animator;
    Rigidbody m_Rigidbody;

    Vector3 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator.SetBool("Return", false);
        transform.RotateAround(transform.position, transform.up, 180f);
    }

    // Update is called once per frame
    // Foraward:    (1, 0, 0)
    // Return:      (-1, 0, 0) 
    void Update()
    {
        if (m_playerLost)
        {
            EndLevel();
        }
        else
        {
            if (m_Animator.GetBool("Return"))
            {
                m_Movement.Set(1 * ghost_speed, 0, 0);
            }
            else
            {
                m_Movement.Set(-1 * ghost_speed, 0, 0);
            }

            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);

            m_Timer += Time.deltaTime;
            if (m_Timer > move_duration)
            {
                m_Timer = 0;
                if (!m_Animator.GetBool("Return"))
                {
                    m_Animator.SetBool("Return", true);
                }
                else
                {
                    m_Animator.SetBool("Return", false);
                }
                transform.RotateAround(transform.position, transform.up, 180f);
            }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_HasAudioPlayed = false;
            m_playerLost = true;
        }
    }
    void EndLevel()
    {
        if (!m_HasAudioPlayed)
        {
            caughtAudio.Play();
            m_HasAudioPlayed = true;
        }
        m_FadeTime += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_FadeTime / fadeDuration;
        if (m_FadeTime > fadeDuration + displayImageDuration)
        {
            loseButtons.SetActive(true);
        }
    }
}
