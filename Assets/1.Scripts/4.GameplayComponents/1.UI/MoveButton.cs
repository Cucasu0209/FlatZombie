using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public enum MoveDir { Back, Forward }
    [SerializeField] private MoveDir Direction;
    [SerializeField] private Button MoveBtn;

    private void Start()
    {
    }
    private void CheckClick(Vector2 mousePos)
    {

    }
}
