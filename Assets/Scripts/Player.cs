using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileCharacterController : MonoBehaviour
{
    private Rigidbody rb; // 캐릭터의 Rigidbody 컴포넌트
    private MapMovement mapMovement; //MapMovement 스크립트의 참조를 저장할 변수
    private ObstacleSpawner obstacleSpawner; //ObstacleSpawner 스크립트의 참조를 저장할 변수
    public AudioSource backgroundMusic; // 배경 음악 오디오 소스



    public float moveSpeed = 200f; // 캐릭터 이동 속도
    public float jumpForce = 5f; // 점프 힘

    private bool grounded = true; // 바닥에 닿아 있는지 여부
    private bool jumping = false; // 점프 중인지 여부

    public int maxHealth = 3; // 최대 체력
    private int currentHealth; // 현재 체력

    public SpawnManager spawnManager; // 스폰 관리자
    public HeartUIManager healthUIManager; // 체력 UI 관리자

    private Animator animator; // 캐릭터 애니메이터

    private float screenWidth; // 화면 가로 길이
    private float leftBoundary; // 왼쪽 이동 가능한 경계
    private float rightBoundary; // 오른쪽 이동 가능한 경계

    public TimerDisplay timerDisplay;
    public GameObject menuPanel; // 메뉴 창 (패널 등)을 가리키는 변수
    public AudioSource audioSource; // 플레이어 오디오 소스 참조

    private float playerX = 0f; // 입력에 따라 x좌표의 위치를 결정할 변수
    Vector3 movement = new Vector3(0, 3, 12); // 플레이어의 현재 위치

    int touchCountChk = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthUIManager.Initialize(maxHealth); // 체력 UI 초기화
        animator = GetComponent<Animator>();

        screenWidth = Screen.width;
        leftBoundary = screenWidth / 3f;
        rightBoundary = 2f * screenWidth / 3f;

        mapMovement = FindObjectOfType<MapMovement>();
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();

        audioSource = GetComponent<AudioSource>();

        backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        HandleTouchInput(); // 터치 입력 처리
    }

    private void HandleTouchInput()
    { 
        // 전에 입력을 하였는지 확인
        if (Input.touchCount > 0 && touchCountChk < 1)
        {
            // 화면 터치 감지
            Touch touch = Input.GetTouch(0);
            
            // 터치 위치 식별을 위한 지역 변수
            Vector2 touchPosition = touch.position;

            // 왼쪽을 터치하면
            if (touchPosition.x < leftBoundary && transform.position.x > -2f)
            {
                // 왼쪽으로 이동
                playerX -= 2f;
                transform.position = new Vector3(playerX, 3, 12);
                // MoveLeft(); // 왼쪽 이동
            }
            // 오른쪽을 터치하면
            else if (touchPosition.x > rightBoundary && transform.position.x < 2)
            {
               // 오른쪽으로 이동
               playerX += 2f;
               transform.position = new Vector3(playerX, 3, 12);
               // MoveRight(); // 오른쪽 이동
            }
            // 가운데 터치하면
            else if (touchPosition.x >= leftBoundary && touchPosition.x <= rightBoundary)
            {
               // 점프
               Jump(); 
            }   
        }

        // 전에 입력했는 지 확인
        touchCountChk = Input.touchCount;
    }

    private void Jump()
    {
        if (grounded && !jumping)
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
            jumping = true;
            animator.SetTrigger("Jump"); // 점프 애니메이션 활성화
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true; // 바닥에 닿음
            jumping = false; // 점프 상태 종료
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false; // 바닥에서 벗어남
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageObject"))
        {
            int damageAmount = other.GetComponent<DamageObject>().damageAmount;
            TakeDamage(damageAmount); // 데미지 처리
        }
        else// if(other.CompareTag("Player") && other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnerTriggerEntered(); // 스폰매니저에 트리거 진입 알림
        }
    }

    private void TakeDamage(int amount)
    {
        
        currentHealth -= amount;
        Debug.Log("플레이어가 " + amount + "만큼의 데미지를 입었습니다. 현재 체력: " + currentHealth);
        healthUIManager.TakeDamage(amount); // 체력 UI 갱신
        animator.SetTrigger("Hit"); // 피격 애니메이션 재생
        audioSource.Play();
        audioSource.time = 0.6f;

        if (currentHealth <= 0)
        {
            foreach (Image heartImage in healthUIManager.hearts)
            {
                Destroy(heartImage.gameObject); // 모든 하트 제거
            }

            // 피격 효과음 및 파괴를 위한 코루틴 시작
            StartCoroutine(EndTakingDamage());
        }
    }
    private IEnumerator EndTakingDamage()
    {
        // 피격 효과음 재생 (예: 1초 동안)
        audioSource.Play();
        audioSource.time = 0.6f;

        // 피격 효과음이 끝날 때까지 대기
        yield return new WaitForSeconds(0.5f); // 피격 효과음의 길이에 맞게 조절

        // 피격 효과음 중지
        audioSource.Stop();

        // 플레이어 오브젝트 파괴
        EndGame();
    }

    private void EndGame()
    {
        Debug.Log("게임 오버!");
        Debug.Log("게임 끝났다 그만 할까?");
        mapMovement.enabled = false;
        Destroy(obstacleSpawner);
        Destroy(gameObject); // 플레이어 오브젝트를 파괴
        timerDisplay.PauseStopwatch(); // 스톱워치 일시 정지
        menuPanel.SetActive(true);
        backgroundMusic.Stop();
    }
}
