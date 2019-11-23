using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class Wall : Tile
{
    public readonly float WIDTH = 1F;
    public readonly float HEIGHT = 4F;

    [SerializeField]
    public Sprite[] walls;

    private const string left = "tfffffttftfttfttttf";
    private const string right = "ftfffttftfttftttttf";
    private const string top = "ttttftfttfffffttffff";
    private const string bottom = "fftfttttfttfffftttf";

    private static int offset = 0;
    private static List<int> tri = new List<int>();
    private static List<Vector3> vects = new List<Vector3>();

    public override bool StartUp(Vector3Int loc, ITilemap map, GameObject go)
    {
        base.StartUp(loc, map, go);
        if (Application.isPlaying)
        {
            CreateWall(loc, State(loc, map));
        }
        return true;
    }

    public override void RefreshTile(Vector3Int pos, ITilemap tilemap)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int p = new Vector3Int(pos.x + x, pos.y + y, pos.z);
                if (IsWall(tilemap, p))
                {
                    tilemap.RefreshTile(p);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int pos, ITilemap tilemap, ref TileData data)
    {
        int state = State(pos, tilemap);
        data.sprite = walls[state];
        data.colliderType = ColliderType.Grid;
    }

    public bool IsWall(ITilemap map, Vector3Int pos)
    {
        return map.GetTile(pos) == this;
    }

    public int State(Vector3Int pos, ITilemap tilemap)
    {
        bool left = false, right = false, bottom = false, top = false;
        if (IsWall(tilemap, new Vector3Int(pos.x - 1, pos.y, pos.z)))
            left = true;
        if (IsWall(tilemap, new Vector3Int(pos.x + 1, pos.y, pos.z)))
            right = true;
        if (IsWall(tilemap, new Vector3Int(pos.x, pos.y - 1, pos.z)))
            bottom = true;
        if (IsWall(tilemap, new Vector3Int(pos.x, pos.y + 1, pos.z)))
            top = true;

        int state = 0;
        if (!left && right && bottom && !top)
            state = 0;
        else if (left && !right && !top && bottom)
            state = 1;
        else if (left && right && !top && !bottom)
            state = 2;
        else if (left && right && !top && bottom)
            state = 3;
        else if (left && right && top && !bottom)
            state = 4;
        else if (left && !right && !top && !bottom)
            state = 5;
        else if (!left && !right && top && !bottom)
            state = 6;
        else if (!left && right && !top && bottom)
            state = 7;
        else if (left && !right && !top && bottom)
            state = 8;
        else if (!left && right && top && !bottom)
            state = 9;
        else if (left && !right && top && !bottom)
            state = 10;
        else if (!left && !right && top && bottom)
            state = 11;
        else if (!left && right && top && bottom)
            state = 12;
        else if (left && !right && top && bottom)
            state = 13;
        else if (!left && !right && !top && bottom)
            state = 14;
        else if (!left && right && !top && !bottom)
            state = 15;
        else if (!left && right && top && !bottom)
            state = 16;
        else if (left && !right && top && !bottom)
            state = 17;
        else if (left && right && top && bottom)
            state = 18;
        return state;
    }

    private void CreateWall(Vector3Int position, int state)
    {
        int x = position.x;
        int y = position.y;
        int bottomLeft = -1;
        int topLeft = -1;
        int topRight = -1;
        int bottomRight = -1;
        
        if (left[state] == 't')
        {
            
            if (bottomLeft == -1)
            {
                vects.Add(new Vector3(x, y, 0));
                vects.Add(new Vector3(x, y, HEIGHT));
                bottomLeft = offset;
                offset += 2;
            }
            if (topLeft == -1)
            {
                vects.Add(new Vector3(x, y + WIDTH, 0));
                vects.Add(new Vector3(x, y + WIDTH, HEIGHT));
                topLeft = offset;
                offset += 2;
            }
            tri.Add(bottomLeft);
            tri.Add(topLeft + 1);
            tri.Add(topLeft);
            tri.Add(bottomLeft);
            tri.Add(bottomLeft + 1);
            tri.Add(topLeft + 1);
        }
        
        if (top[state] == 't')
        {
            if (topLeft == -1)
            {
                vects.Add(new Vector3(x, y + WIDTH, 0));
                vects.Add(new Vector3(x, y + WIDTH, HEIGHT));
                topLeft = offset;
                offset += 2;
            }
            if (topRight == -1)
            {
                vects.Add(new Vector3(x + WIDTH, y + WIDTH, 0));
                vects.Add(new Vector3(x + WIDTH, y + WIDTH, HEIGHT));
                topRight = offset;
                offset += 2;
            }
            tri.Add(topLeft);
            tri.Add(topLeft + 1);
            tri.Add(topRight);
            tri.Add(topLeft + 1);
            tri.Add(topRight + 1);
            tri.Add(topRight);
        }

        if (right[state] == 't')
        {
            if (topRight == -1)
            {
                vects.Add(new Vector3(x + WIDTH, y + WIDTH, 0));
                vects.Add(new Vector3(x + WIDTH, y + WIDTH, HEIGHT));
                topRight = offset;
                offset += 2;
            }
            if (bottomRight == -1)
            {
                vects.Add(new Vector3(x + WIDTH, y, 0));
                vects.Add(new Vector3(x + WIDTH, y, HEIGHT));
                bottomRight = offset;
                offset += 2;
            }
            tri.Add(topRight);
            tri.Add(topRight + 1);
            tri.Add(bottomRight + 1);
            tri.Add(topRight);
            tri.Add(bottomRight + 1);
            tri.Add(bottomRight);
        }
        
        if (bottom[state] == 't')
        {
            if (bottomRight == -1)
            {
                vects.Add(new Vector3(x + WIDTH, y, 0));
                vects.Add(new Vector3(x + WIDTH, y, HEIGHT));
                bottomRight = offset;
                offset += 2;
            }
            if (bottomLeft == -1)
            {
                vects.Add(new Vector3(x, y, 0));
                vects.Add(new Vector3(x, y, HEIGHT));
                bottomLeft = offset;
                offset += 2;
            }
            tri.Add(bottomRight);
            tri.Add(bottomRight + 1);
            tri.Add(bottomLeft + 1);
            tri.Add(bottomRight);
            tri.Add(bottomLeft + 1);
            tri.Add(bottomLeft);
        }
    }
}
