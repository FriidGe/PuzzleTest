using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {

    public static String lvl;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("ChoosePicture");
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Music") != "no")
            GetComponent<AudioSource>().Play();

        lvl = gameObject.name;

        SceneManager.LoadScene("Play");
    }
}
