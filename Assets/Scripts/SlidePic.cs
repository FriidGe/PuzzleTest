using UnityEngine;

public class SlidePic : MonoBehaviour
{
    private GameObject  pictures;
    private Vector3     targetnext = new Vector3(-8.1f, 0, 0),
                        targetback = new Vector3(0, 0, 0);
    private static bool next, back;

	void Start ()
	{
	    pictures = GameObject.Find("pictures");
	}

	void Update ()
	{
	    if (next)
	    {
	        pictures.transform.position = Vector3.MoveTowards(pictures.transform.position, targetnext, Time.deltaTime * 5);
	        if (pictures.transform.position == targetnext)
	        {
	            next = false;
	        }
	    }

	    if (back)
	    {
	        pictures.transform.position = Vector3.MoveTowards(pictures.transform.position, targetback, Time.deltaTime * 5);
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
