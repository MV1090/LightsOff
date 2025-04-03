using UnityEngine;

public class GameTypeManager : Singleton<GameTypeManager>
{
    // Define the size of the grid
    public int gridWidth = 3; // 3 columns
    public int gridHeight = 3; // 3 rows

    public float gridOffsetX;
    public float gridOffsetY;


    // Create a 2D array to store the X and Y coordinates
    public Vector2[,] gridCoordinates;

    void Start()
    {
        // Initialize the 2D array with the given grid dimensions
        gridCoordinates = new Vector2[gridWidth, gridHeight];

        // Use two for loops to populate the array with X and Y positions
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {

                float xPos = x * gridOffsetX;
                float yPos = y * gridOffsetY;
                // Store the X and Y position at each [x, y] in the array
                //gridCoordinates[x, y] = x + y * gridWidth;
                gridCoordinates[x, y] = new Vector2(xPos, yPos); 

                // Optionally, print the coordinates (for debugging)
                Debug.Log($"Grid Position ({x}, {y}) = {gridCoordinates[x, y]}");
            }
        }               
    }
}
