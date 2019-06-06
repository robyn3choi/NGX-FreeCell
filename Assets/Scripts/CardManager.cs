using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Transform canvas;
    public GameObject cardGO;
    public List<Sprite> suitSprites;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i=1; i<14; i++)
        {
            for (int j=0; j<4; j++) // for each suit
            {
                Instantiate(cardGO, canvas);
                Card card = cardGO.GetComponent<Card>();
                card.Initialize(i, j, suitSprites[j]);
            }
        }
    }
}
