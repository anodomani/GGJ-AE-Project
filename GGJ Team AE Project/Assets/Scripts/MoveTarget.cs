using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public LayerMask hitLayers;
    public GameObject pointer;
    SpriteRenderer pointerSprite;

    void Awake()
    {
        pointerSprite = pointer.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Collider2D isWall = Physics2D.OverlapCircle(worldPosition, 1f, hitLayers);
            RaycastHit2D isWall = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f, hitLayers);
            if (!isWall)
            {
                //print("new target at: " + worldPosition);
                pointer.transform.parent.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                pointerSprite.color = new Color(pointerSprite.color.r, pointerSprite.color.g, pointerSprite.color.b, 1);
            }
        }
        pointerSprite.color = new Color(pointerSprite.color.r, pointerSprite.color.g, pointerSprite.color.b, pointerSprite.color.a - (0.05f));
    }
}