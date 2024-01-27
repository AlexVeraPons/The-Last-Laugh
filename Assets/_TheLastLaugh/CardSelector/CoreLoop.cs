using System;
using UnityEngine;

public class CoreLoop : MonoBehaviour
{
    public static Action OnSelectCards;
    public static Action OnSelectCardsDone;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCards?.Invoke();
        }
    }
}
