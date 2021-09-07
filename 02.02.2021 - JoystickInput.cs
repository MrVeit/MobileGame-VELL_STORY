using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler //функционал джойстика.
{
    [SerializeField] [Range(0f, 0.9f)] public float _deadZoneRadius = 0.35f;

    public Image _image, _stickImage;
    public Vector2 _input, _handlePos;
    public ThirdPersonMovement_2.ThirdPersonMovement MainScript;

    public float Horizontal => _input.x <= -_deadZoneRadius || _input.x >= _deadZoneRadius ? _input.x * 2 : 0; //получение свойств перемещения по х и у из класса перемещения персонажа. 

    public float Vertical => _input.y <= -_deadZoneRadius || _input.y >= _deadZoneRadius ? _input.y * 2 : 0;


    private void Start()
    {
        _image = GetComponent<Image>();

        _stickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public Vector2 GetHandleInput()
    {
        if (_handlePos.magnitude > _deadZoneRadius)
        {
            if (_handlePos.magnitude > 1) _handlePos = _handlePos.normalized; //взаимодействие по осям х и у с джойстиком.
            return _handlePos;
        }
        return Vector2.zero;
    }

    public void OnPointerUp(PointerEventData point)
    {
        _handlePos = Vector2.zero;
        _input = Vector2.zero;
        _stickImage.rectTransform.anchoredPosition = Vector2.zero; //обнулени значений джойстика

        
    }

    public void OnPointerDown(PointerEventData point)
    {
        OnDrag(point);
    }

    public void OnDrag(PointerEventData point)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_image.rectTransform, point.position, point.pressEventCamera, out Vector2 pos))
        {
            pos.x = (pos.x / _image.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _image.rectTransform.sizeDelta.y);

            _handlePos = new Vector2(pos.x * 2, pos.y * 2);

            _input.x = Mathf.Clamp(_handlePos.x, -1f, 1f);
            _input.y = Mathf.Clamp(_handlePos.y, -1f, 1f);

            _stickImage.rectTransform.anchoredPosition = new Vector2(GetHandleInput().x * (_image.rectTransform.sizeDelta.x / 2), GetHandleInput().y * (_image.rectTransform.sizeDelta.y / 2));
        }
    }
}