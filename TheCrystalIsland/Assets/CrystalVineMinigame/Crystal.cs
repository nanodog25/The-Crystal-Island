using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public Sprite Vines;
    public Sprite NoVines;

    private bool _isNextSelection;
    private bool _isSelected;
    private bool _isSpinnable;

    private SpriteRenderer _img;
    private List<Crystal> _crystals;
    private LineRenderer _line;

    private void Awake()
    {
        _img = GetComponent<SpriteRenderer>();
        _crystals = transform.parent.GetComponentsInChildren<Crystal>().ToList();
        _line = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        _isSpinnable = _img.sprite == Vines;
        _isSelected = _crystals[0] == this;
        _line.startWidth = .3f;
        _line.endWidth = .3f;
        _line.enabled = false;
        if (_isSelected)
            OnSelect();
    }

    private void Update()
    {
        if (_isNextSelection)
        {
            _isSelected = true;
            _isNextSelection = false;
        }

        if (_isSelected && _isSpinnable)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 0, -2);
                DrawLine();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, 0, 2);
                DrawLine();
            }
        }
    }

    private void LateUpdate()
    {
        if (_isSelected)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _isSelected = false;
                _line.enabled = false;
                _crystals[IterationHelper.PreviousInArray(_crystals.IndexOf(this), _crystals.Count)].OnSelect();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _isSelected = false;
                _line.enabled = false;
                _crystals[IterationHelper.NextInArray(_crystals.IndexOf(this), _crystals.Count)].OnSelect();
            }
        }
    }

    private void DrawLine()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, 40);

        float distance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject != gameObject && hit.distance < distance)
            {
                distance = hit.distance;
            }
        }

        if (distance != float.MaxValue)
            _line.SetPosition(1, new Vector3(0, distance / transform.localScale.x));
        else
            _line.SetPosition(1, new Vector3(0, 40));
    }

    public void OnHit()
    {

    }

    public void OnSelect()
    {
        _isNextSelection = true;
        _line.enabled = true;
        DrawLine();
    }
}
