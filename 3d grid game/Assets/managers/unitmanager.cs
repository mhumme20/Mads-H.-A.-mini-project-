using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class unitmanager : MonoBehaviour
{
    public static unitmanager instance;
    public grid_script gridScript;


    private List<scriptableunit> _units;

    public Basehero selected_hero;

    void Awake()
    {
        instance = this;

        _units = Resources.LoadAll<scriptableunit>("units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;
        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomunit<Basehero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = gridScript.GetRandomHerospawnTile();
            randomSpawnTile.GetComponent<gridcell>().setUnit(spawnedHero);
            spawnedHero.transform.position = randomSpawnTile.transform.position;
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }
    
    public void SpawnVillians()
    {
        var VillianCount = 5;
        for (int i = 0; i < VillianCount; i++)
        {
            var randomPrefab = GetRandomunit<Baseenemy>(Faction.Enemy);
            var spawnedenemy = Instantiate(randomPrefab);
            var randomSpawnTile = gridScript.GetRandomEnemypawnTile();
            randomSpawnTile.GetComponent<gridcell>().setUnit(spawnedenemy);
            spawnedenemy.transform.position = randomSpawnTile.transform.position;
        }
        GameManager.Instance.ChangeState(GameState.Herotunrn);
    }

    private T GetRandomunit<T>(Faction faction) where T : baseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().unitPrefab;
    }

    public void set_selected_hero(Basehero hero)
    {
        selected_hero = hero;
    }

}
