using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager InstanceGame { get; private set; }
    private void Awake()
    {
        if (InstanceGame != null && InstanceGame != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceGame = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public Vector3[] pointSpawn;
    public GameObject player;
    public GameObject gameCanvas;

    public GameObject mainCamera;
    public GameObject mainCanvas;

    public int count;
    public string idCountasd = "idCountasd";
    private void Start()
    {
        LoadCount();
    }
    public void StartGame()
    {
        player.SetActive(true);
        player.transform.Rotate(0,0,0);
        player.transform.position = pointSpawn[count];
        gameCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        mainCamera.SetActive(false);

        if (count == 3)
        {
            count = 0;
        }
        else
        {
            count++;
        }
        SaveCount();
    }
    public void InCorectGame()
    {
        player.SetActive(false);
        gameCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        mainCamera.SetActive(true);
    }

    void SaveCount()
    {
        PlayerPrefs.SetInt(idCountasd, count);
        PlayerPrefs.Save();
    }

    void LoadCount()
    {
        if (PlayerPrefs.HasKey(idCountasd))
        {
            count = PlayerPrefs.GetInt(idCountasd);
        }
    }
}
