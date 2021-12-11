using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public List<GameObject> solidersList = new List<GameObject>();
    public Transform collectedSolidersHolder;
    public GameObject soliderPrefab;
    public GameObject bulletPrefab;

    [Header("Particle FX")]
    public GameObject soliderInvokePFX;
    public GameObject soliderDeathPFX;
    public GameObject enemyDeathPFX;

    [Space(10)]

    public Text textUI;
    [Space(10)]

    public LevelState levelState;
    public enum LevelState
    {
        NotFinished,
        Finished

    }

    public PlayerState playerState;
    public enum PlayerState
    {
        Stopped,
        Move,
    }

    [Space(10)]
    public List<GameObject> enemyList = new List<GameObject>();

    [Header("Enemy Health Values")]
    public int normalEnemyHealth = 100;
    public int giantEnemyHealth = 250;

    [Header("Enemy Materials")]
    public Material defaultEnemyMat;
    public Material hurtEnemyMat;
    public float enemyBeenHurtedTime = .12f;

    [Header("Enemy Lerp Values")]
    public float lookAtLerpSpeed = 4f;
    public float handUpLerpSpeed = 10f;

    [Header("UI")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject CongratulationsPanel;

    public void PlaySoliderInvokePFX(Vector3 position)
    {
        GameObject obj = Instantiate(soliderInvokePFX, position, Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1.5f);
    }

    public void PlaySoliderDeathPFX(Vector3 position)
    {
        GameObject obj = Instantiate(soliderDeathPFX, position, Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1.5f);
    }

    public void PlayEnemyDeathPFX(Vector3 position)
    {
        GameObject obj = Instantiate(enemyDeathPFX, position, Quaternion.Euler(-90, 0, 0));
        Destroy(obj, 1.5f);
    }

    public void UpdateSoliderCount()
    {
        textUI.text = solidersList.Count.ToString();
    }

    public void StartTheGame()
    {
        playerState = PlayerState.Move;
        menuPanel.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Burada Update method kullanmak yerine disaridan baska bir objeden denetlemek daha dogru.
    private void Update()
    {
        if (enemyList.Count == 0)
        {
            CongratulationsPanel.SetActive(true);
        }
    }
}
