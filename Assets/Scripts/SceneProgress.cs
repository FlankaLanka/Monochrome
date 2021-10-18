using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProgress : MonoBehaviour
{
    public enum Section {Tutorial, Sean, Q, Frank};

    public Animator transition;

    public Section section;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(FadingAnim());
        }
    }


    private IEnumerator FadingAnim()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        if(section == Section.Tutorial) {
            SceneManager.LoadScene("Tutorial1");
        }
        else if(section == Section.Sean) {
            SceneManager.LoadScene("badaTestScene");
        }
        else if(section == Section.Q) {
            SceneManager.LoadScene("Q");
        }
        else {
            SceneManager.LoadScene("FrankWorld");
        }
        
    }
}
