using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private int CountBlocks(bool special)
    {
        return FindObjectsOfType<Block>()
            .Count(block => block.Enabled && block.IsSpecial == special);

    }

    private void OnTriggerExit(Collider other)
    {
        var block = other.GetComponent<Block>();

        if (block == null) return;

        block.Enabled = false;

        if (block.IsSpecial)
        {
            Debug.Log("you lost");
        }
        else if (CountBlocks(special: false) == 0)
        {
            Debug.Log("you won");
        }
        else
        {
            Debug.Log("play");
        }

        Debug.Log("pozostalo: " + CountBlocks(false));
        Destroy(block.gameObject);
    }
}
