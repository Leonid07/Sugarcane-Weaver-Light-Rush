using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    [Header("Индекс уровня")]
    public int indexLevel = 0;

    Button thisButton;
    public Image imageLock;
    public TMP_Text textIndexLevel;

    public int isLoad = 0; // 0 не пройдено 1 пройдено
    public string idLevel;

    public Map mapNextLevel;

    public Map thisLevel;

    private void Awake()
    {
        thisLevel = GetComponent<Map>();
        textIndexLevel = GetComponentInChildren<TMP_Text>();
        thisButton = GetComponent<Button>();

        idLevel = gameObject.name;
        thisButton.onClick.AddListener(OnPointerClick);
        CheckLevel();
    }
    public void OnPointerClick()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        if (isLoad == 0)
            return;

        DataManager.InstanceData.mapNextLevel = thisLevel;

        DataManager.InstanceData.mapNextLevel = thisLevel;

        SoundManager.InstanceSound.musicFon_1.Play();
        SoundManager.InstanceSound.musicLevelMainMenu.Stop();

        GameManager.InstanceGame.StartGame();
        PanelManager.InstancePanel.DisActivePanels();
    }
    public void OpenLevel()
    {
        mapNextLevel.isLoad = 1;
        mapNextLevel.CheckLevel();
        DataManager.InstanceData.SaveLevel();
    }

    public void CheckLevel()
    {
        if (isLoad == 0)
        {
            imageLock.gameObject.SetActive(true);
            textIndexLevel.gameObject.SetActive(false);
        }
        if (isLoad == 1)
        {
            imageLock.gameObject.SetActive(false);
            textIndexLevel.gameObject.SetActive(true);
        }
    }
    public void SetTextIndexMap(string text)
    {
        textIndexLevel.text = text;
    }
}