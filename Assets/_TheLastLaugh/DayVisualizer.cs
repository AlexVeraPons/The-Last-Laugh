using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DayVisualizer : MonoBehaviour
{
     public TextMeshPro _dayText;
     public float _showDayFor = 1f;

    private void OnEnable() {
        CoreLoop.OnNewDay += SetDay;
    }
    private void Start() {
        _dayText.enabled = false;
    }
    public void SetDay(int day)
    {
        _dayText.enabled = true;
        _dayText.text = "Day " + day + " of 6";
        StartCoroutine(DisableText());
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(_showDayFor);
        _dayText.enabled = false;
    }
}



