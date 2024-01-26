using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 적을 잡을때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // - 현재 점수를 표시할 때
    // - 현재 점수를 기억할 변수
    public Text ScoreTextUI;
    public Text BestScoreTextUI;

    private int _score = 0;
    public int BestScore = 0;

    // 목표 : score속성에 대한 capsulization
    public int GetScore()
    {
        return _score;
    }
    public void SetScore(int score)
    {
        if (score < 0)
        {
            return;
        }

        _score = score;
        if (_score > BestScore)
        {
            BestScore = _score;
            // 목표 : 최고 점수를 저장
            // 'Playerprefs' 클래스를 사용
            // -> 값을 '키(key)'와 값(Value) 형태로 저장하는 클래스.
            // 저장할 수 있는 데이터타입은 int, float, string
            // 타입별로 저장/로드가 가능한 Set/Get 메서드가 있다.
            PlayerPrefs.SetInt("BestScore", BestScore);
        }

        ScoreTextUI.text = $"점수: {_score}";
        BestScoreTextUI.text = $"최고점수: {BestScore}";
    }

    public void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
    }

    // 구현 순서
    // 1. 만약에 적을 잡으면?
    // 2. 스코어를 증가 시킨다.
    // 2-1. 씬에서 ScoreManager 게임오브젝트를 찾아온다.


    // 2-2. ScoreManager 게임오브젝트에서 ScoreManager 스크립트 컴포넌트를 얻어온다.
    // 2-3. 컴포넌트의 Score 속성을 증가시킨다.

    // 3. 스코어를 화면에 표시한다.

}
