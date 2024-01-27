using UnityEngine;

public class AppearOnSelectionState : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToAppear;
    private void OnEnable()
    {
        CoreLoop.OnSelectCards += Appear;
        CoreLoop.OnSelectCardsDone += Disappear;
    }

    private void OnDisable()
    {
        CoreLoop.OnSelectCards -= Appear;
        CoreLoop.OnSelectCardsDone -= Disappear;
    }

    private void Start() {
        Disappear();
    }


    private void Appear()
    {
        Debug.Log("Appear");
        foreach (GameObject obj in objectsToAppear)
        {
            obj.SetActive(true);
        }
    }

    private void Disappear()
    {
        Debug.Log("Disappear");
        foreach (GameObject obj in objectsToAppear)
        {
            obj.SetActive(false);
        }
    }
}
