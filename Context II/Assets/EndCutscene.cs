using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EndCutscene : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Play(UnityEvent _onDone = null)
    {
        GetComponent<Camera>().enabled = true;
        StartCoroutine(PlayCutscene(_onDone));
    }

    public IEnumerator PlayCutscene(UnityEvent _onDone = null)
    {
        anim.CrossFade("EndCutscene", 0.0f, 0);
        yield return new WaitForSeconds(5f);
        _onDone.Invoke();
    }
}
