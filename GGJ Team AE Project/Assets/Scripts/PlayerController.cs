using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform targetPosition;
    bool moving = false;

    // Awake is called at the start of the program
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementPointClick(startPosition.position, targetPosition.position);
    }

    void MovementPointClick(Vector3 a_startPosition, Vector3 a_targetPosition)
    {
        print("shmoving");
        Node startNode = grid.NodeFromWorldPosition(a_startPosition);
        Node targetNode = grid.NodeFromWorldPosition(a_targetPosition);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(startNode);

        while (OpenList.Count > 0){
            Node currentNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++){
                if (OpenList[i].FCost < currentNode.FCost || OpenList[i].FCost == currentNode.FCost && OpenList[i].hCost < currentNode.hCost){
                    currentNode = OpenList[i];
                }
            }
            OpenList.Remove(currentNode);
            ClosedList.Add(currentNode);

            if (currentNode == targetNode){
                GetFinalPath(startNode, targetNode);
            }

            foreach (Node NeighborNode in grid.GetNeighborNodes(currentNode)){
                if (!NeighborNode.isWall || ClosedList.Contains(NeighborNode)){
                    continue;
                }
                int MoveCost = currentNode.gCost + GetManhattanDistance(currentNode, NeighborNode);

                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode)){
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattanDistance(NeighborNode, targetNode);
                    NeighborNode.parent = currentNode;

                    if (!OpenList.Contains(NeighborNode)){
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }

        /*
        var click = Input.GetButtonDown("TargetPoint");
        if(click == true){
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if(moving && (Vector2)transform.position != targetPosition){
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        } else {
            moving = false;
        }
        */
    }

    void GetFinalPath(Node a_startNode, Node a_endNode){
        List<Node> finalPath = new List<Node>();
        Node currentNode = a_endNode;

        while (currentNode != a_startNode){
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
        print("final path");
        grid.FinalPath = finalPath;
    }

    int GetManhattanDistance(Node a_nodeA, Node a_nodeB){
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
