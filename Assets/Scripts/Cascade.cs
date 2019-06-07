using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cascade : MonoBehaviour, ICell
{
    private List<Card> cardsInPile = new List<Card>();
    private int tableauIndex;

    public void Initialize(int index)
    {
        tableauIndex = index;
    }

    public bool AddInitialCard(Card card)
    {
        AddCard(card);
        // if this pile is full
        if (tableauIndex < 4 && cardsInPile.Count == 7 ||
            tableauIndex >= 4 && cardsInPile.Count == 6)
        {
            return false;
        }
        return true;
    }

    private void AddCard(Card card)
    {
        if (cardsInPile.Count > 0)
        {
            GetFrontCard().isFrontCard = false;
        }
        cardsInPile.Add(card);
        card.isFrontCard = true;
        card.transform.SetParent(transform);
        card.transform.localPosition =
            new Vector3(0, Card.STACK_OFFSET * (cardsInPile.Count - 1), 0);
    }

    public Card GetFrontCard()
    {
        if (cardsInPile.Count > 0)
        {
            return cardsInPile[cardsInPile.Count - 1];
        }
        return null;
    }

    public bool IsPotentialCardDrop(Card card)
    {
        Card frontCard = GetFrontCard();
        return AreDifferentColors(card, frontCard) &&
            card.GetNumber() == frontCard.GetNumber() - 1;
    }

    private bool AreDifferentColors(Card card1, Card card2)
    {
        return (!card1.IsRed() && card2.IsRed()) || (card1.IsRed() && !card2.IsRed());
    }

    public bool IsInCardDropDistance(Card card)
    {
        Vector3 frontCardPos = GetFrontCard().transform.position;
        frontCardPos.y += Card.STACK_OFFSET;
        return Vector3.Distance(card.transform.position, frontCardPos) < Card.DROP_DISTANCE;
    }

    public void Highlight()
    {
        GetFrontCard().Highlight();
    }

    public void StopHighlight()
    {
        GetFrontCard().StopHighlight();
    }

    public void DropCardInCell(Card card)
    {
        StopHighlight();
        AddCard(card);
    }

    public void RemoveFrontCard()
    {
        cardsInPile.RemoveAt(cardsInPile.Count - 1);
        GetFrontCard().isFrontCard = true;
    }
}
