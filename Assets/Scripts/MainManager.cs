using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    
    // В инспекторе к полям привязываются соответствующие объекты
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        // Отображаем имя игрока, введённое в меню
        PlayerNameText.text = PlayerData.Instance.PlayerName;
        
        // Инициализируем счет
        m_Points = 0;
        ScoreText.text = "Счёт: " + m_Points;
        
        // Загружаем лучший рекорд из PlayerPrefs
        PlayerData.Instance.BestScore = PlayerPrefs.GetInt("HighScore", 0);
        PlayerData.Instance.BestPlayerName = PlayerPrefs.GetString("HighScoreName", "—");
        HighScoreText.text = $"Лучший счёт: {PlayerData.Instance.BestPlayerName} : {PlayerData.Instance.BestScore}";
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Счёт : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        
        // Обновление и сохранение рекорда, если он побит
        if (m_Points > PlayerData.Instance.BestScore)
        {
            // Хранят значение рекорда и имя рекордсмена внутри сессии
            PlayerData.Instance.BestScore = m_Points;
            PlayerData.Instance.BestPlayerName = PlayerData.Instance.PlayerName;
            
            // Значения записываются в хранилище
            PlayerPrefs.SetInt("HighScore", PlayerData.Instance.BestScore);
            PlayerPrefs.SetString("HighScoreName", PlayerData.Instance.PlayerName);
            
            // Данные сохраняются между запусками приложения
            PlayerPrefs.Save();
            
            HighScoreText.text = $"Лучший счёт: {PlayerData.Instance.BestPlayerName} : {PlayerData.Instance.BestScore}";
        }
    }
}
