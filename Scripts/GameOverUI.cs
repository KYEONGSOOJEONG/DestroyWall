using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //���ӸŴ������� ���� �Լ� ����
        GameManager.instance.CoinGetText();
    }
}