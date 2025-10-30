using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InputSystem_Actions actions;

    [SerializeField]
    private Player player;

    bool paused;
    private void Awake()
    {
        paused = false;
    }

    public void PauseResume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1.0f;
            Debug.Log("resumed");
            player.enableActions();
        }
        else
        {
            paused = true;
            Time.timeScale = 0.0f;
            Debug.Log("pausa");
            player.disableActions();

        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
