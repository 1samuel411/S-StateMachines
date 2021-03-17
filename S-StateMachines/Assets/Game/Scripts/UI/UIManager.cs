using SLibrary.StateExample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public MainMenuController mainMenuController;
    public InGameController inGameController;
    public PausedController pausedController;
    public GameOverController gameOverController;

    public Canvas canvas;

    private void Awake()
    {
        instance = this;

        foreach(Transform transform in canvas.transform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
