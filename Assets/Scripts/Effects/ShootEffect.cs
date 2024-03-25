using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _lightSource;
    [SerializeField] private AudioSource _shootSound;

    public void Perform()
    {
        StartCoroutine(EffectRoutine());
    }

    private IEnumerator EffectRoutine()
    {
        _shootSound.Play();
        _lightSource.SetActive(true);
        _particleSystem.Clear();
        _particleSystem.Play();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        _lightSource.SetActive(false);
    }
}
