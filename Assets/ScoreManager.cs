using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _instance;

    public Player player;

    private void Start()
    {
        _instance = this;
    }


}
