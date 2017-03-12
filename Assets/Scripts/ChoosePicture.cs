using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePicture : MonoBehaviour {

    public static String pic;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
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

        pic = gameObject.name;

        SceneManager.LoadScene("ChooseLevel");
    }
}
