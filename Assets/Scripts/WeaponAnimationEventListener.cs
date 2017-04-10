using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventListener : MonoBehaviour {

    public AudioSource weaponAudioSource;
    public AudioClip reloadSfx;

    void OnReloadSfxFrame ()
    {
        if (weaponAudioSource != null && reloadSfx != null)
        {
            weaponAudioSource.clip = reloadSfx;
            weaponAudioSource.Play();
        }
    }
}
