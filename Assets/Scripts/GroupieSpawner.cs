using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupieSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private AnimationCurve _spawnRate = AnimationCurve.Linear(0.0f, 1.0f, 30.0f, 0.5f);
    [SerializeField] private Vector2 _minMaxSpawnPoint = new Vector2(-5.0f, 5.0f);
    [SerializeField] private float ySpawn = -2.0f;
    private float _time = 0.0f;
    [SerializeField] private GroupieController _prefab = null;

    public bool IsSpawning = false;

    public float Timer { get => _time; }
    #endregion Fields

    #region Methods
    public void StartSpawn()
    {
        IsSpawning = true;
        StartCoroutine(Spawn());
    }

    void Update()
    {
        _time += Time.deltaTime;
    }
    #endregion Methods

    private IEnumerator Spawn()
    {
        while (IsSpawning)
        {
            yield return new WaitForSeconds(_spawnRate.Evaluate(_time));
            bool right = Random.value > 0.5f;
            GroupieController groupie = Instantiate(_prefab, new Vector2(right ? _minMaxSpawnPoint.y  : _minMaxSpawnPoint.x, ySpawn), Quaternion.identity);
            groupie.SetRight(right);
        }
    }
}
