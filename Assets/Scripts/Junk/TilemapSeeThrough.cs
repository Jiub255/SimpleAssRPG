using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TilemapSeeThrough : MonoBehaviour
{
    //STANDINGHERE BOOL ISNT WORKING, JUST HAVE ONE FOR WHOLE TILESET, NEED INDIVIDUAL BOOLS FOR EACH TILE I THINK
    //Getting better. need to not restart lerp coroutine if moving after standing still for a while.
    //

    //Set a bool on triggerenter or stay, then set it to false on exit?


    public float lerpDuration = 1f;
    public float endAlpha = 0.4f;
    public float stayFadedDuration = 1;

    private Tilemap tilemap;

    public bool standingHere = false; //NOT WORKING YET

    //private bool CRStarted = false;

    //private bool countingDown = false;
    //public float timer = 3f;

    void Start()
    {
        tilemap = GetComponent<Tilemap>(); //gets tilemap component reference

        //unlocks color changing for all tiles in tilemap
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.RemoveTileFlags(tilePosition, TileFlags.LockColor);
        }
    }

    /*
    private void Update()
    {
        if (countingDown)
        {
            timer -= Time.deltaTime;//will each tile need a separate timer? bit confused with this script
            if (timer <= 0)
            {
                StartCoroutine(DeLerpColour(tilePosition));
                //delerp here, but go back to lerping if player comes back. use stopCoroutine
            }
        }
    }
    */

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Vector3 centre = collider.bounds.center; //center of player's trigger collider
            Vector3 min = collider.bounds.min; //bottom left corner of collider
            Vector3 max = collider.bounds.max; //top right corner of collider

            Vector3[] corners =
            {

                //may only need like four corners instead of nine

                new Vector3(min.x, min.y, 0f), new Vector3(min.x, max.y, 0f), new Vector3(min.x, centre.y, 0f),
                new Vector3(max.x, min.y, 0f), new Vector3(max.x, max.y, 0f), new Vector3(max.x, centre.y, 0f),
                // O  x  O
                // O  x  O   six of nine tiles near player? unless collider is smaller than a tile.
                // O  x  O   why not just the four corners then? code from some other random game tbf.
                //adding the middle row for now
                new Vector3(centre.x, min.y, 0f), new Vector3(centre.x, max.y, 0f), new Vector3(centre.x, centre.y, 0f),
            };

            foreach (Vector3 corner in corners)
            {
                Vector3Int tilePosition = tilemap.WorldToCell(corner); //converting corner position to an integer grid?
                if (tilemap.HasTile(tilePosition)) //checks if there's a tile there
                {

                    //need a left and came back bool too?

                    if (!standingHere)
                    {
                        StopCoroutine(LerpColour(tilePosition));
                        standingHere = true;
                    }
                        StartCoroutine(LerpColour(tilePosition)); //runs the coroutine to fade the tile to transparent
                    //if (standinghere), do nothing then?
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            standingHere = false;
        }
    }


    /*
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            //countingDown = true; //check for this in update maybe?

            Vector3 centre = collision.bounds.center; //player min/max when leaving collision with specific tile?
            Vector3 min = collision.bounds.min;
            Vector3 max = collision.bounds.max;

            Vector3[] chorners =
            {
                new Vector3(min.x, min.y, 0f), new Vector3(min.x, max.y, 0f), new Vector3(min.x, centre.y, 0f),
                new Vector3(max.x, min.y, 0f), new Vector3(max.x, max.y, 0f), new Vector3(max.x, centre.y, 0f),
                // same six corner thing
                //adding the middle
                new Vector3(centre.x, min.y, 0f), new Vector3(centre.x, max.y, 0f), new Vector3(centre.x, centre.y, 0f),
            };

            foreach (Vector3 corner in chorners)
            {
                Vector3Int tilePosition = tilemap.WorldToCell(corner);//convert to int grid
                if (tilemap.HasTile(tilePosition)) //does tile exist here?
                {
                    //stopcoroutine lerpcolor here? then start it right back up?


                    //set some bool isDeLerping here? will it work in a foreach tile loop?
                    //StartCoroutine(DeLerpColour(tilePosition)); //attempted delerp coroutine to set back alpha to 1
                }
            }
        }
    }*/

    IEnumerator LerpColour(Vector3Int tilePosition)
    {
       // CRStarted = true;

        Color lerpedColour = tilemap.GetColor(tilePosition);
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, Mathf.Lerp(1, endAlpha, timeElapsed / lerpDuration));
            tilemap.SetColor(tilePosition, lerpedColour);
            timeElapsed += Time.deltaTime;

            //if you leave the tile, it skips to the waitforseconds part
            if (!standingHere)
            {
                timeElapsed = lerpDuration;
                yield return null;
            }

            yield return null;
        }

        lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, endAlpha);
        tilemap.SetColor(tilePosition, lerpedColour);

        //could do wait for seconds 3f or so here, then do another while loop that does the opposite of the above one. Need a second timer variable.
        yield return new WaitForSeconds(stayFadedDuration);
        float timeElapsed2 = 0;


        //put below in an if(NOTstandingherestill) {}
        //OR, if (standingherestill){yield return}?
        while (standingHere)
        {
            Debug.Log("standinghere is true");
            yield return null;
        }
        //the frame you stop standinghere, the fade back to solid begins
        while (timeElapsed2 < lerpDuration)
        {
            lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, Mathf.Lerp(endAlpha, 1, timeElapsed2 / lerpDuration));
            tilemap.SetColor(tilePosition, lerpedColour);
            timeElapsed2 += Time.deltaTime;

            yield return null;
        }

        //tilemap.SetTileFlags(tilePosition, TileFlags.LockColor);
        //why relock color? 

        //CRStarted = false;
    }

    /*
    //this whole coroutine might be unnecessary, could do wait a few seconds then fade back to full alpha in LerpColour, then have it interrupted and started over if player gets near tile again before then.
    IEnumerator DeLerpColour(Vector3Int tilePosition)
    {
        Color lerpedColour = tilemap.GetColor(tilePosition);
        float timeElapsed = 0;
        //try the timer here
        float timeUntilDelerp = timer;

        while (timeElapsed < timeUntilDelerp)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        while (timeElapsed >= timeUntilDelerp && timeElapsed < lerpDuration)
        {
            lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, Mathf.Lerp(endAlpha, 1, timeElapsed / lerpDuration));
            tilemap.SetColor(tilePosition, lerpedColour);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        lerpedColour = new Color(lerpedColour.r, lerpedColour.g, lerpedColour.b, endAlpha);
        tilemap.SetColor(tilePosition, lerpedColour);
        tilemap.SetTileFlags(tilePosition, TileFlags.LockColor);
    }

    */
}


//public class TilemapSeeThrough : MonoBehaviour
//{
   /* public Transform player;
    public Tilemap tm;

    public void Update()
    {
        tm.RefreshAllTiles();

        int x = Mathf.RoundToInt(player.position.x - 0.5f);
        int y = Mathf.RoundToInt(player.position.y - 0.5f);
        Vector3Int pp = new Vector3Int(x, y, 0);

        tm.SetTileFlags(pp, TileFlags.None);
        Debug.Log(tm.GetColor(new Vector3Int(pp.x, pp.y, 0)));
        Color seeThrough = new Color(1f, 1f, 1f, 0.3f);
        tm.SetColor(pp, seeThrough);
    }
   */
