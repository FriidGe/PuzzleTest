using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public GameObject  music_on, music_off, panelPause, panelExit;
    private GameObject mainCamera;
    public Text        pauseMaxScore, pauseCurrentScore;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        if (gameObject.name == "Music")
        {
            if (PlayerPrefs.GetString("Music") == "no")
            {
                music_on.SetActive(false);
                music_off.SetActive(true);
            }
            else
            {
                music_off.SetActive(false);
                music_on.SetActive(true);
            }
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
        GetComponent<SpriteRenderer>().color = Color.white;

        if (PlayerPrefs.GetString("Music") != "no")
            GetComponent<AudioSource>().Play();

        switch (gameObject.name)
        {
            case "Play":
                SceneManager.LoadScene("ChoosePicture");
                break;
            case "ChoosePicture":
                SceneManager.LoadScene("ChoosePicture");
                break;
            case "Rating":
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.OPEXApps.Colors");
                break;
            case "Replay":
                SceneManager.LoadScene("Play");
                break;
            case "Home":
                SceneManager.LoadScene("Main");
                break;
            case "pause":
                panelPause.SetActive(true);
                mainCamera.GetComponent<Game>().pause = true;
                pauseMaxScore.text = PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl).ToString();
                pauseCurrentScore.text = mainCamera.GetComponent<Game>().time.ToString();
                break;
            case "Continue":
                panelPause.SetActive(false);
                mainCamera.GetComponent<Game>().pause = false;
                break;
            case "Next":
                if (ChooseLevel.lvl == "3x3")
                {
                    ChooseLevel.lvl = "3x4";
                }
                else if (ChooseLevel.lvl == "3x4")
                {
                    ChooseLevel.lvl = "4x3";
                }
                else if (ChooseLevel.lvl == "4x3")
                {
                    ChooseLevel.lvl = "4x4";
                }
                else if (ChooseLevel.lvl == "4x4")
                {
                    ChooseLevel.lvl = "5x5";
                }
                else if (ChooseLevel.lvl == "5x5" && ChoosePicture.pic == "pic1")
                {
                    ChooseLevel.lvl = "3x3";
                    ChoosePicture.pic = "pic2";
                }
                else if (ChooseLevel.lvl == "5x5" && ChoosePicture.pic == "pic2")
                {
                    ChooseLevel.lvl = "3x3";
                    ChoosePicture.pic = "pic3";
                }
                else if (ChooseLevel.lvl == "5x5" && ChoosePicture.pic == "pic3")
                {
                    ChooseLevel.lvl = "3x3";
                    ChoosePicture.pic = "pic1";
                }
                SceneManager.LoadScene("Play");

                break;
            case "Exit":
                panelExit.SetActive(true);
                break;
            case "Yes":
                Application.Quit();
                break;
            case "No":
                panelExit.SetActive(false);
                break;
            case "Music":
                if (PlayerPrefs.GetString("Music") != "no")
                {
                    PlayerPrefs.SetString("Music", "no");
                    music_on.SetActive(false);
                    music_off.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetString("Music", "yes");
                    music_off.SetActive(false);
                    music_on.SetActive(true);
                }
                break;
        }
    }
}
