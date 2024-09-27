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
        foreach (Point point in board)
            point.Clear();

        // Actions depending on the phase of the gmae
        switch (gamePhase)
        {
            // If it is the placing phase
            case GamePhase.Placing:
                // For each point of the board, if no player is on it, make it available
                // If there is already a player on that point, make the point illegal
                foreach (Point point in board)
                    if (point.GetPlayerId() == DefaultValues.freePointPlayerId)
                        point.SetPickable();
                    else
                        point.SetIllegal();
                break;
            // If it the moving phase
            case GamePhase.Moving:
                break;
            // If it is the flying phase
            case GamePhase.Flying:
                break;
        }
    }

    // Returns true if a mill is formed for player with the
    // given id when looking at the given point
    public int MillNumber(Point point, long playerId)
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

                // If the three points form a line, in other words, if only
                // one coordinates changed through the three points, while the
                // other coordinates are the same, these three points form
                // a mill
                bool xCoordinateSame = pointX == point1.GetCircleIndex() &&
                    pointX == point2.GetCircleIndex();
                bool yCoordinateSame = pointY == point1.GetColumnIndex() &&
                    pointY == point2.GetColumnIndex();
                bool zCoordinateSame = pointZ == point1.GetRowIndex() &&
                    pointZ == point2.GetRowIndex();

                // If two coordinates are the same, a mill was formed
                // Additionally, if the y and z coordinate are the same, there is
                // a chance this could be the corners of rings, which do not have a line
                // connecting them, so it is also checked that y + z are not even,
                // because if they are even, it means they are on the diagonal
                if ((xCoordinateSame && yCoordinateSame) ||
                    (xCoordinateSame && zCoordinateSame) ||
                    (yCoordinateSame && zCoordinateSame && (pointY + pointZ) % 2 != 0))
                    millNumber++;
            }

        // Returns the number of formed mills divided by 2, since
        // the neighbours are looked at as a pair
        return millNumber / 2;
    }

    // Sets the player id of the current point
    public void SetPointPlayerId(Point point, long currentPlayerId)
    {
        point.SetPlayerId(currentPlayerId);
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
}
