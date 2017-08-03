using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class Pile : MonoBehaviour, IPointerClickHandler
{

    public List<GameObject> cards;
    public GameObject stack1;
    public GameObject stack2;
    public GameObject stack3;
    public GameObject stack4;
    public GameObject stack5;
    public GameObject stack6;
    public GameObject stack7;
    public GameObject wastePile;

    private static System.Random random = new System.Random();

	// Use this for initialization
	void Start () {
        this.CheckIfInitialized(this.stack1);
        this.CheckIfInitialized(this.stack2);
        this.CheckIfInitialized(this.stack3);
        this.CheckIfInitialized(this.stack4);
        this.CheckIfInitialized(this.stack5);
        this.CheckIfInitialized(this.stack6);
        this.CheckIfInitialized(this.stack7);
        this.CheckIfInitialized(this.wastePile);

        if (cards != null)
            Shuffle(cards);

        int count = cards.Count;
        for (int i = 0; i < count; i++)
        {
            // Instantiate prefab and assign it to stack
            GameObject cardPrefab = (GameObject)Instantiate(cards[i]);

            #region Assign cards to stacks

            if (i == 0)
            {
                cardPrefab.transform.SetParent(this.stack1.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i == 1)
            {
                cardPrefab.transform.SetParent(this.stack2.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 2)
            {
                cardPrefab.transform.SetParent(this.stack2.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i >= 3 && i <= 4)
            {
                cardPrefab.transform.SetParent(this.stack3.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 5)
            {
                cardPrefab.transform.SetParent(this.stack3.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i >= 6 && i <= 8)
            {
                cardPrefab.transform.SetParent(this.stack4.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 9)
            {
                cardPrefab.transform.SetParent(this.stack4.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i >= 10 && i <= 13)
            {
                cardPrefab.transform.SetParent(this.stack5.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 14)
            {
                cardPrefab.transform.SetParent(this.stack5.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i >= 15 && i <= 19)
            {
                cardPrefab.transform.SetParent(this.stack6.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 20)
            {
                cardPrefab.transform.SetParent(this.stack6.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else if (i >= 21 && i <= 26)
            {
                cardPrefab.transform.SetParent(this.stack7.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }
            else if (i == 27)
            {
                cardPrefab.transform.SetParent(this.stack7.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveFront();
                card.draggable = true;
            }
            else
            {
                cardPrefab.transform.SetParent(this.transform);
                Card card = cardPrefab.GetComponent<Card>();
                card.SetActiveBackground();
                card.draggable = false;
            }

            #endregion
        }
	}

    private void Shuffle(List<GameObject> list)
    {
        Debug.Log("shuffle start");
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void CheckIfInitialized(GameObject go)
    {
        if (go == null)
            Debug.LogError(go.name + " is not assigned!");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // If pile is empty
        if (this.transform.childCount == 0)
        {
            // Restore any remaining cards from Waste pile
            int count = this.wastePile.transform.childCount;
            if (count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    Transform child = this.wastePile.transform.GetChild(i);
                    child.SetParent(this.transform);
                    Card card = child.GetComponent<Card>();
                    card.SetActiveBackground();
                    card.draggable = false;
                }

                Score.score -= 200;
            }
        }
    }
}
