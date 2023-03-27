using System;
using UnityEngine;

public class CutsceneCamera : MovingObject
{
    public float animDuration = 2.0f;

    [SerializeField] private Transform cameraOneTransform;
    [SerializeField] private Transform cameraTwoTransform;
    private bool isOnOne = true;

    public void Play(Vector3 _startPosition, Vector3 _endPosition, Quaternion _startRot, Quaternion _endRot, Action _onDone = null)
    {
        StartCoroutine(MoveToInSeconds(_startPosition, _endPosition, animDuration, _onDone));
        StartCoroutine(RotateTowardsInSeconds(_startRot, _endRot, animDuration));
    }

    public void Switch()
    {
        Debug.Log("CUTSCENE");

        if (isOnOne)
        {
            Play(cameraOneTransform.position, cameraTwoTransform.position, cameraOneTransform.rotation, cameraTwoTransform.rotation);
            isOnOne = false;
        }
        else
        {
            Play(cameraTwoTransform.position, cameraOneTransform.position, cameraTwoTransform.rotation, cameraOneTransform.rotation);
            isOnOne = true;
        }
    }
}
