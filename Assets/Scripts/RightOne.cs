using UnityEngine;

public class RightOne : MonoBehaviour
{
    private GameObject     mainCamera;
    private static Vector3 tempObjectPosition;
    private static bool    checkMouseClick, changePosAnim;
    static SpriteRenderer  object0, object1;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    private void Update()
    {
        if (changePosAnim)
        {
            object1.transform.position = Vector3.MoveTowards(object1.transform.position, tempObjectPosition,
                Time.deltaTime * 3);
            if (tempObjectPosition == object1.transform.position)
            {
                mainCamera.GetComponent<Game>().checkWin = true;
                changePosAnim = false;
            }
        }

    }

    private void OnMouseUpAsButton()
    {
        if (mainCamera.GetComponent<Game>().shuffle)
        {
            if (!checkMouseClick)
            {
                object0 = GetComponent<SpriteRenderer>();
                tempObjectPosition = object0.transform.position;
                object0.color = new Color(0.77f, 0.71f, 0);
                checkMouseClick = true;
            }
            else
            {
                object1 = GetComponent<SpriteRenderer>();
                object0.transform.position = object1.transform.position;
                changePosAnim = true;
                object0.color = Color.white;
                checkMouseClick = false;
            }
        }
    }
}