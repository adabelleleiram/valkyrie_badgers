using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AnimationWait : MonoBehaviour 
{
    public float timeToWaitAfter = 10.0f;
    
    [System.Serializable]
    public class ActionEvent : UnityEvent { }

    public ActionEvent waitAfter;


    public void Play()
    {
        StartCoroutine(WaitForPlay());
    }
  
    IEnumerator WaitForPlay()
    {

        yield return new WaitForSeconds(timeToWaitAfter);
        waitAfter.Invoke();
    }
}