using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Stack : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        int count = this.transform.childCount;
        Card cardNew = eventData.pointerDrag.GetComponent<Card>();

        // If there are any cards on stack
        if (count > 0)
        {
            Card cardLast = this.transform.GetChild(count - 1).GetComponent<Card>();

            // Check if the colors are different
            if ((cardLast.cardType == Card.CardType.Cuori || cardLast.cardType == Card.CardType.Quadri) && (cardNew.cardType == Card.CardType.Fiori || cardNew.cardType == Card.CardType.Picche) ||
                (cardLast.cardType == Card.CardType.Fiori || cardLast.cardType == Card.CardType.Picche) && (cardNew.cardType == Card.CardType.Cuori || cardNew.cardType == Card.CardType.Quadri))
                // Check if values are different for 1
                if (cardNew.value != 10)
                {
                    if (cardLast.value == (cardNew.value + 1))
                    {
                        cardLast.draggable = false;
                        cardNew.parentToReturnTo = this.transform;
                    }
                }
                else
                {
                    if (cardLast.value == (cardNew.value + 2))
                    {
                        cardLast.draggable = false;
                        cardNew.parentToReturnTo = this.transform;
                    }
                }
        }
        // If stack is empty only king can be added
        else if(count == 0 && cardNew.value == 14)
        {
            cardNew.parentToReturnTo = this.transform;
        }        
    }
}
