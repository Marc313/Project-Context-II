using UnityEngine;

public class BurnOverTime : MonoBehaviour
{
    [Tooltip("The minimal percent of the game's timer to start the burning effect")]
    [Range(0, 1)] 
    [SerializeField] 
    private float minStartBurnPercent;

    [Tooltip("The maximal percent of the game's timer to start the burning effect")]
    [Range(0, 1)]
    [SerializeField]
    private float maxStartBurnPercent;

    [Range(0, 1)]
    [SerializeField] private float fireDuration = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float burnDuration = 0.25f;
    [SerializeField] private Material burnMaterial;

    private float burnPercent;
    private bool isBurning;
    private bool isMaterialChanged;
    private GameObject fireObject;

    private void Awake()
    {
        fireObject = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        fireObject.SetActive(false);
        burnPercent = Random.Range(minStartBurnPercent, maxStartBurnPercent);
    }

    private void Update()
    {
        if (!isBurning && TimeManager.Instance.TimePercent > burnPercent)
        {
            fireObject.SetActive(true);
            isBurning = true;
            Debug.Log("Burn!");
        }
        else if (isBurning && !isMaterialChanged && TimeManager.Instance.TimePercent > burnPercent + burnDuration)
        {
            isMaterialChanged = true;
            GetComponent<Renderer>().material = burnMaterial;
        }
        else if (isBurning && TimeManager.Instance.TimePercent > burnPercent + fireDuration)
        {
            fireObject.SetActive(false);
            Debug.Log("Stop burning");
        }

    }
}
