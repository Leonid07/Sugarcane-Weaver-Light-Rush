using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(load());
    }
    public IEnumerator load()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
