using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    #region Variables
    public float rotationSpeed;
    #endregion

    /**
     * Rotates the object from one orientation to another with a certain rotation speed.
     */
    public IEnumerator RotateTowardsSlowly(Quaternion oldRotation, Quaternion targetRotation, System.Action onDone = null)
    {
        float angle = Quaternion.Angle(oldRotation, targetRotation);
        float time = angle / rotationSpeed;
        float t = 0;

        transform.rotation = oldRotation;

        while (t < time)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;

        onDone?.Invoke();
    }
}