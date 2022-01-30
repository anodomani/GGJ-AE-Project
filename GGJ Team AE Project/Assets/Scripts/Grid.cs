//ref https://www.youtube.com/watch?v=AKKpPmxx07w
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    public float distance;

    Node[,] grid;
    public List<Node> FinalPath;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    // Start is called before the first frame update
    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid(){
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.down * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                Vector3 worldPoint = bottomLeft + Vector3.right * (x*nodeDiameter+nodeRadius) + Vector3.down * (y*nodeDiameter+nodeRadius);
                bool wall = true;

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, wallMask)){
                    wall = false;
                }

                grid[x,y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

        if (grid != null){
            foreach (Node node in grid){
                if (node.isWall){
                    Gizmos.color = Color.cyan;
                }
                else {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null){
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance));
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_worldPosition){
        float xpoint = ((a_worldPosition.x + gridWorldSize.x/2)/gridWorldSize.x);
        float ypoint = ((a_worldPosition.y + gridWorldSize.y/2)/gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }
    public List<Node> GetNeighborNodes(Node a_Node){
        List<Node> NeighboringNodes = new List<Node>();
        //rightSide
        int xCheck;
        int yCheck;
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX){
            if (yCheck >= 0 && yCheck < gridSizeY){
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        
        //rightSide
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX){
            if (yCheck >= 0 && yCheck < gridSizeY){
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //leftSide
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX){
            if (yCheck >= 0 && yCheck < gridSizeY){
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //topSide
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX){
            if (yCheck >= 0 && yCheck < gridSizeY){
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //bottomSide
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX){
            if (yCheck >= 0 && yCheck < gridSizeY){
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighboringNodes;
    }
    
}
