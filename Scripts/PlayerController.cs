using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hSpeed;
    public float vSpeed;
   

    private float minimumY = -1.54f;
    private float maximumY = 1.04f;
    private float minimumX = -2.7f;
    private float maximumX = -1.47f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.instance.isGameOver)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 moveplayer = Vector3.right * h * hSpeed + Vector3.up * v * vSpeed;
            Vector3 boundaryPos = transform.position + moveplayer * Time.deltaTime;

            boundaryPos.y = Mathf.Clamp(boundaryPos.y, minimumY, maximumY);
            boundaryPos.x = Mathf.Clamp(boundaryPos.x, minimumX, maximumX);

            transform.position = boundaryPos;
        }
        //���� Ű�� �޾Ƽ� ���Ʒ��� �����Ѵ� �ִ��ּڰ� ���̿��� �Դٰ����ϰ� �����
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.GameOver();
        WallSpawner.Instance.StopSpawnEnemy();
        //���̸� ��
    }
}
