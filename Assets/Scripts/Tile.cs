using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    //void Awake() 
    //{
    //    SetColor();
    //}
    public void Init(bool isOffset) {
        GetComponent<Renderer>().material.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter() {
        Debug.Log(gameObject.name);
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    //public void SetColor()
    //{
    //    GetComponent<Renderer>().material.color = _offsetColor;
    //}
  
}
