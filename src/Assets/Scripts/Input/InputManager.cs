using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Settings")]
    public bool enableKeyboardInput = true;
    public bool enableMouseInput = true;
    public bool enableTouchInput = true;

    public event Action<Vector2> OnTouchStart;
    public event Action<Vector2> OnTouchMove;
    public event Action<Vector2> OnTouchEnd;
    public event Action OnTap;
    public event Action<Vector2> OnSwipeLeft;
    public event Action<Vector2> OnSwipeRight;
    public event Action<Vector2> OnSwipeUp;
    public event Action<Vector2> OnSwipeDown;

    public event Action OnMouseClick;
    public event Action<Vector2> OnMouseMove;
    public event Action<float> OnMouseScroll;

    public event Action<Vector2> OnKeyMove;

    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    [Header("Swipe Settings")]
    public float swipeThreshold = 50f;
    public float timeThreshold = 0.3f;

    private float startTime;

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
        if (enableKeyboardInput)
        {
            HandleKeyboardInput();
        }

        if (enableMouseInput)
        {
            HandleMouseInput();
        }

        if (enableTouchInput)
        {
            HandleTouchInput();
        }
    }

    private void HandleKeyboardInput()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }

        if (movement != Vector2.zero)
        {
            OnKeyMove?.Invoke(movement.normalized);
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            OnMouseMove?.Invoke(mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            OnMouseScroll?.Invoke(scroll);
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
            startTime = Time.time;
            OnTouchStart?.Invoke(touch.position);
        }

        if (touch.phase == TouchPhase.Moved)
        {
            OnTouchMove?.Invoke(touch.position);
        }

        if (touch.phase == TouchPhase.Ended)
        {
            touchEndPosition = touch.position;
            float gestureTime = Time.time - startTime;
            float gestureDistance = (touchEndPosition - touchStartPosition).magnitude;

            OnTouchEnd?.Invoke(touch.position);

            if (gestureDistance < swipeThreshold && gestureTime < timeThreshold)
            {
                OnTap?.Invoke();
            }
            else if (gestureDistance >= swipeThreshold && gestureTime <= timeThreshold)
            {
                Vector2 direction = touchEndPosition - touchStartPosition;
                HandleSwipe(direction);
            }
        }
    }

    private void HandleSwipe(Vector2 direction)
    {
        float x = Mathf.Abs(direction.x);
        float y = Mathf.Abs(direction.y);

        if (x > y)
        {
            if (direction.x > 0)
            {
                OnSwipeRight?.Invoke(touchEndPosition);
            }
            else
            {
                OnSwipeLeft?.Invoke(touchEndPosition);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                OnSwipeUp?.Invoke(touchEndPosition);
            }
            else
            {
                OnSwipeDown?.Invoke(touchEndPosition);
            }
        }
    }
}