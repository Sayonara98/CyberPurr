using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverCanvas;
    public GameObject AirPort;
    public GameObject Player;
    
    // Start is called before the first frame update
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        AirPort.SetActive(false);
    }

    public void Replay()
    {
        GameOverCanvas.SetActive(false);
        AirPort.SetActive(true);
        AirPort.GetComponent<Airport>().Replay();
        List<GameObject> listObj = new List<GameObject>(GameObject.FindGameObjectsWithTag("Soldier"));
        foreach (GameObject obj in listObj)
            obj.SetActive(false);
        Player.GetComponent<Turret>().Replay();
        Score.score = 0;
    }
}
