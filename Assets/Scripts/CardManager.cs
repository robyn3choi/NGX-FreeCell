using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform canvas;
    public GameObject cardPrefab;
    public List<Sprite> suitSprites;
    public List<Cascade> cascades;
    public GameObject cheatFreeCellParent;

    private List<Card> cards = new List<Card>();
    private int currentCascadeIndex = 0;
    private List<ICell> cells = new List<ICell>(); // cascades, freecells, foundations 
    private List<ICell> potentialCellsForCardDrop = new List<ICell>();
    private List<FreeCell> freeCells;
    private ICell cellDraggedFrom;
    private ICell currentDropCell;

    public static CardManager inst = null;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        freeCells = new List<FreeCell>(FindObjectsOfType<FreeCell>());
        cells.AddRange(freeCells);

        Foundation[] foundations = FindObjectsOfType<Foundation>();
        cells.AddRange(foundations);
        GameManager.inst.SetFoundations(foundations);

        for (int i = 0; i < cascades.Count; i++)
        {
            cascades[i].Initialize(i);
            cells.Add(cascades[i]);
        }

        // create all the cards
        for (int i = 1; i < 14; i++)
        {
            for (int j = 0; j < 4; j++) // for each suit
            {
                GameObject cardGO = Instantiate(cardPrefab);
                Card card = cardGO.GetComponent<Card>();
                card.Initialize(i, j, suitSprites[j], canvas);
                cards.Add(card);
            }
        }

        // make a list of references to each card
        List<Card> availableCards = new List<Card>();
        foreach (Card card in cards)
        {
            availableCards.Add(card);
        }
        // randomly put cards into tableau
        for (int i = 0; i < 52; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            Card randomCard = availableCards[randomIndex];
            AddCardToCascade(randomCard);
            availableCards.RemoveAt(randomIndex);
        }
    }

    private void AddCardToCascade(Card card)
    {
        if (currentCascadeIndex == cascades.Count)
        {
            print("too many cards, not enough piles!");
        }
        else
        {
            bool doesPileHaveSpace = cascades[currentCascadeIndex].AddInitialCard(card);
            if (!doesPileHaveSpace)
            {
                currentCascadeIndex++;
            }
        }
    }

    public void StartCardDrag(Card card)
    {
        foreach (ICell cell in cells)
        {
            if (card == cell.GetFrontCard())
            {
                cell.RemoveFrontCard();
                cellDraggedFrom = cell;
                break;
            }
        }

        foreach (ICell cell in cells)
        {
            if (cell.IsPotentialCardDrop(card))
            {
                potentialCellsForCardDrop.Add(cell);
            }
        }
    }

    public void CardDrag(Card card)
    {
        currentDropCell = null;
        foreach (ICell cell in potentialCellsForCardDrop)
        {
            if (cell.IsInCardDropDistance(card))
            {
                cell.Highlight();
                currentDropCell = cell;
            }
            else
            {
                cell.StopHighlight();
            }
        }
    }

    public void EndCardDrag(Card card)
    {
        if (currentDropCell != null)
        {
            currentDropCell.DropCardInCell(card);
        }
        else
        {
            cellDraggedFrom.DropCardInCell(card);
        }
        potentialCellsForCardDrop.Clear();
        cellDraggedFrom = null;
        currentDropCell = null;
    }

    public void TryAddCardToFreeCell(Card card)
    {
        foreach (FreeCell freecell in freeCells)
        {
            if (freecell.GetFrontCard() == null) // if a free cell is empty
            {
                foreach (Cascade cascade in cascades)
                {
                    if (card == cascade.GetFrontCard())
                    {
                        cascade.RemoveFrontCard();
                        freecell.DropCardInCell(card);
                        break;
                    }
                }
                break;
            }
        }
    }

    public void AddCheatFreeCells()
    {
        FreeCell[] cheatFreeCells = cheatFreeCellParent.GetComponentsInChildren<FreeCell>();
        freeCells.AddRange(cheatFreeCells);
        cells.AddRange(cheatFreeCells);
    }
}
