using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public enum CardType
    {
        Cuori = 1,
        Quadri = 2,
        Fiori = 3,
        Picche = 4,
        None = 0
    }

    public CardType cardType = CardType.None;
    public int value = 0;
    public Transform parentToReturnTo = null;
    public bool draggable = false;

    public Transform originalParent = null;
    private Transform figure = null;
    private Transform cardNumber = null;
    private Transform semi = null;
    private Transform background = null;
    private bool active = false;
    private const string pileName = "Pile";
    private const string wastePileName = "Waste Pile";
    private const string stack1 = "Stack1";
    private const string stack2 = "Stack2";
    private const string stack3 = "Stack3";
    private const string stack4 = "Stack4";
    private const string stack5 = "Stack5";
    private const string stack6 = "Stack6";
    private const string stack7 = "Stack7";
    private const string foundationCuori = "FoundationCuori";
    private const string foundationQuadri = "FoundationQuadri";
    private const string foundationFiori = "FoundationFiori";
    private const string foundationPicche = "FoundationPicche";
    private List<Transform> draggedCards = new List<Transform>();

    private void SetActiveImage(Transform parent, Transform image, string name, bool active)
    {
        if (image == null)
            image = parent.FindChild(name);

        if (image == null)
            Debug.LogError(parent.gameObject.name + " " + name + " image is null");
        else
            image.gameObject.SetActive(active);
    }

    public void SetActiveBackground()
    {
        this.SetActiveImage(this.transform, this.figure, "figure", false);
        this.SetActiveImage(this.transform, this.cardNumber, "cardNumber", false);
        this.SetActiveImage(this.transform, this.semi, "semi", false);
        this.SetActiveImage(this.transform, this.background, "background", true);
        this.active = false;
    }

    public void SetActiveFront()
    {
        this.SetActiveImage(this.transform, this.figure, "figure", true);
        this.SetActiveImage(this.transform, this.cardNumber, "cardNumber", true);
        this.SetActiveImage(this.transform, this.semi, "semi", true);
        this.SetActiveImage(this.transform, this.background, "background", false);
        this.active = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Save original parent in case it can't be put on another drop zone
        this.parentToReturnTo = this.transform.parent;
        // Save original parent in another variable for later comparison
        this.originalParent = this.transform.parent;
        // Clear previous group
        draggedCards.Clear();

        // Dragging single card
        if (this.draggable)
        {
            this.transform.SetParent(this.transform.parent.parent);

            // Disable block raycasting while dragging the card
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        // Dragging group of cards
        else if (!this.draggable && this.active)
        {
            string name = this.transform.parent.name;
            if (name == stack1 || name == stack2 || name == stack3 || name == stack4 || name == stack5 || name == stack6 || name == stack7)
            {
                int index = 0;
                int count = this.transform.parent.childCount;

                // Get index of dragged card
                for (int i = 0; i < count; i++)
                {
                    Card card = this.transform.parent.GetChild(i).GetComponent<Card>();
                    if (card != null)
                        if (this.cardType == card.cardType && this.value == card.value)
                        {
                            index = i;
                            break;
                        }
                }

                // If dragged card is not last on the stack, create group
                if (index != count - 1)
                {
                    //Save canvas parent for all dragged cards
                    Transform canvasParent = this.transform.parent.parent;

                    // Create dragged cards group
                    for (int i = index; i < count; i++)
                    {
                        this.transform.parent.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = false;
                        this.transform.parent.GetChild(i).GetComponent<Card>().originalParent = this.originalParent;
                        this.transform.parent.GetChild(i).GetComponent<Card>().parentToReturnTo = this.parentToReturnTo;
                        draggedCards.Add(this.transform.parent.GetChild(i));
                    }

                    // Set canvas as parent for all cards in group
                    for (int i = 0; i < draggedCards.Count; i++)
                        draggedCards[i].SetParent(canvasParent);
                }
            }
        }      
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Dragging single card
        if (this.draggable)
        {
            // Position card on mouse position
            this.transform.position = eventData.position;
        }
        // Dragging group of cards
        else if (!this.draggable && this.active)
        {
            // Position card on mouse position
            this.transform.position = new Vector3(eventData.position.x, eventData.position.y - 30, 0);
            for (int i = 1; i < draggedCards.Count; i++)
                draggedCards[i].transform.position = new Vector3(draggedCards[i - 1].transform.position.x, draggedCards[i - 1].transform.position.y - 20, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Set card to assigned parent
        this.transform.SetParent(parentToReturnTo);

        // Enable block raycasting after finishing dragging the card
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Dragging group of cards
        if (!this.draggable && this.active)
        { 
            for (int i = 1; i < draggedCards.Count; i++)
            {
                Card card = draggedCards[i].GetComponent<Card>();
                card.parentToReturnTo = this.parentToReturnTo;
                draggedCards[i].SetParent(this.parentToReturnTo);
                draggedCards[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

        // If parent changed
        if (this.originalParent != this.parentToReturnTo)
        {
            Moves.moves++;
            UndoMovement.AddToMovesList(this.transform, this.originalParent);

            if (draggedCards.Count > 0)
                for (int i = draggedCards.Count-1; i >= 0; i--)
                //for (int i = draggedCards.Count - 1; i > 0 ; i--)
                    UndoMovement.AddToMovesList(draggedCards[i], draggedCards[i].GetComponent<Card>().originalParent);

            // Playing a card from stock to tableau
            if (this.originalParent.name == wastePileName && (this.parentToReturnTo.name == stack1 || this.parentToReturnTo.name == stack2 || this.parentToReturnTo.name == stack3 ||
                                                             this.parentToReturnTo.name == stack4 || this.parentToReturnTo.name == stack5 || this.parentToReturnTo.name == stack6 ||
                                                             this.parentToReturnTo.name == stack7))
                Score.score += 45;

            // Transferring a card to the foundations
            if (this.parentToReturnTo.name == foundationCuori || this.parentToReturnTo.name == foundationFiori || this.parentToReturnTo.name == foundationPicche || this.parentToReturnTo.name == foundationQuadri)
                Score.score += 60;

            // Moving a foundation card back to the tableau
            if ((this.originalParent.name == foundationCuori || this.originalParent.name == foundationFiori || this.originalParent.name == foundationPicche || this.originalParent.name == foundationQuadri) 
                && (this.originalParent.name != this.parentToReturnTo.name))
                Score.score -= 75;

            int count = this.originalParent.transform.childCount;
            if (this.originalParent.name != wastePileName)
                // If any card remained on original stack
                if (count > 0)
                {
                    // Unlock the card on top of stack
                    Transform child = this.originalParent.transform.GetChild(count - 1);
                    Card card = child.GetComponent<Card>();
                    card.SetActiveFront();
                    card.draggable = true;

                    // Turning over a face down tableau card
                    Score.score += 25;
                }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // If card is on pile, put it on waste pile
        if (this.transform.parent.name == pileName)
        {
            Moves.moves++;
            this.originalParent = GameObject.Find(pileName).transform;
            UndoMovement.AddToMovesList(this.transform, this.originalParent);
            GameObject wastePile = GameObject.Find(wastePileName);
            this.transform.SetParent(wastePile.transform);
            this.SetActiveFront();
            this.draggable = true;
        }
    }
}
