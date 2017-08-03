using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UndoMovement : MonoBehaviour
{
    private static List<ArrayList> undoMoves;
    private const string pileName = "Pile";

    // Use this for initialization
    void Start()
    {
        undoMoves = new List<ArrayList>();
    }

    public static void AddToMovesList(Transform card, Transform originalParent)
    {
        ArrayList array = new ArrayList();
        array.Add(card);
        array.Add(originalParent);
        undoMoves.Add(array);
    }

    public void OnClick()
    {
        if (undoMoves.Count > 0)
        {
            Transform previousOriginalParent = null;
            for (int i = undoMoves.Count - 1; i >= 0; i--)
            {
                ArrayList array = undoMoves[undoMoves.Count - 1];
                Transform card = (Transform)array[0];
                Transform originalParent = (Transform)array[1];

                if(previousOriginalParent == null)
                {
                    // If stack was not empty flip back last card
                    if (originalParent.childCount > 0)
                        originalParent.GetChild(originalParent.childCount - 1).GetComponent<Card>().SetActiveBackground();

                    UndoMove(card, originalParent);
                    previousOriginalParent = originalParent;
                }
                else
                {
                    if (previousOriginalParent.name != originalParent.name)
                        break;
                    else
                    {
                        UndoMove(card, originalParent);
                        previousOriginalParent = originalParent;
                    }
                }
            }
        }
    }

    private void UndoMove(Transform card, Transform originalParent)
    {
        card.SetParent(originalParent);
        if (originalParent.name == pileName)
            card.GetComponent<Card>().SetActiveBackground();
        undoMoves.RemoveAt(undoMoves.Count - 1);
        Score.score -= 25;
    }
}

