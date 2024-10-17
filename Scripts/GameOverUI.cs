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
        //게임매니져에서 만든 함수 실행
        GameManager.instance.CoinGetText();
    }
}
