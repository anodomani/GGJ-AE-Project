using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNode : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tooFarAway;
    public LayerMask player;
    public LayerMask hitLayers;

    public TMPro.TextMeshProUGUI dialogueTitle;
    public TMPro.TextMeshProUGUI dialogueBody;
    public GameObject dialogueFull;

    DialogueData LoadedDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            OnClick();
        }
        tooFarAway.alpha -= 0.02f;
    }

    void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f, hitLayers);
 
        if (hit.collider)
        {
            Collider2D inRange = Physics2D.OverlapCircle(hit.transform.position, 18f, player);
            Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
            if (inRange)
            {
                Debug.Log ("player in range");
                LoadedDialogue = hit.collider.GetComponent<DialogueData>();
                dialogueFull.SetActive(true);
                print(LoadedDialogue.dialogueBody.text);
                dialogueTitle.text = LoadedDialogue.dialogueTitle;
                dialogueBody.text = LoadedDialogue.dialogueBody.text;
            }
            else
            {
                tooFarAway.alpha = 1f;
            }
        }
    }
}
