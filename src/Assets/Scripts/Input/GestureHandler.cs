using UnityEngine;
using System;

public class GestureHandler : MonoBehaviour
{
    public static GestureHandler instance;

    [Header("Swipe Settings")]
    public float swipeThreshold = 50f; // Minimum distance for a swipe to be recognized
    public float timeThreshold = 0.3f; // Maximum time allowed for a swipe gesture

    [Header("Pinch Settings")]
    public float pinchThreshold = 0.1f; // Minimum distance change for a pinch to be recognized

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float startTime;

    public event Action<Vector2> OnTap;
    public event Action<Vector2> OnSwipeLeft;
    public event Action<Vector2> OnSwipeRight;
    public event Action<Vector2> OnSwipeUp;
    public event Action<Vector2> OnSwipeDown;
    public event Action<float> OnPinch;
    public event Action<Vector2, Vector2> OnPinchMove;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleTouch();
    }

    private void HandleTouch()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            startTouchPosition = touch.position;
            startTime = Time.time;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            endTouchPosition = touch.position;
            float gestureTime = Time.time - startTime;
            float gestureDistance = (endTouchPosition - startTouchPosition).magnitude;

            if (gestureDistance < swipeThreshold && gestureTime < timeThreshold)
            {
                // Tap
                OnTap?.Invoke(endTouchPosition);
            }
            else if (gestureDistance >= swipeThreshold && gestureTime <= timeThreshold)
            {
                // Swipe
                Vector2 direction = endTouchPosition - startTouchPosition;
                HandleSwipe(direction);
            }
        }

        // Handle Pinch
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 prevTouch1Pos = touch1.position - touch1.deltaPosition;
                Vector2 prevTouch2Pos = touch2.position - touch2.deltaPosition;

                float prevTouchDeltaMag = (prevTouch1Pos - prevTouch2Pos).magnitude;
                float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                if (Mathf.Abs(deltaMagnitudeDiff) > pinchThreshold)
                {
                    // Pinch gesture detected
                    OnPinch?.Invoke(deltaMagnitudeDiff);
                }

                // Pinch move
                OnPinchMove?.Invoke(touch1.position, touch2.position);
            }
        }
    }

    private void HandleSwipe(Vector2 direction)
    {
        float x = Mathf.Abs(direction.x);
        float y = Mathf.Abs(direction.y);

        if (x > y)
        {
            // Horizontal Swipe
            if (direction.x > 0)
            {
                OnSwipeRight?.Invoke(endTouchPosition);
            }
            else
            {
                OnSwipeLeft?.Invoke(endTouchPosition);
            }
        }
        else
        {
            // Vertical Swipe
            if (direction.y > 0)
            {
                OnSwipeUp?.Invoke(endTouchPosition);
            }
            else
            {
                OnSwipeDown?.Invoke(endTouchPosition);
            }
        }
    }
}