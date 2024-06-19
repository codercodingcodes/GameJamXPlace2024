using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(SceneTransition);
        
    }
    void SceneTransition(){
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
