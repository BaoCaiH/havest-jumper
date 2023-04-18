using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    //private int fruits = 0;

    //[SerializeField] private Text fruitsCount;
    //[SerializeField] private AudioSource collectionSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (collided.CompareTag("Collectibles"))
        {
            anim.SetTrigger("collect");
            //Destroy(collided);
            //collectionSFX.Play();
            //fruits++;
            //fruitsCount.text = "Fruits: " + fruits;
        }
    }
}
