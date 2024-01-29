using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 디자인패턴 : 예전부터 소프트웨어 개발 과정에서 발견된 설계 노하우에 이름을 붙여
 * 재사용하기 좋은 형태로 묶어 정리한 것
 * 장점 : 서로 같은 패턴을 알고 있을 때 의사소통이 잘됨. ( 내용, 설계 원칙, 특성, 조건 등)
 * 모범 사례이므로 가독성, 유지보수, 확장성, 신뢰성 UP
 * 
 * 단점 : 오용과 남용(서보고 싶은 마음에 억지로 남용)
 * 학습 초기에 학습곡선이 있다. ( 처음 적용에 시간이 든다.)
 * 
 * 알면 좋은 패턴 : 싱글톤, 오브젝트풀, 전략, 옵저거, 팩토리
 * 
 * */
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

    public static ScoreManager Instance;

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
    public void AddScore()
    {
        SetScore(_score + 1);
    }

    private void Awake()
    {
        Debug.Log("ScoreManager 객체의 Awake호출");

        // 싱글톤 패턴: 오직 한 개의 클래스 인스턴스를 갖도록 보장하고
        //              전역적인 접근점을 제공한다. (아무데서나 쉽게)
        // 사용 이유:
        // 게임 개발에서 매니저, 관리자에서 싱글톤을 활용하는건 관행.
        // - 전역 접근, 코드 단순화 (관리자를 찾기 위한 복잡한 로직이 필요 없다.
        // - 중복 생성 방지 (메모리 및 리소스 관리능력 UP)


        if (Instance == null)
        {
            Debug.Log("새로 생성된 것이다!");
            Instance = this;
        }
        else
        {
            Debug.Log("scoremanager 이미 있음!");
            Destroy(gameObject);
        }

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
