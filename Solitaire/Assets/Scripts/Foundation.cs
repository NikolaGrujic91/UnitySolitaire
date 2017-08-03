using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Foundation : MonoBehaviour, IDropHandler
{

    public enum FoundationType
    {
        Cuori = 1,
        Quadri = 2,
        Fiori = 3,
        Picche = 4,
        None = 0
    }

    public FoundationType foundationtype = FoundationType.None;
    public bool finished = false;

    public void OnDrop(PointerEventData eventData)
    {
        int count = this.transform.childCount;
        Card card = eventData.pointerDrag.GetComponent<Card>();

        if (count == 0)
        {
            if ((int)card.cardType == (int)this.foundationtype && card.value == 11)
            {
                card.parentToReturnTo = this.transform;
            }
        }
        else if (count == 1)
        {
            if ((int)card.cardType == (int)this.foundationtype && card.value == 2)
            {
                card.parentToReturnTo = this.transform;
            }
        }
        else if (count == 10)
        {
            if ((int)card.cardType == (int)this.foundationtype && card.value == 12)
            {
                card.parentToReturnTo = this.transform;
            }
        }
        else if ((count >= 2 && count <= 9) || count >= 11)
        {
            int newCardValue = card.value - 1;
            int lastCardValue = this.transform.GetChild(count - 1).GetComponent<Card>().value;

            if ((int)card.cardType == (int)this.foundationtype && newCardValue == lastCardValue)
            {
                card.parentToReturnTo = this.transform;
            }
        }

        // Check if foundation is finished
        if(count == 12)
        {
            this.finished = true;
        }
    }
}
