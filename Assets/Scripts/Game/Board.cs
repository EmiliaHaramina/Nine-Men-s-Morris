using System.Collections.Generic;
using UnityEngine;
using static GameManager;

// The board contains points that players can place
// their men on
public class Board : MonoBehaviour
{
    // A list of all points on the board
    private List<Point> board;
    // Prefab for a point
    [SerializeField] private GameObject pointPrefab;
    // Parent of points
    [SerializeField] private GameObject points;
    // Prefab for a line betwene points
    [SerializeField] private GameObject linePrefab;
    // Parent of lines
    [SerializeField] private GameObject lines;

    // Sound effects available for the board
    [SerializeField] private SoundEffectSource buttonClickSoundEffectSource;
    [SerializeField] private SoundEffectSource legalMoveSoundEffectSource;
    [SerializeField] private SoundEffectSource illegalMoveSoundEffectSource;

    // Initializes the board according to the ring number
    public void Initialize(int ringNumber)
    {
        board = new List<Point>();

        // The number of columns and rows in each circle
        int rowNumber = 3;
        int columnNumber = 3;

        // The center of columns and rows
        int columnCenter = 1;
        int rowCenter = 1;

        // The width and height of the board
        Rect rect = GetComponent<RectTransform>().rect;
        var width = Mathf.Abs(rect.xMax - rect.xMin);
        var height = Mathf.Abs(rect.yMax - rect.yMin);

        var widthPerCircle = (width / 2) / ringNumber;
        var heightPerCircle = (height / 2) / ringNumber;

        // Going through the number of rings
        for (int i = 0; i < ringNumber; i++)
            // Going through the number of columns
            for (int j = 0; j < rowNumber; j++)
                // Going through the number of rows
                for (int k = 0; k < columnNumber; k++)
                {
                    // If this location would be in the center, ignore it, since
                    // the circle's center isn't a playable position
                    if (j == columnCenter && k == rowCenter)
                        continue;

                    // Initializes a point with the calculated coordinates
                    float pointX = (i + 1) * widthPerCircle * (k - 1);
                    float pointY = -(i + 1) * heightPerCircle * (j - 1);
                    GameObject point = Instantiate(pointPrefab, new Vector3(pointX, pointY, 0f), Quaternion.identity);
                    point.transform.SetParent(points.transform, false);
                    point.GetComponent<Point>().Initialize(i, j, k);
                    board.Add(point.GetComponent<Point>());
                }

        // Drawing lines between points
        foreach (Point point1 in board)
            foreach (Point point2 in board)
            {
                // Note: this is only checked one way to avoid drawing the same line twice
                int point1CircleIndex = point1.GetCircleIndex();
                int point2CircleIndex = point2.GetCircleIndex();
                int point1RowIndex = point1.GetRowIndex();
                int point2RowIndex = point2.GetRowIndex();
                int point1ColumnIndex = point1.GetColumnIndex();
                int point2ColumnIndex = point2.GetColumnIndex();

                // Booleans determining whether there is a difference of 1 between coordinates
                bool circleDifferenceOne = point1CircleIndex - point2CircleIndex == 1;
                bool rowDifferenceOne = point1RowIndex - point2RowIndex == 1;
                bool columnDifferenceOne = point1ColumnIndex - point2ColumnIndex == 1;
                // Boolean determining whether coordinates are the same
                bool circleSame = point1CircleIndex - point2CircleIndex == 0;
                bool rowSame = point1RowIndex - point2RowIndex == 0;
                bool columnSame = point1ColumnIndex - point2ColumnIndex == 0;

                // The x and y coordinates of both points
                float point1X = (point1CircleIndex + 1) * widthPerCircle * (point1ColumnIndex - 1);
                float point2X = (point2CircleIndex + 1) * widthPerCircle * (point2ColumnIndex - 1);
                float point1Y = -(point1CircleIndex + 1) * heightPerCircle * (point1RowIndex - 1);
                float point2Y = -(point2CircleIndex + 1) * heightPerCircle * (point2RowIndex - 1);

                // If there should be a horizontal line
                if ((columnDifferenceOne && circleSame && rowSame) ||
                    (circleDifferenceOne && rowSame && columnSame &&
                    point1RowIndex == 1 && point1ColumnIndex != 1))
                {
                    // Calculate line coordinates and instantiate line
                    float lineX = (point1X + point2X) / 2;
                    float lineY = point1Y;
                    GameObject line = Instantiate(linePrefab, new Vector3(lineX, lineY, 0f), Quaternion.identity);
                    line.transform.SetParent(lines.transform, false);
                    // Set the line width
                    line.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(point1X - point2X), DefaultValues.lineThickness);
                }
                // If there should be a vertical line
                else if ((rowDifferenceOne && circleSame && columnSame) ||
                    (circleDifferenceOne && rowSame && columnSame &&
                    point1ColumnIndex == 1 && point1RowIndex != 1))
                {
                    // Calculate line coordinates and instantiate line
                    float lineY = (point1Y + point2Y) / 2;
                    float lineX = point1X;
                    GameObject line = Instantiate(linePrefab, new Vector3(lineX, lineY, 0f), Quaternion.identity);
                    line.transform.SetParent(lines.transform, false);
                    // Set the line height
                    line.GetComponent<RectTransform>().sizeDelta = new Vector2(DefaultValues.lineThickness, Mathf.Abs(point1Y - point2Y));
                }
            }
    }

    // Updates the current state of the board
    public void UpdateCurrentRound(GamePhase gamePhase, long currentPlayerId)
    {
        // Clears the board first
        ClearBoard();

        // Actions depending on the phase of the gmae
        switch (gamePhase)
        {
            // If it is the placing phase or the second part of the flying phase
            case GamePhase.Placing:
            case GamePhase.Flying2:
                // For each point on the board, if no player is on it, make it available
                // If there is already a player on that point, make the point illegal
                foreach (Point point in board)
                    if (point.GetPlayerId() == DefaultValues.freePointPlayerId)
                        point.SetPickable();
                    else
                        point.SetIllegal();
                break;
            // If it the first part of the moving phase
            case GamePhase.Moving1:
                bool hasMoves = false;
                // For each point on the board, if it is the current player's and it has
                // available spaces to move, make it available, otherwise, make it illegal
                foreach (Point point in board)
                {
                    if (point.GetPlayerId() == currentPlayerId)
                    {
                        List<Point> neighbours = FindNeighbours(point);
                        bool canMove = false;
                        foreach (Point neighbour in neighbours)
                            if (neighbour.GetPlayerId() == DefaultValues.freePointPlayerId)
                            {
                                point.SetPickable();
                                canMove = true;
                                hasMoves = true;
                                break;
                            }
                        if (!canMove)
                            point.SetIllegal();
                    }
                    else
                        point.SetIllegal();
                }

                if (!hasMoves)
                {
                    CurrentPlayerLostGame();
                }
                break;
            // If it is the second part of the moving phase
            case GamePhase.Moving2:
                // Find the moving point
                Point movingPoint = null;
                foreach (Point point in board)
                {
                    if (point.IsMoving())
                        movingPoint = point;
                }

                List<Point> movingPieceNeighbours = FindNeighbours(movingPoint);
                foreach (Point point in board)
                {
                    if (point.GetPlayerId() == DefaultValues.freePointPlayerId &&
                        movingPieceNeighbours.Contains(point))
                        point.SetPickable();
                    else
                        point.SetIllegal();
                }

                break;
            // If it is the flying phase
            case GamePhase.Flying1:
                foreach (Point point in board)
                {
                    if (point.GetPlayerId() == currentPlayerId)
                        point.SetPickable();
                    else
                        point.SetIllegal();
                }
                break;
            // If it is the removing phase
            case GamePhase.Removing:
                // TODO: Cannot remove in mills unless that is the only choice
                // For each point on the board, if the point is from the other player
                // and is NOT included in any mills, make it pickable, otherwise,
                // make it illegal
                bool allPointsInMills = true;
                foreach (Point point in board)
                {
                    if (point.GetPlayerId() != DefaultValues.freePointPlayerId &&
                        point.GetPlayerId() != currentPlayerId)
                    {
                        // Checks number of mills from opposing player for this point 
                        int millNumber = MillNumber(point, 3 - currentPlayerId, false);
                        if (millNumber == 0)
                        {
                            point.SetPickable();
                            allPointsInMills = false;
                        }
                        else
                            point.SetIllegal();
                    }
                    else
                    {
                        point.SetIllegal();
                    }
                }

                // If all removable points are included in a mill, the board is cleared and
                // all opposing player's points are made pickable
                if (allPointsInMills)
                {
                    ClearBoard();

                    foreach (Point point in board)
                        if (point.GetPlayerId() != DefaultValues.freePointPlayerId &&
                        point.GetPlayerId() != currentPlayerId)
                            point.SetPickable();
                }

                break;
        }
    }

    // Clears all points on the board
    public void ClearBoard()
    {
        foreach (Point point in board)
            point.Clear();
    }

    // Returns points that are neighbours of the given point
    public List<Point> FindNeighbours(Point point)
    {
        List<Point> neighbours = new();

        foreach (Point otherPoint in board)
        {
            // Neighbours are calculated by only having a difference of 1 for coordinates and
            // are not diagonal
            int xDifference = Mathf.Abs(otherPoint.GetCircleIndex() - point.GetCircleIndex());
            int yDifference = Mathf.Abs(otherPoint.GetColumnIndex() - point.GetColumnIndex());
            int zDifference = Mathf.Abs(otherPoint.GetRowIndex() - point.GetRowIndex());
            int difference = xDifference + yDifference + zDifference;
            if (otherPoint != point && difference == 1 &&
                (yDifference == 0 && zDifference == 0 && (otherPoint.GetColumnIndex() + otherPoint.GetRowIndex()) % 2 != 0 ||
                yDifference != 0 || zDifference != 0))
                neighbours.Add(otherPoint);
        }

        return neighbours;
    }

    // Returns true if a mill is formed for player with the
    // given id when looking at the given point
    public int MillNumber(Point point, long playerId, bool activateMillSymbol)
    {
        // A new list which will store all second neighbours of this
        // point (second neighbour = difference of 2 in coordinates)
        List<Point> secondNeighbours = new List<Point>();
        // Stores the number of mills made
        int millNumber = 0;

        // The given point's coordinates
        int pointX = point.GetCircleIndex();
        int pointY = point.GetColumnIndex();
        int pointZ = point.GetRowIndex();

        // Go through each point on the board
        foreach (Point otherPoint in board)
        {
            // If this is the point we are observing or if it not owned
            // by the player we are observing, ignore it
            if (point == otherPoint || otherPoint.GetPlayerId() != playerId)
                continue;

            // The other point's coordinates
            int otherPointX = otherPoint.GetCircleIndex();
            int otherPointY = otherPoint.GetColumnIndex();
            int otherPointZ = otherPoint.GetRowIndex();

            // Calculating the difference of the two point's coordinate
            // values
            int difference = Mathf.Abs(pointX - otherPointX) +
                Mathf.Abs(pointY - otherPointY) + Mathf.Abs(pointZ - otherPointZ);

            // If the value is 2 or less, these points are second neigbours
            // and that point is added to the second neighbour list
            if (difference <= 2)
                secondNeighbours.Add(otherPoint);
        }

        // For each pair of second neighbours, check if they make a mill
        // with the given point
        foreach (Point point1 in secondNeighbours)
            foreach (Point point2 in secondNeighbours)
            {
                // If the pair of points contains the same points, ignore
                // that pair
                if (point1 == point2)
                    continue;

                int point1X = point1.GetCircleIndex();
                int point1Y = point1.GetColumnIndex();
                int point1Z = point1.GetRowIndex();
                int point2X = point2.GetCircleIndex();
                int point2Y = point2.GetColumnIndex();
                int point2Z = point2.GetRowIndex();

                // If the three points form a line, in other words, if only
                // one coordinates changed through the three points, while the
                // other coordinates are the same, these three points form
                // a mill
                bool xCoordinateSame = pointX == point1X && pointX == point1X;
                bool yCoordinateSame = pointY == point1Y && pointY == point2Y;
                bool zCoordinateSame = pointZ == point1Z && pointZ == point2Z;

                // If two coordinates are the same, a mill was formed
                // Additionally, if the y and z coordinate are the same, there is
                // a chance this could be the corners of rings, which do not have a line
                // connecting them, so it is also checked that y + z are not even,
                // because if they are even, it means they are on the diagonal
                if ((xCoordinateSame && yCoordinateSame) ||
                    (xCoordinateSame && zCoordinateSame) ||
                    (yCoordinateSame && zCoordinateSame && (pointY + pointZ) % 2 != 0))
                {
                    // The maximum allowed difference on all coordinates between
                    // two points to form a mill is two
                    int xMaxDifference = Mathf.Max(Mathf.Abs(pointX - point1X), Mathf.Abs(pointX - point2X), Mathf.Abs(point1X - point2X));
                    int yMaxDifference = Mathf.Max(Mathf.Abs(pointY - point1Y), Mathf.Abs(pointY - point2Y), Mathf.Abs(point1Y - point2Y));
                    int zMaxDifference = Mathf.Max(Mathf.Abs(pointZ - point1Z), Mathf.Abs(pointZ - point2Z), Mathf.Abs(point1Z - point2Z));

                    if (xMaxDifference > 2 || yMaxDifference > 2 || zMaxDifference > 2)
                        continue;

                    // Increase the number of found mills
                    millNumber++;

                    // Activate mill symbols on the points forming a mill
                    if (activateMillSymbol)
                    {
                        point.ActivateMillSymbol();
                        point1.ActivateMillSymbol();
                        point2.ActivateMillSymbol();
                    }
                }
            }

        // Returns the number of formed mills divided by 2, since
        // the neighbours are looked at as a pair
        return millNumber / 2;
    }

    // Deactivates the mill symbols of all points on the board
    public void ClearMillSymbols()
    {
        foreach (Point point in board)
            point.DeactivateMillSymbol();
    }

    // Sets the player id of the given point
    public void SetPointPlayerId(Point point, long currentPlayerId)
    {
        point.SetPlayerId(currentPlayerId);
    }

    // Clears the player id from the given point
    public void RemovePlayerId(Point point)
    {
        point.RemovePlayerId();
    }

    // Plays the sound effect for an illegal move on the board
    public void PlayIllegalMoveSoundEffect()
    {
        illegalMoveSoundEffectSource.PlaySoundEffect();
    }

    // Plays the sound effect for a legal move on the board
    public void PlayLegalMoveSoundEffect()
    {
        legalMoveSoundEffectSource.PlaySoundEffect();
    }

    // Plays the place animation for the given point, coloring
    // it in the color of the current player's color
    public void PlayPlaceAnimation(Point point, string colorHexValue)
    {
        point.PlayPlaceAnimation(colorHexValue);
    }

    // Plays the men remove animation for the given point
    public void PlayRemoveAnimation(Point point)
    {
        point.PlayRemoveAnimation();
    }

    // Enables the outline of the given point
    public void SetMoving(Point point, bool moving)
    {
        point.SetMoving(moving);
    }

    // For all points on the board, sets them to non-moving
    public void ClearMovingPoints()
    {
        foreach (Point point in board)
            point.SetMoving(false);
    }
}
