using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFind;

public class Pathing : MonoBehaviour {

    public int mapWidth;
    public int mapHeight;

    public float pathingDensity;
    static float density;

    static Grid grid;

    void Start()
    {
        density = pathingDensity;
        float[,] tilesmap = new float[mapWidth, mapHeight];

        int x = 0;
        int y = 0;
        while (x < mapWidth)
        {
            y = 0;
            while (y < mapHeight)
            {
                tilesmap[x, y] = 1;
                y++;
            }

            x++;
        }

        grid = new Grid(mapWidth, mapHeight, tilesmap);

    }

    public static List<Point> getPath(Point start, Point target)
    {
        return (Pathfinding.FindPath(grid, start, target));
    }

    public static Vector3 pointToPosition(Point input)
    {
        Vector2 test = new Vector2((float)input.x / density, (float)input.y / density);
        Debug.Log(test);
        return test;
    }

    public static Point positionToPoint(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x * density));
        x = x >= 0 ? x : 0;

        int y = Mathf.FloorToInt((position.y * density));
        y = y >= 0 ? y : 0;
        return new Point(x,y);
    }
}
