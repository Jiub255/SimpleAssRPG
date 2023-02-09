using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSeeThrough3 : MonoBehaviour
{
    public float fadedAlpha = 0.4f;
    private Tilemap tilemap;

    private int xc;
    private int yc;
    [SerializeField] private float xOffset = -0.5f;
    [SerializeField] private float yOffset = -0.5f;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.RemoveTileFlags(tilePosition, TileFlags.LockColor);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //find the nearest tile to player, fade the nine tiles around that one
            xc = (int)Mathf.Round(collision.transform.position.x + xOffset);
            yc = (int)Mathf.Round(collision.transform.position.y + yOffset);

            Vector3Int[] surroundingTiles =
            {
                new Vector3Int(xc - 1, yc + 1, 0), new Vector3Int(xc, yc + 1, 0), new Vector3Int(xc + 1, yc + 1, 0),
                new Vector3Int(xc - 1, yc, 0), new Vector3Int(xc, yc, 0), new Vector3Int(xc + 1, yc, 0),
                new Vector3Int(xc - 1, yc - 1, 0), new Vector3Int(xc, yc - 1, 0), new Vector3Int(xc + 1, yc - 1, 0),
            };
            
            foreach (Vector3Int surroundingTile in surroundingTiles)
            {
                if (tilemap.HasTile(surroundingTile))
                {
                    Color color = tilemap.GetColor(surroundingTile);
                    color = new Color(color.r, color.g, color.b, fadedAlpha);
                    tilemap.SetColor(surroundingTile, color);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            xc = (int)Mathf.Round(collision.transform.position.x + xOffset);
            yc = (int)Mathf.Round(collision.transform.position.y + yOffset);

            Vector3Int[] outerEdges =
            {
                new Vector3Int(xc - 2, yc + 2, 0), //top row
                new Vector3Int(xc - 1, yc + 2, 0),
                new Vector3Int(xc, yc + 2, 0),
                new Vector3Int(xc + 1, yc + 2, 0),
                new Vector3Int(xc + 2, yc + 2, 0),

                new Vector3Int(xc - 2, yc - 2, 0), //bottom row
                new Vector3Int(xc - 1, yc - 2, 0),
                new Vector3Int(xc, yc - 2, 0),
                new Vector3Int(xc + 1, yc - 2, 0),
                new Vector3Int(xc + 2, yc - 2, 0),

                new Vector3Int(xc - 2, yc - 1, 0), //left side
                new Vector3Int(xc - 2, yc, 0),
                new Vector3Int(xc - 2, yc + 1, 0),

                new Vector3Int(xc + 2, yc - 1, 0), //right side
                new Vector3Int(xc + 2, yc, 0),
                new Vector3Int(xc + 2, yc + 1, 0),
            };

            foreach (Vector3Int outerEdge in outerEdges)
            {
                if (tilemap.HasTile(outerEdge)) //checks if there's a tile there
                {
                    Color color = tilemap.GetColor(outerEdge);
                    color = new Color(color.r, color.g, color.b, 1);
                    tilemap.SetColor(outerEdge, color);
                }
            }

            Vector3Int[] surroundingTiles =
            {
                new Vector3Int(xc - 1, yc + 1, 0), new Vector3Int(xc, yc + 1, 0), new Vector3Int(xc + 1, yc + 1, 0),
                new Vector3Int(xc - 1, yc, 0), new Vector3Int(xc, yc, 0), new Vector3Int(xc + 1, yc, 0),
                new Vector3Int(xc - 1, yc - 1, 0), new Vector3Int(xc, yc - 1, 0), new Vector3Int(xc + 1, yc - 1, 0),
            };

            foreach (Vector3Int surroundingTile in surroundingTiles)
            {
                if (tilemap.HasTile(surroundingTile))
                {
                    Color color = tilemap.GetColor(surroundingTile);
                    color = new Color(color.r, color.g, color.b, fadedAlpha);
                    tilemap.SetColor(surroundingTile, color);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            xc = (int)Mathf.Round(collision.transform.position.x + xOffset);
            yc = (int)Mathf.Round(collision.transform.position.y + yOffset);

            Vector3Int[] all25Tiles =
            {
                new Vector3Int(xc - 1, yc + 1, 0), new Vector3Int(xc, yc + 1, 0), new Vector3Int(xc + 1, yc + 1, 0), //middle nine
                new Vector3Int(xc - 1, yc, 0), new Vector3Int(xc, yc, 0), new Vector3Int(xc + 1, yc, 0),
                new Vector3Int(xc - 1, yc - 1, 0), new Vector3Int(xc, yc - 1, 0), new Vector3Int(xc + 1, yc - 1, 0),

                new Vector3Int(xc - 2, yc + 2, 0), //top row
                new Vector3Int(xc - 1, yc + 2, 0),
                new Vector3Int(xc, yc + 2, 0),
                new Vector3Int(xc + 1, yc + 2, 0),
                new Vector3Int(xc + 2, yc + 2, 0),

                new Vector3Int(xc - 2, yc - 2, 0), //bottom row
                new Vector3Int(xc - 1, yc - 2, 0),
                new Vector3Int(xc, yc - 2, 0),
                new Vector3Int(xc + 1, yc - 2, 0),
                new Vector3Int(xc + 2, yc - 2, 0),

                new Vector3Int(xc - 2, yc - 1, 0), //left side
                new Vector3Int(xc - 2, yc, 0),
                new Vector3Int(xc - 2, yc + 1, 0),

                new Vector3Int(xc + 2, yc - 1, 0), //right side
                new Vector3Int(xc + 2, yc, 0),
                new Vector3Int(xc + 2, yc + 1, 0),
            };

            foreach (Vector3Int allTile in all25Tiles)
            {
                if (tilemap.HasTile(allTile)) //checks if there's a tile there
                {
                    Color color = tilemap.GetColor(allTile);
                    color = new Color(color.r, color.g, color.b, 1);
                    tilemap.SetColor(allTile, color);
                }
            }
        }
    }
}
