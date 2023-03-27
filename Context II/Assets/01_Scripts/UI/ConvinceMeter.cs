using ThrowingGame;
using UnityEngine;
using UnityEngine.UI;

public class ConvinceMeter : MonoBehaviour
{
    public float onHitValue;
    public float switchThreshold;

    private float currentValue;
    private Slider convincedBar;

    private void Awake()
    {
        ServiceLocator.RegisterService(this);
        convincedBar = GetComponent<Slider>();    
    }

    private void Start()
    {
        currentValue = 0.5f;
        convincedBar.value = currentValue;
    }

    public void ChangeConvinceValue(ThrowingGame.NPCThrowing.Side _side)
    {
        float addition = _side == ThrowingGame.NPCThrowing.Side.Citizen ? -onHitValue : onHitValue;
        convincedBar.value = Mathf.Clamp01(convincedBar.value + addition);
        currentValue = convincedBar.value;

        if (Mathf.Abs(currentValue - 0.5f) > switchThreshold)
        {
            // Switch!
            Debug.Log("Switch!");
            if (currentValue < 0.5f)
                GamemodeManager.Instance.SwitchToCitizens();
            else
                GamemodeManager.Instance.SwitchToCEO();
        }
    }
}
