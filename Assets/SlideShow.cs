using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private GameObject[] _slides;
    private int _currentSlideIndex = 0;

    private void Start()
    {
        foreach (var slide in _slides)
        {
            slide.SetActive(false);
        }
        _slides[_currentSlideIndex].SetActive(true);
    }
    public void MouseDown(Vector2 mousePosition)
    {
        nextSlide();
    }

    private void nextSlide()
    {
        _slides[_currentSlideIndex].SetActive(false);
        _currentSlideIndex++;
        if (_currentSlideIndex >= _slides.Length)
        {
            LoadGame();
        }
        else
        {
            _slides[_currentSlideIndex].SetActive(true);
        }
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
