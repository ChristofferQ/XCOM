using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private GameObject _highlight;
    public GameObject rangeHighlight;
    public GameObject unitHighlight;
    public bool isCheck;
    public int dist; 
    public bool Walkable = true; 
    public Tile parent; 
    public bool inRange;
    


    public void Init(bool isOffset) {
        GetComponent<Renderer>().material.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }

    void OnMouseExit() {
        _highlight.SetActive(false);
    }

}
