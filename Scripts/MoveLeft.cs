using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float scrollSpeed;
    public float startPositionX;
    public float resetPosition;


    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isGameOver)
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            if (transform.position.x <= resetPosition)
            {
                Vector3 newPos = new Vector3(startPositionX, transform.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
        //게임오버되면 안움직이게끔 하기
        
    }
}
