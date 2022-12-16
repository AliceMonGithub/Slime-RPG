using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    public Coroutine InvokeCoroutine(IEnumerator enumerator);
    public void StopCoroutine(IEnumerator enumerator);
}
