using UnityEngine;

public class GamemodeManager : MonoBehaviour
{
    public GameObject[] activeInProtestScene;
    public GameObject[] activeInCEOScene;

    private PlayerMovement playerMovement;

    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SwitchToCEO();
        }
    }
}
