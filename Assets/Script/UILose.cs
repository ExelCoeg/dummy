using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILose : UI
{
    public Button restart;
    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

}
