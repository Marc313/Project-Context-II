using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public float length = 10;
    public Transform startPos;
    public float dotValue;
    private LineRenderer lineRenderer;

    private bool isEnabled;

    private void Awake()
    {
        lineRenderer= GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, startPos.position);
    }

    public void SetEndPos(Vector3 _endPos)
    {
        if (Vector3.Dot(startPos.forward, _endPos - startPos.position) > dotValue)
        {
            lineRenderer.SetPosition(1, _endPos);
        }
    }

    public void Enable()
    {
        isEnabled = true;
        lineRenderer.enabled = true;
    }

    public void Disable()
    {
        isEnabled= false;
        lineRenderer.enabled = false;

    }
}
