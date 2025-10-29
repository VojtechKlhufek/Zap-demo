using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject playerGameObject;
    private Player playerScript;
    private Collider2D playerCollider;

    private GameObject ground;
    private Collider2D groundCollider;
    private SpriteRenderer groundSpriteRenderer;
    private Color groundActiveColor = new Color(0.47f, 0.47f, 0.47f, 1f);
    private Color groundInactiveColor = new Color(0.47f, 0.47f, 0.47f, 0.2f);

    private int levelCount;
    System.Random generator = new();

    //Levels are represented as GameObjects which can be toggled for easy level switching
    private List<GameObject> levelsInOrder = new();
    private int currentPlayerLocation;

    [SerializeField] private TextMeshProUGUI winText;
    private int winWaitTime = 3;

    
    void Start()
    {
        winText.color = new Color(1,1,1,0);
        ground = GameObject.Find("Ground");
        groundCollider = ground.GetComponent<Collider2D>();
        groundSpriteRenderer = ground.GetComponent<SpriteRenderer>();
        
        playerGameObject = GameObject.Find("Player");
        playerCollider = playerGameObject.GetComponent<Collider2D>();
        playerScript = playerGameObject.GetComponent<Player>();
        
        currentPlayerLocation = 0;
        
        List<GameObject> levels = GetLevels();
        levelCount = levels.Count;

        levelsInOrder = RandomizeLevelOrder(levels);
        
        foreach(GameObject level in levelsInOrder)
        {
            level.SetActive(false);
        }

        levelsInOrder[currentPlayerLocation].SetActive(true);

    }

    private List<GameObject> GetLevels()
    {
        List<GameObject> levelList = new();
        int i = 1;
        while(true)
        {
            GameObject level = GameObject.Find("Level" + i);
            if (level != null)
            {
                levelList.Add(level);
                i++;
            }
            else break;
        }
        return levelList;
    }

    List<GameObject> RandomizeLevelOrder(List<GameObject> levels)
    {
        List<GameObject> orderedLevels = new();
        for (var j = levelCount; j>0; j--)
        {
            int nextLevelId = generator.Next(0, j);
            GameObject newLevel = levels[nextLevelId];
            
            levels.Remove(newLevel);
            orderedLevels.Add(newLevel);
        }
        return orderedLevels;
    }

    void Update()
    {
        if (playerScript.HasJumped() && currentPlayerLocation != 0)
        {
            ToggleGround(false);
        }
        CheckHeight();
    }

    void ToggleGround(bool activate)
    {
        //Phisics2D.IgnoreCollision is used instead of a toggle because I don't want enemies to fall through
        if (activate)
        {
            Physics2D.IgnoreCollision(playerCollider, groundCollider, false);
            groundSpriteRenderer.color = groundActiveColor;
        }
        else
        {
            Physics2D.IgnoreCollision(playerCollider, groundCollider);
            groundSpriteRenderer.color = groundInactiveColor;
        }
    }

    //Checks if the player is low or high enough for a level change
    void CheckHeight()
    {
        if (playerGameObject.transform.position.y > 5 )
        {
            ChangeLevel(1);
            playerGameObject.transform.position = new Vector3(playerGameObject.transform.position.x, -4);
        } 
        if (playerGameObject.transform.position.y < -6 && currentPlayerLocation > 0)
        {
            ChangeLevel(-1);
            playerGameObject.transform.position = new Vector3(playerGameObject.transform.position.x, 4.5f);
        }
    }

    void ChangeLevel(int levelDelta)
    {
        levelsInOrder[currentPlayerLocation].SetActive(false);
        
        //if all levels are passed, the player wins
        if (currentPlayerLocation + levelDelta >= levelCount)
        {
            Win();
        }
        else
        {
            currentPlayerLocation += levelDelta;
            levelsInOrder[currentPlayerLocation].SetActive(true);
            if (levelDelta > 0 || currentPlayerLocation == 0)
            {
                ToggleGround(true);
            }
        }
        
        DestroyBullets();
        DestroyItems();
    }

    private void DestroyItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in items)
        {
            Destroy(item);
        } 
    }
    private void DestroyBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Win()
    {
        winText.color = new Color(1,1,1,1);
        StartCoroutine(WaitAndLoadMenu(winWaitTime));
    }
    
    IEnumerator WaitAndLoadMenu(int duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("Menu");
    }
}
