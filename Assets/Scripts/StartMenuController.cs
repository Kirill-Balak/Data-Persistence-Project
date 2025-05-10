using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Это отдельный простой класс для хранения имени между сценами
public static class PlayerInfo
{
    public static string playerName;
}

public class StartMenuController : MonoBehaviour
{
    // Назначить в инспекторе
    public TMP_InputField playerNameInput;
    
    // Этот метод вызовется, когда нажмём кнопку Start
    public void StartGame()
    {
        // Сохраняем имя
        PlayerData.Instance.PlayerName = playerNameInput.text;
        
        // Загружаем сцену, номер задаётся в File → Build Profiles
        SceneManager.LoadScene(1);
    }
}
