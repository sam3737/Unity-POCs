using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    public string optionName;
    public bool subMenu;
    public Vector3 cameraPos;
    public Vector3 cameraRotation;
    public float moveSpeed;
    public float rotationSpeed;
    private bool inSubmenu;

    public AudioClip myAudioClip;
    private AudioSource audioSource;

    private Coroutine moveCoroutine;
    private Coroutine rotationCoroutine;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = myAudioClip;

        inSubmenu = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Detect right click 
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            if (rotationCoroutine != null)
                StopCoroutine(rotationCoroutine);

            moveCoroutine = StartCoroutine(moveCameraPos(MenuManager.defaultCameraPosition));
            rotationCoroutine = StartCoroutine(moveCameraRotBack());
        }
    }

    void OnMouseDown()
    {
        Debug.Log(optionName);
        if (subMenu)
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            if (rotationCoroutine != null)
                StopCoroutine(rotationCoroutine);

            moveCoroutine = StartCoroutine(moveCameraPos(cameraPos));
            rotationCoroutine = StartCoroutine(moveCameraRot(cameraRotation));
            MenuManager.enabled = false;
        }
    }

    void OnMouseEnter()
    {
        audioSource.Play();
        if (!inSubmenu)
            transform.Rotate(new Vector3(0, 10, 0));
    }

    void OnMouseExit()
    {
        if (!inSubmenu)
            transform.Rotate(new Vector3(0, -10, 0));
    }

    // Start a subroutine to smoothly move the camera to the target position
    IEnumerator moveCameraPos(Vector3 target)
    {
        while (Vector3.Distance(Camera.main.transform.position, target) > 0.001f)
        {
            float step = moveSpeed * Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target, step);
            yield return null;
        }

        Camera.main.transform.position = target;
    }

    // Start a subroutine to smoothly move the camera to the target rotation
    IEnumerator moveCameraRot(Vector3 target)
    {
        while (Quaternion.Angle(Camera.main.transform.rotation, Quaternion.Euler(target)) > 0.001f)
        {
            float step = rotationSpeed * Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(target), step);
            yield return null;
        }

        Camera.main.transform.rotation = Quaternion.Euler(target);
    }

    // Start a subroutine to smoothly move the camera to the target rotation
    IEnumerator moveCameraRotBack()
    {
        while (Quaternion.Angle(Camera.main.transform.rotation, Quaternion.Euler(MenuManager.getTargetRotation())) > 0.001f)
        {
            float step = rotationSpeed * Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(MenuManager.getTargetRotation()), step);
            yield return null;
        }

        Camera.main.transform.rotation = Quaternion.Euler(MenuManager.getTargetRotation());
    }
}
