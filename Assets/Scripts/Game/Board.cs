using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// The board contains points that players can place
// their men on
public class Board : MonoBehaviour
{
    // A list of all points on the board
    private List<Point> board;
    // Prefab for a point
    [SerializeField] private GameObject pointPrefab;
    // Prefab for a line betwene points
    [SerializeField] private GameObject linePrefab;

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
        var corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        var width = Mathf.Abs(corners[2].x - corners[0].x);
        var height = Mathf.Abs(corners[2].y - corners[0].y);

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
                    point.transform.SetParent(transform, false);
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
                    line.transform.SetParent(transform, false);
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
                    line.transform.SetParent(transform, false);
                    // Set the line height
                    line.GetComponent<RectTransform>().sizeDelta = new Vector2(DefaultValues.lineThickness, Mathf.Abs(point1Y - point2Y));
                }
            }
    }
}
