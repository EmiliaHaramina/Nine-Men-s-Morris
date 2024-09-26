using System.Collections.Generic;
using UnityEngine;

// The board contains points that players can place
// their men on
public class Board : MonoBehaviour
{
    // A list of all points on the board
    private List<Point> board;
    // Prefab for a point
    [SerializeField] private GameObject pointPrefab;

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

                    // Initializes a point with the given coordinates
                    float pointX = (i + 1) * widthPerCircle * (k - 1);
                    float pointY = -(i + 1) * heightPerCircle * (j - 1);
                    GameObject point = Instantiate(pointPrefab, new Vector3(pointX, pointY, 0f), Quaternion.identity);
                    //GameObject point = Instantiate(pointPrefab, new Vector3((i + 1) * (j - 1), (i + 1) * (k - 1), 0f), Quaternion.identity);
                    point.transform.SetParent(transform, false);
                    point.GetComponent<Point>().Initialize(i, j, k);
                    board.Add(point.GetComponent<Point>());
                }
    }
}
