using UnityEngine;

// A point is an intersection on the board where players can place their men
public class Point : MonoBehaviour
{
    // The id of the player occupying this point
    private long playerId;
    // The position of the point on the board, the first coordinate is the index
    // of the circle, the second is the index of the column, and the third is
    // the index of the row
    private Vector3Int position;

    // A point is defined by its position and the playerId is set to the default one
    // that belong to no player
    public void Initialize(int circleIndex, int rowIndex, int columnIndex)
    {
        position = new Vector3Int(circleIndex, rowIndex, columnIndex);
        playerId = DefaultValues.freePointPlayerId;
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
}
