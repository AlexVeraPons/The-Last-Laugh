using System;
using System.Collections;
using UnityEngine;

public class BossReactionShower : MonoBehaviour
{
    [SerializeField] private GameObject _angry;
    [SerializeField] private GameObject _funny;
    [SerializeField] private GameObject _chuckle;

    [SerializeField] private AudioClip _angryClip;
    [SerializeField] private AudioClip _funnyClip;
    [SerializeField] private AudioClip _chuckleClip;

    [SerializeField] private float _timeToHide = 1f;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        ReactionBoss.OnBossInteraction += ShowReaction;
    }

    private void OnDisable()
    {
        ReactionBoss.OnBossInteraction -= ShowReaction;
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _angry.SetActive(false);
        _funny.SetActive(false);
        _chuckle.SetActive(false);
    }

    private void ShowReaction(float obj)
    {
        if (obj == 0)
        {
            _audioSource.clip = _angryClip;
            _audioSource.Play();

            _angry.SetActive(true);
            _funny.SetActive(false);
            _chuckle.SetActive(false);
        }
        else if (obj >= 0.75)
        {
            _audioSource.clip = _funnyClip;
            _audioSource.Play();

            _angry.SetActive(false);
            _funny.SetActive(true);
            _chuckle.SetActive(false);
        }
        else if (obj <= 0.5f)
        {
            _audioSource.clip = _chuckleClip;
            _audioSource.Play();

            _angry.SetActive(false);
            _funny.SetActive(false);
            _chuckle.SetActive(true);
        }

        StartCoroutine(HideReaction());
    }

    private IEnumerator HideReaction()
    {
        yield return new WaitForSeconds(_timeToHide);
        _angry.SetActive(false);
        _funny.SetActive(false);
        _chuckle.SetActive(false);
    }
}
