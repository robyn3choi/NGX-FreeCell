using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform canvas;
    public GameObject cardPrefab;
    public List<Sprite> suitSprites;
    public List<Transform> tableauPiles;
    
    private List<Card> cards = new List<Card>();
    private int currentTableauIndex = 0;
    private int numCardsInCurrentTableauPile = 0;
    private const float cardOffset = -26;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // create all the cards
        for (int i=1; i<14; i++)
        {
            for (int j=0; j<4; j++) // for each suit
            {
                GameObject cardGO = Instantiate(cardPrefab);
                Card card = cardGO.GetComponent<Card>();
                card.Initialize(i, j, suitSprites[j]);
                cards.Add(card);
            }
        }
        
        // make a list of references to each card
        List<Card> availableCards = new List<Card>();
        foreach(Card card in cards)
        {
            availableCards.Add(card);
        }
        // randomly put cards into tableau
        for (int i=0; i<52; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            Card randomCard = availableCards[randomIndex];
            AddCardToTableau(randomCard);
            availableCards.RemoveAt(randomIndex);
        }
    }

    private void AddCardToTableau(Card card)
    {
        if (currentTableauIndex == tableauPiles.Count)
        {
            print("too many cards, not enough piles!");
        }
        else
        {
            Transform pile = tableauPiles[currentTableauIndex];
            card.transform.SetParent(pile);
            card.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, cardOffset * numCardsInCurrentTableauPile, 0);
            numCardsInCurrentTableauPile++;
            if (currentTableauIndex < 4 && numCardsInCurrentTableauPile == 7 ||
                currentTableauIndex >= 4 && numCardsInCurrentTableauPile == 6)
            {
                currentTableauIndex++;
                numCardsInCurrentTableauPile = 0;
            }
        }
    }
}
