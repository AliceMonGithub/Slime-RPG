using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    public Coroutine InvokeCoroutine(IEnumerator enumerator);
    public void StopCoroutine(IEnumerator enumerator);

    public void Invoke(string name, float time);
    public void InvokeRepeating(string name, float time, float rate);
}
