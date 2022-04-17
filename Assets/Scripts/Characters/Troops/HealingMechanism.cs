using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HealingMechanism : MonoBehaviour
{
    public float rate;                  // healing rate per second (possibly convert to dictionary if rates vary)
    public float epsilonDistance;       // movement threshold when deciding whether to trigger healing
    public float epsilonHealth;         // movement threshold when deciding whether to trigger healing

    [SerializeField] private string[] healableLayerNames;
    
    private readonly Vector2[] _directionDeltas =
    {
        new Vector2(-1, +1),
        new Vector2(+1, -1),
        new Vector2(+1, +1),
        new Vector2(-1, -1),
    };

    private Vector2 _previousPosition;

    // TODO: simplify by checking if current position is destination
    private bool CheckMovementStatic()
    {
        return Vector2.Distance(transform.position, _previousPosition) < epsilonDistance;
    }

    private bool UpdateTile(GameObject tile)
    {
        TileManager tileInterface = tile.GetComponent<TileManager>();

        if (tileInterface.isPolluted || Math.Abs(tileInterface.health - tileInterface.maxHealth) > epsilonHealth)
        {
            tileInterface.UpdateHealth(rate * Time.deltaTime);
            tileInterface.UpdatePollutionStatus(false);
            
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        int layerMask = 1;
        
        foreach (Vector2 directionDelta in _directionDeltas)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionDelta, 3, layerMask);
                

            if (hit && healableLayerNames.Any(hit.collider.gameObject.tag.Contains) && CheckMovementStatic())
            {
                if (UpdateTile(hit.transform.gameObject))
                {
                    Debug.DrawRay(transform.position, new Vector3(directionDelta.x, directionDelta.y, 0), Color.red);
                    break;
                }
            }
        }

        _previousPosition = transform.position;
    }
}
