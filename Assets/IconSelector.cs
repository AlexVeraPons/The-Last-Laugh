using System.Collections.Generic;
using UnityEngine;

public class IconSelector : MonoBehaviour
{
    private List<GameObject> _icons = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _icons.Add(child.gameObject);
        }
    }

    public void ShowIcon(CardStat stat)
    {
        Start();

        foreach (GameObject icon in _icons)
        {
            if (icon.name == stat.ToString())
            {
                icon.SetActive(true);
            }
            else
            {
                icon.SetActive(false);
            }
        }
    }
}
