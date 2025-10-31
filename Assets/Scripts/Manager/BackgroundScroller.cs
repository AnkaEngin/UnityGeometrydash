using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Background and ground looping mechanism
public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    private Vector2 _uvOffset;
    void Update()
    {
        _uvOffset += new Vector2(_x, _y) * Time.deltaTime;
        _img.uvRect = new Rect(_uvOffset.x, _uvOffset.y, 1, 1);
    }
}