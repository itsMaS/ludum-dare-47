using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Config gameConfiguration;

    public bool gameOver = false;

    public static LevelManager instance { get; private set; }

    public float lerpRemainingTime { get { return Mathf.InverseLerp(0, transformationCooldown, remainingTime); } }

    public UnityEvent onTransform;
    public PlayerController active;

    public float transformationCooldown;
    public float remainingTime;

    public float playerMaxHealth;
    public float playerHealth { get { return active.health; } }
    public float playerHealthNormalized { get { return Mathf.InverseLerp(0, playerMaxHealth, playerHealth); } }

    private void Awake()
    {
        instance = this;
        Damagable.Damagables = new Dictionary<Collider2D, Damagable>();
        //Config.pa = Instantiate(gameConfiguration);
        Config.pa = gameConfiguration;
    }
    private void Start()
    {
        CameraManager.instance.target = active.pivot;

        remainingTime = transformationCooldown;
        StartCoroutine(GameLoop());

        active.maxHealth = playerMaxHealth;
        active.health = playerMaxHealth;
    }
    IEnumerator GameLoop()
    {
        while (!gameOver)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = transformationCooldown;
                var next = active.Transform();
                StartCoroutine(Transformation(active, next));
                active = next;
                onTransform.Invoke();
                CameraManager.instance.target = active.pivot;
            }
            yield return null;
        }
    }

    IEnumerator Transformation(PlayerController previous, PlayerController next)
    {
        next.gameObject.SetActive(false);
        next.name = next.name.Replace("(Clone)", "");
        AnimationCurve outro = AnimationCurve.EaseInOut(0, 0, 1, 1);
        previous.dv.Hit(Color.white, outro, 1);
        next.health = previous.health;
        next.maxHealth = previous.maxHealth;
        if (gameOver) yield break;
        yield return new WaitForSeconds(1f);
        AudioManager.Play("Transformation");
        if (gameOver) yield break;
        EffectManager.instance.SpawnParticle("Explosion", previous.pivot.position+new Vector3(0,0.15f,0));
        active.transform.position = previous.pivot.position;
        Destroy(previous.gameObject);
        next.gameObject.SetActive(true);
        AnimationCurve intro = AnimationCurve.EaseInOut(0, 1, 1, 0);
        next.dv.Hit(Color.white, intro, 1);
    }
    public void GameOver()
    {
        gameOver = true;
        StartCoroutine(PerformGameOver());
    }
    IEnumerator PerformGameOver()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
