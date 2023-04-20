using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private AudioSource sfxCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (collided.CompareTag("Collectibles"))
        {
            anim.SetTrigger("collect");
            sfxCollect.Play();
        }
    }
}
