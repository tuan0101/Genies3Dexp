using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] Button playBtn;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnPlayButton);
    }

    void OnPlayButton()
    {
        SceneManager.LoadScene("main");
    }

}
