using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyDelay(3f));
    }

    IEnumerator DestroyDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
