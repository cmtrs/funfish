using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    private bool isGameOver;

    [SerializeField] private List<GameObject> monsterPrefabs; 
    [SerializeField] private Transform spawnPoint; // The point where the monster will spawn

    private int _monsterIndex = 0;

    private void Start()
    {

        //MyGlobal.playMusic();

        isGameOver = false;
        _monsterIndex = 0;

        EventManager.OnMonsterDestroyed += SpawnMonster;
        EventManager.OnGameOver += GameOver;

        SpawnMonster();

    }


    public void SpawnMonster()
    {
        if (!isGameOver)
        {
            // Choose a random monster prefab from the list
            //  int randomIndex = Random.Range(0, monsterPrefabs.Count);
            int randomIndex = _monsterIndex;
            GameObject selectedMonsterPrefab = monsterPrefabs[randomIndex];
            // Instantiate(selectedMonsterPrefab, spawnPoint.position, Quaternion.identity);
            var monster = Instantiate(selectedMonsterPrefab, spawnPoint.position, Quaternion.identity);
            monster.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            monster.tag = "Monster";

            _monsterIndex++;
            if (_monsterIndex >= monsterPrefabs.Count)
            {
                _monsterIndex = 0;
            }

            // monster.transform.SetParent(Canvas.)
            // monster.transform.SetParent(GameObject.FindGameObjectWithTag("Monster").transform, false);
            // monster.SetActive(true);

            //RectTransform rt = monster.GetComponent<RectTransform>();
            //rt.localPosition = Vector3.zero; // or another position within the canvas bounds
            //rt.localScale = Vector3.one; // 
        }
        
    }

    public void GameOver()
    {
        isGameOver = true;
       // Time.timeScale = 0.0f;
        MyGlobal.gotoFailedScreen();
    }

    public void throwBall()
    {
        GameObject otherObject = GameObject.Find("Player");
        if (otherObject != null)
        {
            var obj = otherObject.GetComponent<PlayerController>();
            if (obj != null)
            {
                obj.throwBubble();
            }
        }
    }


    private void OnDestroy()
    {
        EventManager.OnMonsterDestroyed -= SpawnMonster;
        EventManager.OnMonsterDestroyed -= SpawnMonster;
    }
}