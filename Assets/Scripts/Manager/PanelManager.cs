using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager InstancePanel { get; private set; }

    private void Awake()
    {
        if (InstancePanel != null && InstancePanel != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstancePanel = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject panelWin;

    public Button buttonHome_1;
    public Button buttonHome_2;

    public Button ContinueLEvel;

    public GameObject panelWinn;
    public GameObject panelLose;

    private void Start()
    {
        buttonHome_1.onClick.AddListener(Home);
        buttonHome_2.onClick.AddListener(Home);

        ContinueLEvel.onClick.AddListener(NextLevel);
    }
    public void Home()
    {
        GameManager.InstanceGame.InCorectGame();
        SoundManager.InstanceSound.musicFon_1.Stop();
        SoundManager.InstanceSound.musicLevelMainMenu.Play();
        DisActivePanels();
    }
    public void NextLevel()
    {
        DataManager.InstanceData.mapNextLevel.mapNextLevel.LoadLevel();
        DisActivePanels();
    }
    public void DisActivePanels()
    {
        panelWinn.SetActive(false);
        panelLose.SetActive(false);
    }
}