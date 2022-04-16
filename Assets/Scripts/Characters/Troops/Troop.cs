using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Troop : MonoBehaviour //TODO: inherit from pathfinder
{
    private SpriteRenderer spriteRenderer;
    public Controller movementController;
    public bool chosen;
    public Energy energy;
    public int moveCost;

    public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;
    public List<TileBase> walkableTiles;

    private TileManager structure;

    //private Vector2[] directions = new Vector2[] { new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1) };

    private void Start()
    {
        energy = GameObject.Find("Energy").GetComponent<Energy>();
        movementController = GameObject.Find("TroopMovement").GetComponent<Controller>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private bool isValidMoveSpot(Vector3Int movePos)
    {
        if (energy.amount < moveCost) return false;
        if (walkableTiles.Contains(tilemap.GetTile(movePos))) return true;
        return false;
    }

    void Update()
    {
        Vector3Int selfpos = tilemap.WorldToCell(transform.position);
        selfpos.z = tilemapRenderer.sortingOrder;
        Vector3Int mouseTile = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mouseTile.z = tilemapRenderer.sortingOrder;
        
        if (!tilemap.HasTile(selfpos)) Destroy(gameObject);
        if (Input.GetButtonDown("Fire1") && tilemap.HasTile(mouseTile) && chosen && isValidMoveSpot(mouseTile))
        {
            energy.amount -= moveCost;
            StopAllCoroutines();
            StartCoroutine(movementController.Move(transform, tilemap.WorldToCell(transform.position), mouseTile));
        }

        // If place is valid thru specific troop's range
        if (mouseTile == tilemap.WorldToCell(transform.position) || chosen)
        {
            spriteRenderer.color = Color.green;
            if (Input.GetButtonDown("Fire1"))
            {
                chosen = true;
            }
        }
        else {
            spriteRenderer.color = Color.white;
        }

        if (Input.GetButtonDown("Fire1") && chosen && mouseTile != tilemap.WorldToCell(transform.position)) {
            chosen = false;
        }
    }
}
