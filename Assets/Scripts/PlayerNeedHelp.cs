using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNeedHelp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject helper = collision.gameObject;
        Animator anim = helper.GetComponentInParent<Animator>();
        AudioSource sfxHelp = helper.GetComponentInParent<AudioSource>();
        if (helper.CompareTag("Helps"))
        {
            anim.SetTrigger("help");
            sfxHelp.Play();
        }
    }
}
