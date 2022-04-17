using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public float health;
    public int maxHealth;
    public bool isChube;
    public int maxAmount;
    public float healAmount;

    public static float enemyDamage = 0.003f;
    public Tile pollutedTile;
    public Tile tile;
    public Tilemap tilemap;
    public TilemapRenderer tRenderer;
    public PrefabBrushManager manager;
    public TilemapController tilemapController;

    private Vector3Int pos;

    public float renderDecayThreshold = 0.75f;

    public bool isPolluted = false;
    private bool colliding = false;

    void Start()
    {
        health = maxHealth;
        GameObject temp = GameObject.FindGameObjectWithTag("Tilemap");
        tilemap = temp.GetComponent<Tilemap>();
        tRenderer = temp.GetComponent<TilemapRenderer>();
        manager = temp.GetComponent<TilemapController>().prefabBrushManagerTemp;
        tilemapController = temp.GetComponent<TilemapController>();

        pos = tilemap.WorldToCell(transform.position);
        pos.z = tRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            manager.subtractCount(gameObject.name);
            if (isChube)
            {
                onChubeDeath();
            }

            tilemap.SetTile(pos, null);
            tilemapController.coroutine(pos);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (!tilemap.HasTile(pos)) //tile set to null by cascader
        {
            cascadeDestroy();
        }

        tilemap.SetTile(pos, (health < renderDecayThreshold * maxHealth) ? pollutedTile : tile);

        if (isChube && !colliding && health <= maxHealth) health += healAmount;
    }

    public void cascadeDestroy() //called by cascader
    {
        manager.subtractCount(gameObject.name);
        if (isChube)
        {
            onChubeDeath();
        }
        tilemap.SetTile(pos, null);
        Destroy(gameObject);
        Destroy(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && gameObject.tag == "Walkable") {
            CallTileManually();
        }
    }

    public void CallTileManually() {
        print("HIT");
        print(pos);
        
        tilemap.SetTile(pos, pollutedTile);
        isPolluted = true;
        colliding = true;
    }
    
    void OnTriggerStay2D()
    {
        health -= enemyDamage * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliding = false;
    }
    public void damage(float amount) {
        //Debug.Log("STRUCTURE BEING DAMAGED. HEALTH AT: " + health);
        health -= amount;
    }


    private void onChubeDeath() {
        SceneManager.LoadScene("End Monologue");
    }

    // TODO: convert to a get/set field/property thing
    public void UpdateHealth(float increment)
    {
        health += increment;
    }
    
    public void UpdatePollutionStatus(bool status)
    {
        isPolluted = status;
    }
}
