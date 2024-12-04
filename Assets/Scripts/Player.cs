using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    public float rotationSpeed = 100f;
    public LineRenderer lineRenderer;
    public int maxReflectionCount = 5;
    public float maxRayDistance = 100f;
    public string reflectionTag = "Reflective";
    public string target = "Target";

    private Quaternion targetRotation;
    public float smoothTime = 0.1f; // Чем выше значение, тем медленнее будет поворот

    private bool rotateUp, rotateDown, rotateLeft, rotateRight;

    void Start()
    {
        targetRotation = transform.rotation;

        // Добавляем события нажатия и отпускания для кнопок
        AddButtonListeners(upButton, () => rotateUp = true, () => rotateUp = false);
        AddButtonListeners(downButton, () => rotateDown = true, () => rotateDown = false);
        AddButtonListeners(leftButton, () => rotateLeft = true, () => rotateLeft = false);
        AddButtonListeners(rightButton, () => rotateRight = true, () => rotateRight = false);
    }

    private void AddButtonListeners(Button button, System.Action onPress, System.Action onRelease)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entryDown.callback.AddListener((eventData) => onPress());
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        entryUp.callback.AddListener((eventData) => onRelease());
        trigger.triggers.Add(entryUp);
    }

    void Update()
    {
        HandleCameraRotation();
        DrawRay();
    }

    private void HandleCameraRotation()
    {
        if (rotateUp)
            targetRotation *= Quaternion.Euler(-rotationSpeed * Time.deltaTime, 0, 0);
        if (rotateDown)
            targetRotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime, 0, 0);
        if (rotateLeft)
            targetRotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);
        if (rotateRight)
            targetRotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);

        // Ограничение углов по оси X (поворот вверх-вниз)
        Vector3 eulerAngles = targetRotation.eulerAngles;
        targetRotation = Quaternion.Euler(eulerAngles);

        // Плавное вращение камеры
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);
    }

    private void DrawRay()
    {
        Vector3 direction = transform.forward;
        Vector3 origin = transform.position - new Vector3(0, 0.5f, 0); // Смещение вниз на 0.5 единицы
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, origin);

        for (int i = 0; i < maxReflectionCount; i++)
        {
            Ray ray = new Ray(origin, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                if (hit.collider.CompareTag(target))
                {
                    PanelManager.InstancePanel.panelWin.SetActive(true);
                    DataManager.InstanceData.mapNextLevel.OpenLevel();
                }

                if (hit.collider.CompareTag(reflectionTag))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    origin = hit.point;
                }
                else
                {
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, origin + direction * maxRayDistance);
                break;
            }
        }
    }
}
