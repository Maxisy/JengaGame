using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text TextComponent;
    int SpecialBlockBeginningNumber;
    public bool IsPlaying;
    public string NextLevelName;

    void Start()
    {
        FixLightning();

        TextComponent.enabled = false;
        SpecialBlockBeginningNumber = CountBlocks(true);
        IsPlaying = true;
    }

    public void OnValidate()
    {
        FixLightning();
    }

    void FixLightning()
    {
        RenderSettings.ambientLight = Color.white;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
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
            StartCoroutine(EndGameCoroutine(false));

        else if (CountBlocks(special: false) == 0)
            StartCoroutine(EndGameCoroutine(true));

        Destroy(block.gameObject);
    }

    IEnumerator EndGameCoroutine(bool won)
    {
        if (IsPlaying == false) yield break;
        
        TextComponent.enabled = true;
        IsPlaying = false;

        if (won)
        {
            for (int i = 5; i > 0; i--)
            {
                TextComponent.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            if (SpecialBlockBeginningNumber != CountBlocks(true))
                won = false; 
        }

        TextComponent.text = won ? "Wygrana!" : "Przegrana";

        yield return new WaitForSeconds(3);

        if (string.IsNullOrEmpty(NextLevelName))
        {
            Debug.Log("koniec");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(NextLevelName);
        }
    }
}
