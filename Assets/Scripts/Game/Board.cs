using System.Collections.Generic;
using UnityEngine;

// The board contains points that players can place
// their men on
public class Board
{
    // A list of all points on the board
    private List<Point> board;

    // Initializes the board according to the ring number
    public void Initialize(int ringNumber)
    {
        board = new List<Point>();

        // The number of columns and rows in each circle
        int columnNumber = 3;
        int rowNumber = 3;

        // The center of columns and rows
        int columnCenter = 1;
        int rowCenter = 1;

        // Going through the number of rings
        for (int i = 0; i < ringNumber; i++)
            // Going through the number of columns
            for (int j = 0; j < columnNumber; j++)
                // Going through the number of rows
                for (int k = 0; k < rowNumber; k++)
                {
                    // If this location would be in the center, ignore it, since
                    // the circle's center isn't a playable position
                    if (j == columnCenter && k == rowCenter)
                        continue;

                    // Initializes a point with the given coordinates
                    Vector3Int pointPosition = new(i, j, k);
                    Point point = new(ref pointPosition);
                    board.Add(point);
                }
    }
}
