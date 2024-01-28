using System;
using UnityEngine;

public class SoundShuffle : MonoBehaviour
{

    private AudioSource _audioSource;
    [SerializeField] private DeckController _deck;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _deck.OnDeckShuffled += PlayShuffleSound;
    }

    private void PlayShuffleSound(Deck deck)
    {
        _audioSource.Play();
    }
}
