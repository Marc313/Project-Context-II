using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GamemodeManager : Singleton<GamemodeManager>
{
    public UnityEvent beforeSwitchEvent;
    public UnityEvent afterSwitchEvent;
    public GameObject[] activeInProtestScene;
    public GameObject[] activeInCEOScene;

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

}
