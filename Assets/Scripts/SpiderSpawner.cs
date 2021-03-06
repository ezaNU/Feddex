﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;


namespace Assets.Scripts
{
  public class SpiderSpawner : MonoBehaviour
  {
    private int _spidersToSpawn;
    private int _spidersSpawned;
    private bool _spawning;
    private float _lastSpawned;

    public GameObject Prefab;

    private readonly string[] TARGETS = new string[] { "Bed", "wardrobe", "Door", "bedside drawers"};
    private readonly Vector3[] SPAWN_POSITIONS = new Vector3[]
    {
      new Vector3(-17.88f, -0.45f, -6.65f), // under bed
      new Vector3(-5.96f, -0.45f, -8.78f), // under barrel
      new Vector3(16.05f, -0.45f, 11.34f), // under door
      new Vector3(4.53f, -0.45f, 7.25f), // under table
    };

    private List<GameObject> _spiders = new List<GameObject> {};

    void Start()
    {
      StartSpawning();
    }

    void StartSpawning()
    {
      _spidersToSpawn = Random.Range(3, 5);
      _spawning = true;
    }

    void Update()
    {
      for (int i = 0; i < _spiders.Count; i++)
      {
        var spider = _spiders[i];

        if (spider == null)
        {
          _spiders.RemoveAt(i);
          _spidersSpawned = _spiders.Count;
        }
      }

      if (_spidersSpawned < _spidersToSpawn)
      {
        if (_lastSpawned + 0.5 < Time.time)
        {
          SpawnSpider();
          _spidersSpawned++;
        }
      }
    }

    void SpawnSpider()
    {
      Vector3 spawnPosition = SPAWN_POSITIONS[Random.Range(0, SPAWN_POSITIONS.Length - 1)];

      GameObject spider = Instantiate(Prefab, spawnPosition, new Quaternion());
      GameObject spiderTarget = GameObject.Find(
        TARGETS[Random.Range(0, TARGETS.Length - 1)]
      );
      spider.GetComponent<MoveToTargetController>().setTarget(spiderTarget);

      _spiders.Add(spider);

      _lastSpawned = Time.time;
    }
  }
}