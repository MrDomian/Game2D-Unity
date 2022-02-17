using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip pointSound, keySound, chestSound, gravelSound, menuSound;
    static AudioSource audioSrc;

    void Start()
    {
        pointSound = Resources.Load<AudioClip>("getPoint_m1");
        keySound = Resources.Load<AudioClip>("getKey_m1");
        chestSound = Resources.Load<AudioClip>("openChest_m1");
        gravelSound = Resources.Load<AudioClip>("breakGravel_m1");
        menuSound = Resources.Load<AudioClip>("clickMenu_m1");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "getPoint_m1":
                audioSrc.PlayOneShot(pointSound);
                break;
            case "getKey_m1":
                audioSrc.PlayOneShot(keySound);
                break;
            case "openChest_m1":
                audioSrc.PlayOneShot(chestSound);
                break;
            case "breakGravel_m1":
                audioSrc.PlayOneShot(gravelSound);
                break;
            case "clickMenu_m1":
                audioSrc.PlayOneShot(menuSound);
                break;
        }
    }
}
