
public interface ICell
{
    Card GetFrontCard();
    void RemoveFrontCard();
    bool IsPotentialCardDrop(Card card);
    bool IsInCardDropDistance(Card card);
    void Highlight();
    void StopHighlight();
    void DropCardInCell(Card card);
}
