using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Kill(float delayInSeconds) {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(gameObject);
    }
    public void KillSignal(float delayInSeconds) {
        StartCoroutine("Kill", delayInSeconds);
    }
}
