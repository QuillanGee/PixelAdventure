using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    // Start is called before the first frame update
    private bool levelCompleted = false;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            anim.SetTrigger("finished");
            Invoke("CompleteLevel", 2f); //waits 2 seconds before running complete level
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
