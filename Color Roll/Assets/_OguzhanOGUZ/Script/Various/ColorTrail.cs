using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTrail : MonoBehaviour
{
    private Material _material;
    private Transform _transform;
    public TrailRenderer _trailRenderer;
    public static int orderInLayer = 0;

    private void Start()
    {
        _transform = transform;
        _transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        SetSortingOrder();
    }

    public void SetPosition(Vector3 pos, bool emit)
    {
        if(_trailRenderer)
        {
            _trailRenderer.emitting = emit;
            _transform.position = pos;
        }
    }
    public void ChangeColor(int colorNo)
    {
        _trailRenderer.startColor = GameManager.Instance.GetColor(colorNo);
        _trailRenderer.endColor = GameManager.Instance.GetColor(colorNo);
    }

    private void SetSortingOrder()
    {
        _trailRenderer.sortingOrder = orderInLayer;
        ColorTrail.orderInLayer++;
        if(orderInLayer > 20)
        {
            orderInLayer = 0;
        }
    }
}
