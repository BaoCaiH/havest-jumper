using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNeedHelp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.CompareTag("Helps"))
        {
            Animator anim = collided.GetComponentInParent<Animator>();
            AudioSource sfxHelp = collided.GetComponentInParent<AudioSource>();
            anim.SetTrigger("help");
            sfxHelp.Play();
        }
    }
}
