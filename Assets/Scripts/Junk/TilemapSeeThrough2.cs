using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSeeThrough2 : MonoBehaviour
{
    List<Vector3Int> trackedCells;
    Tilemap tilemap;
    GridLayout gridLayout;

    void Awake()
    {
        trackedCells = new List<Vector3Int>();
        tilemap = GetComponent<Tilemap>();
        gridLayout = GetComponentInParent<GridLayout>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // NB: Bounds cannot have zero width in any dimension, including z
        var cellBounds = new BoundsInt(
            gridLayout.WorldToCell(other.bounds.min),
            gridLayout.WorldToCell(other.bounds.size) + new Vector3Int(0, 0, 1));

        IdentifyIntersections(other, cellBounds);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Same as OnTriggerEnter2D()
        var cellBounds = new BoundsInt(
            gridLayout.WorldToCell(other.bounds.min),
            gridLayout.WorldToCell(other.bounds.size) + new Vector3Int(0, 0, 1));

        IdentifyIntersections(other, cellBounds);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Intentionally pass zero size bounds
        IdentifyIntersections(other, new BoundsInt(Vector3Int.zero, Vector3Int.zero));
    }

    void IdentifyIntersections(Collider2D other, BoundsInt cellBounds)
    {
        // Take a copy of the tracked cells
        var exitedCells = trackedCells.ToList();

        // Find intersections within cellBounds
        foreach (var cell in cellBounds.allPositionsWithin)
        {
            // First check if there's a tile in this cell
            if (tilemap.HasTile(cell))
            {
                // Find closest world point to this cell's center within other collider
                var cellWorldCenter = gridLayout.CellToWorld(cell);
                var otherClosestPoint = other.ClosestPoint(cellWorldCenter);
                var otherClosestCell = gridLayout.WorldToCell(otherClosestPoint);

                // Check if intersection point is within this cell
                if (otherClosestCell == cell)
                {
                    if (!trackedCells.Contains(cell))
                    {
                        // other collider just entered this cell
                        trackedCells.Add(cell);

                        // Do actions based on other collider entered this cell
                        Debug.Log("behind");
                    }
                    else
                    {
                        // other collider remains in this cell, so remove it from the list of exited cells
                        exitedCells.Remove(cell);
                    }
                }
            }
        }

        // Remove cells that are no longer intersected with
        foreach (var cell in exitedCells)
        {
            trackedCells.Remove(cell);

            // Do actions based on other collider exited this cell
            Debug.Log("left");
        }
    }
}
