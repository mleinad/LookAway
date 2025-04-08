using UnityEngine;
using UnityEngine.UI;

public class GazeItemCollector : MonoBehaviour
{
    [Header("Collect Settings")]
    public float collectRange = 5f;
    public KeyCode collectKey = KeyCode.E;

    [Header("Game Objects")]
    public GameObject paper1;
    public GameObject paper2;
    public GameObject keyObject;

    [Header("UI")]
    public Text instructionText;
    public Text tooltipText;

    private int collectedCount = 0;

    void Start()
    {
        paper1.SetActive(true);
        paper2.SetActive(false);
        keyObject.SetActive(false);

        UpdateInstruction("Search for the first note");
        UpdateTooltip(""); // Hide tooltip initially
    }

    void Update()
    {
        CheckGazeHover();

        if (Input.GetKeyDown(collectKey))
        {
            TryCollect();
        }
    }

    void CheckGazeHover()
    {
        GameObject target = GazeBehaviour.CurrentGazeTarget;

        if (target == null)
        {
            UpdateTooltip("");
            return;
        }

        float distance = Vector3.Distance(Camera.main.transform.position, target.transform.position);
        if (distance > collectRange)
        {
            UpdateTooltip("");
            return;
        }

        if ((paper1.activeInHierarchy && target == paper1) ||
            (paper2.activeInHierarchy && target == paper2) ||
            (keyObject.activeInHierarchy && target == keyObject))
        {
            UpdateTooltip("Press E to pick up");
        }
        else
        {
            UpdateTooltip("");
        }
    }

    void TryCollect()
    {
        GameObject target = GazeBehaviour.CurrentGazeTarget;
        if (target == null) return;

        float distance = Vector3.Distance(Camera.main.transform.position, target.transform.position);
        if (distance > collectRange) return;

        Debug.Log($"Gaze hit (resolved): {target.name}");

        if (paper1.activeInHierarchy && target.transform.IsChildOf(paper1.transform))
        {
            paper1.SetActive(false);
            paper2.SetActive(true);
            collectedCount++;
            UpdateInstruction("Search for the second note");
            UpdateTooltip("");
            return;
        }

        if (paper2.activeInHierarchy && target.transform.IsChildOf(paper2.transform))
        {
            paper2.SetActive(false);;
            keyObject.SetActive(true);
            collectedCount++;
            UpdateInstruction("Search for the key");
            UpdateTooltip("");
            return;
        }

        if (keyObject.activeInHierarchy && target.transform.IsChildOf(keyObject.transform))
        {
            keyObject.SetActive(false);
            collectedCount++;
            UpdateInstruction("You got the key. You can now leave!");
            UpdateTooltip("");
        }
    }





    void UpdateInstruction(string message)
    {
        if (instructionText != null)
            instructionText.text = message;
    }

    void UpdateTooltip(string message)
    {
        if (tooltipText != null)
            tooltipText.text = message;
    }
}
