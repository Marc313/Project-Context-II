using UnityEngine;

public class GamemodeManager : Singleton<GamemodeManager>
{
    public GameObject[] activeInProtestScene;
    public GameObject[] activeInCEOScene;

    public int scoreToSwitch = 5;

    private PlayerMovement playerMovement;
    private int ceoHitCount;
    private bool inCEOMode = false;

    private void Awake()
    {
        Instance = this;
        playerMovement= FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in activeInCEOScene)
        {
            go.SetActive(false);
        }

        if (playerMovement != null) playerMovement.enabled = false;
        else Debug.Log("PlayerMovement not found");
    }

    public void SwitchToCEO()
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
    }

    public void AddCEOHitCount()
    {
        if (inCEOMode) return;

        ceoHitCount++;
        if (ceoHitCount >= scoreToSwitch)
        {
            SwitchToCEO();
        }
    }

}