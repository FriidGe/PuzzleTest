using UnityEngine;

public class SlideLvl : MonoBehaviour
{
    private GameObject     pictures, buttonNext, buttonBack;
    private static Vector3 targetnext = new Vector3(-8f, 0, 0),
                           targetback = new Vector3(0, 0, 0);
    private static bool    next, back;

	void Start ()
	{
	    pictures = GameObject.Find("pictures");
	    buttonNext = GameObject.Find("next");
	    buttonBack = GameObject.Find("back");
	}

	void Update ()
	{
	    if (next)
	    {
	        if (targetnext != pictures.transform.position)
	        {
	            pictures.transform.position =
	                Vector3.MoveTowards(pictures.transform.position, targetnext, Time.deltaTime * 15);
	            if (pictures.transform.position == targetnext)
	            {
	                next = false;
	            }
	        }
	        else
	        {
	            targetnext.x += -8f;
	            pictures.transform.position =
	                Vector3.MoveTowards(pictures.transform.position, targetnext, Time.deltaTime * 15);
	            if (pictures.transform.position == targetnext)
	            {
	                next = false;
	            }
	        }

	        if (pictures.transform.position.x <= -24f)
	        {
	            buttonNext.SetActive(false);
	        }
	    }

	    if (pictures.transform.position.x >= 0)
	    {
	        buttonBack.SetActive(false);
	    }

	    if (pictures.transform.position.x < 0)
	    {
	        buttonBack.SetActive(true);
	    }

	    if (back)
	    {
	        targetnext.x = -8f;
	        buttonNext.SetActive(true);
	        pictures.transform.position =
	            Vector3.MoveTowards(pictures.transform.position, targetback, Time.deltaTime * 15);
	        if (pictures.transform.position == targetback)
	        {
	            back = false;
	        }
	    }
	}

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.09f, 0.37f, 0.45f);
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.19f, 0.73f, 0.91f);
    }

    private void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Music") != "no")
            GetComponent<AudioSource>().Play();

        switch (gameObject.name)
        {
            case "next":
                next = true;
                break;
            case "back":
                back = true;
                break;
        }
    }
}
