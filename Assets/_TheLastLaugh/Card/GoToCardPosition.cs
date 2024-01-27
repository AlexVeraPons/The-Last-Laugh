using System;
using System.Collections;
using UnityEngine;

public class GoToCardPosition : MonoBehaviour
{
    [SerializeField] private float _timeToReachTarget = 1f;

private Vector3 _targetPosition;
    public void GoToPosition(int index)
    {
        _targetPosition = CardPositionStorer.Instance.GetCardPosition(index);
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 startPosition = transform.position;
        float time = 0;
        while (time < _timeToReachTarget)
        {
            transform.position = Vector3.Lerp(startPosition, _targetPosition, time / _timeToReachTarget);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPosition;
    }
}
