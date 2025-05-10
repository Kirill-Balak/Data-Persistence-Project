using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // get означает, что можно получить внешний доступ, private set защищает от изменений (инкапсуляция)
    public static PlayerData Instance { get; private set;}
    
    // Здесь будет храниться введенное имя, в инспекторе нужно переносить поле с текстом сюда
    public string PlayerName;
    
    // Текущий счет, в инспекторе нужно переносить поле с текстом сюда
    public int currentScore;
    
    // Рекорд,в инспекторе нужно переносить поле с текстом сюда
    public int BestScore;
    
    // Имя рекордсмена, в инспекторе нужно переносить поле с текстом сюда
    public string BestPlayerName;

    // Сохранение объекта между сценами
    void Awake()
    {   // с помощью && Instance != this дополнительно проверяется, что это не тот объект, который нужен, чтобы он не удалил сам себя
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
