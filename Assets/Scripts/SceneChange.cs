using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject collectibles;
    [SerializeField] string nextScene;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        int remainingFruit = collectibles.transform.childCount;
        bool exit = !anim.GetBool("exit");
        if (remainingFruit == 0 && exit)
        {
            anim.SetBool("exit", true);
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}
