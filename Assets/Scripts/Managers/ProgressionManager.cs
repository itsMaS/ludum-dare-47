using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    List<Damagable> Enemies = new List<Damagable>();

    public GameObject Enemy;

    public float spawnInterval;
    public float spawnRange;
    public float despawnRange;

    public AnimationCurve amountFromLevel;

    public static ProgressionManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(CleanUp());
    }
    IEnumerator SpawnEnemies()
    {
        while(!LevelManager.instance.gameOver)
        {
            yield return new WaitForSeconds(spawnInterval);
            if(Enemies.Count < amountFromLevel.Evaluate(XPManager.instance.level))
            {
                var tmp = Instantiate(Enemy, (Vector2)LevelManager.instance.active.pivot.position + Random.insideUnitCircle.normalized*spawnRange,
                    Quaternion.identity).GetComponent<EnemyController>();
                Enemies.Add(tmp);
            }
        }
    }
    IEnumerator CleanUp()
    {
        while(!LevelManager.instance.gameOver)
        {
            yield return new WaitForSecondsRealtime(15);
            Enemies.RemoveAll(item => item == null);
            List<Damagable> Delete = new List<Damagable>();
            foreach (var item in Enemies)
            {
                if(Vector2.Distance(item.pivot.position, LevelManager.instance.active.pivot.position) > despawnRange)
                {
                    Delete.Add(item);
                }
            }
            Delete.ForEach(item => Destroy(item.gameObject));
            Enemies.RemoveAll(item => Delete.Contains(item));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector2.zero, spawnRange);
    }
}
