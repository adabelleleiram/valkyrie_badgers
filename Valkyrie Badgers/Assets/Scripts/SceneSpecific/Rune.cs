using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {

    public GameObject activeObject;
    public GameObject highlightObject;

    RunePuzzle puzzle;


    private void OnMouseEnter()
    {
        if(!activeObject.activeSelf)
            highlightObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlightObject.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        puzzle.OnRuneClicked(this);
    }

    public void SetActivated(bool active)
    {
        activeObject.SetActive(active);
    }

    public void SetPuzzle(RunePuzzle aPuzzle)
    {
        puzzle = aPuzzle;
    }
}
