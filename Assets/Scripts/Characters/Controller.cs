using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : MonoBehaviour {
	public Tilemap tilemap;
    public TilemapRenderer tilemapRenderer;

    public Pathfinder pathfinder;
    public Difficulty difficulty;

    public float baseSpeed;
    public float speed;

    private void Awake()
    {
        pathfinder.tilemap = tilemap;
    }

    private void Update()
    {
        speed = baseSpeed * difficulty.difficultyMultiplier;
    }

    public IEnumerator Move(Transform character, Vector3Int origin, Vector3Int destination)
    {
	    pathfinder.destinationLocation = origin;//tilemap.WorldToCell(origin);
        pathfinder.destinationLocation.z = tilemapRenderer.sortingOrder;
        pathfinder.originLocation = destination;//tilemap.WorldToCell(destination);
        pathfinder.originLocation.z = tilemapRenderer.sortingOrder;
		
        IEnumerable<Vector3Int> path = pathfinder.BackPropagatePath ();

		foreach (Vector3Int location in path)
		{
			Vector3 localTarget = tilemap.GetCellCenterWorld(location);
			while (character.position != localTarget) {
                
                character.position = Vector3.MoveTowards(character.position, localTarget, Time.deltaTime * speed);
				yield return new WaitForFixedUpdate();
			}
		}
	}
}