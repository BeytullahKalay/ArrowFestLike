using UnityEngine;
using UnityEngine.UI;

public class PlayButtonFunction : MonoBehaviour
{

    [SerializeField] private GameMaster gm;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        button.onClick.AddListener(ButtonFunction);    
    }

    private void ButtonFunction()
    {
        gm.StartTheGame();
    }
}
