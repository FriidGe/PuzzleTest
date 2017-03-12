using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject PanelWin;
    private static System.Random rng = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
    public List<GameObject> Puzzls = new List<GameObject>();
    private Vector3[]    puzlposOriginal = new Vector3 [25],
                         positions;
    private Vector3      normalScale;
    public bool          checkWin, shuffle, pause;
    private bool         finish, shuffleFinish;
    private static bool  animShuffle;
    public float         time;
    public Vector3[]     position3x3, position3x4, position4x3, position4x4, position5x5;
    public Text          timer, maxScore, winCurrentTime, winMaxScore, textWinMaxScore;
    private int columns, rows;


    void Start ()
    {
        var splitted = ChooseLevel.lvl.Split('x');
        columns = Int32.Parse(splitted[0]);
        rows = Int32.Parse(splitted[1]);

        maxScore.text = PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl).ToString();

        var position = new Dictionary<string, Vector3[]>()
        {
            {"3x3", position3x3},
            {"3x4", position3x4},
            {"4x3", position4x3},
            {"4x4", position4x4},
            {"5x5", position5x5}
        };

        positions = position[ChooseLevel.lvl];

        for (int i = 0; i < columns * rows; i++)
        {
            Puzzls.Add(Instantiate(Resources.Load(ChoosePicture.pic + "/" + ChooseLevel.lvl + "/" + ChoosePicture.pic + "_" + ChooseLevel.lvl + "_" + i)) as GameObject);
        }

        for(int i = 0; i < Puzzls.Count; i++)
        {
            Puzzls[i].transform.position = positions[i];
        }

        normalScale = Puzzls[0].transform.localScale;
        Array.Copy(positions, puzlposOriginal, positions.Length);
        StartCoroutine(ForShuffle());
    }

    private IEnumerator ForShuffle()
    {
        yield return new WaitForSeconds(1);
        Shuffle();
    }

    private IEnumerator GoFinish()
    {
        yield return new WaitForSeconds(1);
        PanelWin.SetActive(true);
        if (PlayerPrefs.GetString("Music") != "no")
            PanelWin.GetComponent<AudioSource>().Play();
    }

    private IEnumerator EndShuffle()
    {
        yield return new WaitForSeconds(1);
        animShuffle = false;
        shuffleFinish = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("ChooseLevel");
        }

        if (!finish && shuffleFinish && !pause)
        {
            time += Time.deltaTime;
            timer.text = time.ToString();
        }

        if (checkWin)
        {
            Check();
        }

        if (animShuffle)
        {
            for (int i = 0; i < Puzzls.Count; i++)
            {
                if (Puzzls[i].transform.position != positions[i])
                {
                    Puzzls[i].transform.position =
                        Vector3.MoveTowards(Puzzls[i].transform.position, positions[i], Time.deltaTime * 15);
                }
                Puzzls[i].transform.localScale = new Vector3(normalScale.x-0.2f, normalScale.y-0.2f);
            }
            StartCoroutine(EndShuffle());
        }
    }
    
   void Check()
    {
        bool allNormal = true;

        for(int i = 0; i < Puzzls.Count; i++)
        {
            if (Puzzls[i].transform.position == puzlposOriginal[i])
            {
                Puzzls[i].transform.localScale = normalScale;
            }
            else
            {
                Puzzls[i].transform.localScale = new Vector3(normalScale.x - 0.2f, normalScale.y - 0.2f);
            }
        }

        checkWin = false;

        for (int i = 0; i < Puzzls.Count; i++)
        {
            if (Puzzls[i].transform.localScale != normalScale)
            {
                allNormal = false;
                break;
            }
        }

        if (allNormal)
        {
            finish = true;

            if (PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl) > time || PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl) == 0)
            {
                PlayerPrefs.SetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl, time);
                maxScore.text = PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl).ToString();
                textWinMaxScore.text = "Новый рекорд";
                textWinMaxScore.color = new Color(0.51f, 0, 0);
            }

            winCurrentTime.text = time.ToString();
            winMaxScore.text = PlayerPrefs.GetFloat(ChoosePicture.pic + "_" + ChooseLevel.lvl).ToString();
            StartCoroutine(GoFinish());
        }
    }

    void Shuffle()
    {

        for (int i = Puzzls.Count - 1; i >= 1; i--)
        {
            int j = rng.Next(i + 1);
            var temp = positions[j];
            positions[j] = positions[i];
            positions[i] = temp;
        }

        animShuffle = true;
        shuffle = true;
    }
}
