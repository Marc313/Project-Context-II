using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamemodeManager : Singleton<GamemodeManager>
{
    public UnityEvent beforeSwitchEvent;
    public UnityEvent afterSwitchEvent;
    public GameObject[] activeInProtestScene;
    public GameObject[] activeInCEOScene;

    public PropjeSelectMenu selectMenu;

    private PlayerMovement playerMovement;
    private int ceoHitCount;
    private bool inCEOMode = false;

    private void Awake()
    {
        Instance = this;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in activeInCEOScene)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SwitchToCitizens();
        }
    }

    public void SwitchToCitizens()
    {
        if (!inCEOMode) return;

        inCEOMode = false;
        beforeSwitchEvent?.Invoke();

        Invoke(nameof(ContinueCitizenSwitch), 2.0f);
    }

    private void ContinueCitizenSwitch()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        else Debug.Log("PlayerMovement not found");

        foreach (GameObject go in activeInProtestScene)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in activeInCEOScene)
        {
            go.SetActive(false);
        }

        afterSwitchEvent?.Invoke();
        afterSwitchEvent.RemoveAllListeners();
        selectMenu?.OnSwitch(false);
    }

    public void SwitchToCEO()
    {
        if (inCEOMode) return;

        inCEOMode = true;
        beforeSwitchEvent?.Invoke();
        Invoke(nameof(ContinueCEOSwitch), 2.0f);
    }

    private void ContinueCEOSwitch()
    {
        if (playerMovement != null) playerMovement.enabled = true;
        else Debug.Log("PlayerMovement not found");

        foreach (GameObject go in activeInCEOScene)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in activeInProtestScene)
        {
            go.SetActive(false);
        }

        afterSwitchEvent?.Invoke();
        afterSwitchEvent.SetPersistentListenerState(0, UnityEventCallState.Off);
        selectMenu?.OnSwitch(true);
    }

    public void AddCEOHitCount()
    {
        if (inCEOMode) return;

/*        ceoHitCount++;
        if (ceoHitCount >= scoreToSwitch)
        {
            SwitchToCEO();
        }*/
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    } 

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
