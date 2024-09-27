using UnityEngine;
using UnityEngine.UI;

// A point is an intersection on the board where players can place their men
public class Point : MonoBehaviour
{
    // The id of the player occupying this point
    private long playerId;
    // The position of the point on the board, the first coordinate is the index
    // of the circle, the second is the index of the column, and the third is
    // the index of the row
    private Vector3Int position;
    // The animator of the point
    [SerializeField] private Animator pointAnimator;
    // The animator for the legal move
    [SerializeField] private Animator legalMoveAnimator;
    // The image of the piece on the point
    [SerializeField] private Image pieceImage;
    // The button of the point
    [SerializeField] private Button button;

    // A point is defined by its position and the playerId is set to the default one
    // that belong to no player, at the start, it is not interactable
    public void Initialize(int circleIndex, int rowIndex, int columnIndex)
    {
        position = new Vector3Int(circleIndex, rowIndex, columnIndex);
        playerId = DefaultValues.freePointPlayerId;
        button.enabled = true;
    }

    // Returns the id of the player occupying this point
    public long GetPlayerId()
    {
        return playerId;
    }

    // Sets the id of the player occupying this point
    public void SetPlayerId(long playerId)
    {
        this.playerId = playerId;
    }

    // Returns the index of the circle of this point
    public int GetCircleIndex()
    {
        return position.x;
    }

    // Returns the index of the column of this point
    public int GetRowIndex()
    {
        return position.y;
    }

    // Returns the index of the row of this point
    public int GetColumnIndex()
    {
        return position.z;
    }

    // Removes all listeners from the point
    public void Clear()
    {
        button.onClick.RemoveAllListeners();
    }

    // Makes the point pickable so a player can click it, the legal
    // move animation is also played
    public void SetPickable()
    {
        button.onClick.AddListener(() => LegalPointClicked());
        legalMoveAnimator.SetBool("legalMove", true);
    }
    
    // Makes the point illegal to play so it plays an illegal sound
    // when clicked
    public void SetIllegal()
    {
        button.onClick.AddListener(() => IllegalPointClicked());
    }

    // Illegal point has been clicked, tell GameManager
    private void IllegalPointClicked()
    {
        GameManager.IllegalPointClicked();
    }

    // Legal point has been clicked, tell GameManager and stop
    // animation
    private void LegalPointClicked()
    {
        legalMoveAnimator.SetBool("legalMove", false);
        GameManager.LegalPointClicked(this);
    }

    // Plays the men placing animation for this point and changes
    // the color to be the current player's color
    public void PlayPlaceAnimation(string colorHexValue)
    {
        ColorUtility.TryParseHtmlString(colorHexValue, out Color color);
        pieceImage.color = color;
        pointAnimator.SetBool("placed", true);
    }
}
