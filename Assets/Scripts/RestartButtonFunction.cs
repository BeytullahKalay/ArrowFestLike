using UnityEngine;
using UnityEngine.UI;

public class RestartButtonFunction : MonoBehaviour
{

    [SerializeField] private GameMaster gm;
    private Button button;


    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.onClick.AddListener(ButtonFunction);
    }

    private void ButtonFunction()
    {
        gm.RestartScene();
    }
}
