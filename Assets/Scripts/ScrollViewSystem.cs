using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect _scrollRect;

    //[SerializeField] private ButtonScroll _rightButton;
    //[SerializeField] private ButtonScroll _leftButton;
    [SerializeField] private ButtonScroll _upButton;
    [SerializeField] private ButtonScroll _bottomButton;

    [SerializeField] private float ScrollSpeed = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_leftButton != null)
        //{
        //    if (_leftButton.isDown)
        //    {
        //        ScrollLeft();
        //    }
        //}
        //if (_rightButton != null)
        //{
        //    if (_rightButton.isDown)
        //    {
        //        ScrollRight();
        //    }
        //}
        if (_upButton != null)
        {
            if (_upButton.isDown)
            {
                ScrollTop();
            }
        }
        if (_bottomButton != null)
        {
            if (_bottomButton.isDown)
            {
                ScrollBottom();
            }
        }
    }

    //private void ScrollRight()
    //{
    //    if (_scrollRect != null)
    //    {
    //        if (_scrollRect.horizontalNormalizedPosition >= 0f)
    //        {
    //            _scrollRect.horizontalNormalizedPosition -= ScrollSpeed;
    //        }
    //    }
    //}
    //private void ScrollLeft()
    //{
    //    if (_scrollRect != null)
    //    {
    //        if (_scrollRect.horizontalNormalizedPosition <= 1f)
    //        {
    //            _scrollRect.horizontalNormalizedPosition += ScrollSpeed;
    //        }
    //    }
    //}
    private void ScrollBottom()
    {
        if (_scrollRect != null)
        {
            if (_scrollRect.verticalNormalizedPosition <= 1f)
            {
                _scrollRect.verticalNormalizedPosition += ScrollSpeed;
            }
        }
    }
    private void ScrollTop()
    {
        if (_scrollRect != null)
        {
            if (_scrollRect.verticalNormalizedPosition >= 0f)
            {
                _scrollRect.verticalNormalizedPosition -= ScrollSpeed;
            }
        }
    }
}
