using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fighting : MonoBehaviour
{
    public float damage;
    bool fighting;
    int layer;

    public float speed;
    Vector3 position;

    public float perceptionRadius;
    public float attackRadius;

    private bool returning;
    private Tilemap tilemap;

    void Start()
    {
        if (gameObject.tag == "Troop")
        {
            tilemap = GetComponent<Troop>().tilemap;
            layer = LayerMask.GetMask("Enemies");
        }
        else if (gameObject.tag == "Enemy")
        {
            tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            layer = LayerMask.GetMask("Troops");
        }
    }

    void Update()
    {
        GameObject character;
        if (!fighting && Raycast(out character))
        {
            fighting = true;
            StopAllCoroutines();
            StartCoroutine(Fight(character));
        }
    }

    IEnumerator Fight(GameObject character)
    {
        Health opponent = character.GetComponent<Health>();

        while (true)
        {
            try
            {
                if (!tilemap.HasTile(tilemap.WorldToCell(transform.position))) break;
                float distance = Vector3.Distance(transform.position, character.transform.position);
                if (distance <= attackRadius)
                    opponent.TakeDamage(damage * Time.deltaTime);
                if (distance <= perceptionRadius)
                    transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
                else
                    break;

                position = transform.position;
            }
            catch (Exception error)
            {
                Debug.Log(error);
                break;
            }

            yield return null;
        }

        fighting = false;
    }

    bool Raycast(out GameObject character)
    {
        Collider2D collider = Physics2D.OverlapCircle(
            (Vector2)transform.position,
            perceptionRadius,
            layer);
        character = collider != null ? collider.gameObject : null;
        return character != null;
    }
}
