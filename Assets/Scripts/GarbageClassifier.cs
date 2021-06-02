using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageClassifier : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cursorImage;
    [SerializeField] Vector3 targetPosition = new Vector3(2.405752f, 2.203f, -2.352f);
    [SerializeField] Vector3 targetAngle = new Vector3(30.359f, 0, 0);
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] Sprite openedHandSprite;
    [SerializeField] Sprite closedHandSprite;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failParticles;
    [SerializeField] AudioSource successSound;
    [SerializeField] AudioSource failSound;
    [SerializeField] Text scoreText;
    [SerializeField] Text timerText;
    [SerializeField] GameObject[] garbageOptions;
    private Vector3 currentAngle;
    private Vector3 currentPosition;
    private int score = 0;
    const int MAX_SCORE = 5000;
    const int SUCCESS_SCORE = 100;
    const int HALF_SUCCESS_SCORE = 50;


    // Start is called before the first frame update
    void Start()
    {
        currentAngle = mainCamera.transform.localEulerAngles;
        currentPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateCamera();
        UpdateCursorPosition();
    }

    void AnimateCamera()
    {
        currentAngle = LerpVector(currentAngle, targetAngle, rotationSpeed);
        mainCamera.transform.localEulerAngles = currentAngle;
        currentPosition = LerpVector(currentPosition, targetPosition, movementSpeed);
        mainCamera.transform.position = currentPosition;
    }

    Vector3 LerpVector(Vector3 originalVector, Vector3 targetVector, float speed)
    {
        originalVector = new Vector3(
            Mathf.LerpAngle(originalVector.x, targetVector.x, Time.deltaTime * speed),
            Mathf.LerpAngle(originalVector.y, targetVector.y, Time.deltaTime * speed),
            Mathf.LerpAngle(originalVector.z, targetVector.z, Time.deltaTime * speed)
        );

        return originalVector;
    }

    void UpdateCursorPosition()
    {
        cursorImage.transform.position = Input.mousePosition;
        if (Input.GetMouseButton(0)) {
            cursorImage.GetComponent<Image>().sprite = closedHandSprite;
        } else {
            cursorImage.GetComponent<Image>().sprite = openedHandSprite;
        }
    }

    public void CountSuccess(GameObject bin)
    {
        PlayParticles(bin, successParticles);
        PlaySound(successSound);
        score += SUCCESS_SCORE;
        UpdateScore();
        LoadGarbage();
    }

    public void CountPartialSuccess(GameObject bin)
    {
        PlayParticles(bin, successParticles);
        PlaySound(successSound);
        score += HALF_SUCCESS_SCORE;
        UpdateScore();
        LoadGarbage();
    }

    public void CountFail(GameObject bin)
    {
        PlayParticles(bin, failParticles);
        PlaySound(failSound);
        LoadGarbage();
    }

    private void PlayParticles(GameObject bin, ParticleSystem particles)
    {
        if(particles) {
            particles.transform.position = bin.transform.position;
            particles.transform.Translate(new Vector3(0, 0, 1f));
            particles.Play();
        }
    }

    private void PlaySound(AudioSource sound)
    {
        if(sound) {
            sound.PlayOneShot(sound.clip);
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void LoadGarbage()
    {
        int randomIndex = Random.Range(0, garbageOptions.Length);
        Instantiate(garbageOptions[randomIndex], new Vector3(2.42f, 1.09f, -1.29f), Quaternion.identity);
    }
}
