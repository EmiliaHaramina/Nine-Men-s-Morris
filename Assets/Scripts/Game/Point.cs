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
    // The mill symbol of the point that is shown when a mill is formed
    [SerializeField] private Image millSymbol;
    // The outline shows that the player picked this piece to move
    [SerializeField] private Image outline;
    // Bool that shows the piece on this point is about to move
    private bool moving;

    // A point is defined by its position and the playerId is set to the default one
    // that belong to no player, at the start, it is not interactable
    public void Initialize(int circleIndex, int rowIndex, int columnIndex)
    {
        position = new Vector3Int(circleIndex, rowIndex, columnIndex);
        playerId = DefaultValues.freePointPlayerId;
        button.enabled = true;
        millSymbol.enabled = false;
        outline.enabled = false;
        moving = false;
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

    // Removes all listeners from the point and removes the
    // legal move animation
    public void Clear()
    {
        button.onClick.RemoveAllListeners();
        legalMoveAnimator.SetBool("legalMove", false);
    }

    // If this point has a defined player id, change it
    // so it doesn't belong to a player and play the animation
    // for removing a piece
    public void RemovePlayerId()
    {
        if (playerId != DefaultValues.freePointPlayerId)
            playerId = DefaultValues.freePointPlayerId;
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
        legalMoveAnimator.SetBool("legalMove", false);
    }

    // Illegal point has been clicked, tell GameManager
    private void IllegalPointClicked()
    {
        // If the game is paused, don't do anything
        if (GameManager.IsGamePaused())
            return;
        GameManager.IllegalPointClicked();
    }

    // Legal point has been clicked, tell GameManager and stop
    // animation
    private void LegalPointClicked()
    {
        // If the game is paused, don't do anything
        if (GameManager.IsGamePaused())
            return;
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
        pointAnimator.SetBool("removed", false);
    }

    // Plays the men removing animation for this point
    public void PlayRemoveAnimation()
    {
        pointAnimator.SetBool("placed", false);
        pointAnimator.SetBool("removed", true);
    }

    // Activates the mill symbol of the point, giving it the opposite
    // color of the piece color
    public void ActivateMillSymbol()
    {
        millSymbol.enabled = true;
        Color pieceColor = pieceImage.color;
        Color color = new(1f - pieceColor.r, 1f - pieceColor.g, 1f - pieceColor.b);
        millSymbol.color = color;
    }

    // Deactivates the mill symbol of the point
    public void DeactivateMillSymbol()
    {
        millSymbol.enabled = false;
    }

    // Shows the outline of the point and prepares it for moving
    public void SetMoving(bool moving)
    {
        this.moving = moving;
        outline.enabled = moving;
    }

    // Returns true if the piece on this point is moving
    public bool IsMoving()
    {
        return moving;
    }
}
