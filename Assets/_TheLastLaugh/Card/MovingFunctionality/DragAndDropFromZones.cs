using System;
using UnityEngine;

public class DragAndDropFromZones : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 _mousePosition;
    private DropZone _currentDropZone;
    private bool _isDragging = false;
    private Card _card;

    private bool _canBeDragged = true;

    private Vector3 _originalScale;

    private void OnEnable() {
        GameLoop.OnBossReaction += SetCanBeDraggedFalse;
        GameLoop.OnBossReactionEnded += SetCanBeDraggedTrue;
    }
    
    private void SetCanBeDraggedTrue()
    {
        _canBeDragged = true;
    }

    private void SetCanBeDraggedFalse()
    {
        _canBeDragged = false;
    }

    private void Start()
    {
        _initialPosition = transform.position;
        _card = GetComponent<CardVisualizer>().GetCard();
        _currentDropZone = GetComponentInParent<DropZone>();
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        if (_isDragging)
        {
            transform.position = _mousePosition;
        }
    }

    public void OnMouseClick(Vector2 mousePosition)
    {
        if (IsMouseOver() && _canBeDragged)
        {
            _initialPosition = transform.position;
            _isDragging = true;

            if (_currentDropZone != null) { _currentDropZone.RemoveCard(_card); }
        }
    }

    public void OnMouseRelease(Vector2 mousePosition)
    {
        if (!_isDragging) return;

        _isDragging = false;

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            DropZone dropZone = hit.collider.GetComponent<DropZone>();
            if (dropZone != null)
            {
                _currentDropZone = dropZone;
                if (dropZone.CanFit(_card.startOrrEnd))
                {

                    _initialPosition = transform.position;
                    transform.parent = dropZone.transform;
                    dropZone.AddCard(_card);
                    return;
                }
            }
        }

        transform.position = _initialPosition;
    }

    public void OnMouseMove(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;

        if(IsMouseOver())
        {
            transform.localScale = _originalScale * 2f;
        }
        else
        {
            transform.localScale = _originalScale;
        }
    }

    private bool IsMouseOver()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }

    public void SetDropZone(DropZone dropZone)
    {
        _currentDropZone = dropZone;
        transform.parent = dropZone.transform;
    }
}
