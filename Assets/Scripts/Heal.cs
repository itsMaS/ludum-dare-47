using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float floatDespawnDistance = 6;

    private bool claimed = false;

    float healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!claimed && Damagable.Damagables.ContainsKey(collision) && Damagable.Damagables[collision] is PlayerController)
        {
            claimed = true;
            XPManager.instance.GainXP(Random.Range(Config.pa.XPfromHeal.x, Config.pa.XPfromHeal.y));
            Damagable.Damagables[collision].Heal(healAmount);
            AudioManager.Play("Pickup");
            SpawnNew();
        }
    }

    IEnumerator CheckDistance()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            if(Vector2.Distance(transform.position, LevelManager.instance.active.pivot.position) >= floatDespawnDistance)
            {
                SpawnNew();
            }
        }
    }

    public void SpawnNew()
    {
        Instantiate(gameObject, (Vector2)LevelManager.instance.active.pivot.position +
    Random.insideUnitCircle.normalized * Config.pa.healDistance, Quaternion.identity).transform.name = "Heal";
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        LevelManager.instance.onTransform += Transformation;
        UpdateActive();
    }
    private void OnDisable()
    {
        LevelManager.instance.onTransform -= Transformation;
    }
    private void Transformation(PlayerController next)
    {
        UpdateActive();
    }
    private void UpdateActive()
    {
        bool worm = LevelManager.instance.active is WormController;
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(worm);
        }
    }
    private void Start()
    {
        StartCoroutine(CheckDistance());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, floatDespawnDistance);
    }
}
