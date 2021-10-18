using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{

    public static ProgressManager instance;

    public GameObject player;
    public GameObject camera;
    public float playerMoveDuration = 1.5f;

    private GameObject mayor;
    private GameObject orangeMayor;
    private GameObject redMayor;
    private GameObject yellowMayor;
    private GameObject blueMayor;
    private GameObject allMayor;

    public Transform orangeStartPlayerPosition;
    public Transform redStartPlayerPosition;
    public Transform yellowStartPlayerPosition;
    public Transform blueStartPlayerPosition;
    public Transform orangeEndPlayerPosition;
    public Transform redEndPlayerPosition;
    public Transform yellowEndPlayerPosition;
    public Transform blueEndPlayerPosition;

    public Transform orangeKeyIn;
    public Transform redKeyIn;
    public Transform yellowKeyIn;
    public Transform blueKeyIn;

    private GameObject redDoor;
    private GameObject yellowDoor;
    private GameObject blueDoor;
    private GameObject orangeKey;
    private GameObject redKey;
    private GameObject yellowKey;
    private GameObject blueKey;

    private bool orangeComplete = false;
    private bool redComplete = false;
    private bool yellowComplete = false;
    private bool blueComplete = false;
    private bool allComplete = false;
    private bool firstTime = true;
    private PaintbrushColor mostRecentColor;

    Subscription<PickupPaintbrush> pickupPaintbrushSubscription;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // Initialization
            pickupPaintbrushSubscription = EventBus.Subscribe<PickupPaintbrush>(_OnPaintbrush);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "HubWorld") {
            orangeKeyIn = GameObject.Find("OrangeKey (1)").transform;
            redKeyIn = GameObject.Find("RedKey (1)").transform;
            yellowKeyIn = GameObject.Find("YellowKey (1)").transform;
            blueKeyIn = GameObject.Find("BlueKey (1)").transform;
            
            player = GameObject.Find("Player");
            camera = GameObject.Find("CameraContainer");
            redDoor = GameObject.Find("RedWinCondition");
            yellowDoor = GameObject.Find("YellowWinCondition");
            blueDoor = GameObject.Find("BlueWinCondition");
            orangeKey = GameObject.Find("OrangeKey");
            redKey = GameObject.Find("RedKey");
            yellowKey = GameObject.Find("YellowKey");
            blueKey = GameObject.Find("BlueKey");
            orangeMayor = GameObject.Find("orangeNPC");
            redMayor = GameObject.Find("redNPC");
            yellowMayor = GameObject.Find("yellowNPC");
            blueMayor = GameObject.Find("blueNPC");
            allMayor = GameObject.Find("allNPC");
            mayor = GameObject.Find("firstNPC");

            if(!firstTime) {
                orangeKey.SetActive(false);
                redKey.SetActive(false);
                yellowKey.SetActive(false);
                blueKey.SetActive(false);
                orangeMayor.SetActive(false);
                redMayor.SetActive(false);
                yellowMayor.SetActive(false);
                blueMayor.SetActive(false);
                mayor.SetActive(false);
                // set doors inactive if complete
                if(orangeComplete) {
                    orangeKey.SetActive(true);
                    if(mostRecentColor != PaintbrushColor.Orange) {
                        orangeKey.transform.position = orangeKeyIn.position;
                        orangeMayor.SetActive(true);
                    }
                }
                if(redComplete) {
                    redKey.SetActive(true);
                    if(mostRecentColor != PaintbrushColor.Red) {
                        redKey.transform.position = redKeyIn.position;
                        redMayor.SetActive(true);
                    }
                    redDoor.SetActive(false);
                }
                if(yellowComplete) {
                    yellowKey.SetActive(true);
                    if(mostRecentColor != PaintbrushColor.Yellow) {
                        yellowKey.transform.position = yellowKeyIn.position;
                        yellowMayor.SetActive(true);
                    }
                    yellowDoor.SetActive(false);
                }
                if(blueComplete) {
                    blueKey.SetActive(true);
                    if(mostRecentColor != PaintbrushColor.Blue) {
                        blueKey.transform.position = blueKeyIn.position;
                        blueMayor.SetActive(true);
                    }
                    blueDoor.SetActive(false);
                }
                if(allComplete) {
                    allMayor.SetActive(true);
                    orangeMayor.SetActive(false);
                    redMayor.SetActive(false);
                    yellowMayor.SetActive(false);
                    blueMayor.SetActive(false);
                    mayor.SetActive(false);
                }

                EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(false));
                StartCoroutine(Move());
            }
            else {
                firstTime = false;
                orangeKey.SetActive(false);
                redKey.SetActive(false);
                yellowKey.SetActive(false);
                blueKey.SetActive(false);
                orangeMayor.SetActive(false);
                redMayor.SetActive(false);
                yellowMayor.SetActive(false);
                blueMayor.SetActive(false);
                allMayor.SetActive(false);
            }
        }
    }

    void _OnPaintbrush(PickupPaintbrush e)
    {
        mostRecentColor = e.color;
        if(e.color == PaintbrushColor.Orange) {
            orangeComplete = true;
        }
        else if(e.color == PaintbrushColor.Red) {
            redComplete = true;
        }
        else if(e.color == PaintbrushColor.Yellow) {
            yellowComplete = true;
        }
        else {
            blueComplete = true;
        }

        if(orangeComplete && redComplete && yellowComplete && blueComplete) {
            allComplete = true;
        }
    }

    IEnumerator Move()
    {
        player.GetComponent<BoxCollider2D>().enabled = false;
        float startTime = Time.time;
        float progress = 0;

        Vector3 startPosition = player.transform.position;
        Transform newPlayerPosition = player.transform;
        if(mostRecentColor == PaintbrushColor.Orange) {
            startPosition = orangeStartPlayerPosition.position;
            newPlayerPosition = orangeEndPlayerPosition;
        }
        else if(mostRecentColor == PaintbrushColor.Red) {
            startPosition = redStartPlayerPosition.position;
            newPlayerPosition = redEndPlayerPosition;
        }
        else if(mostRecentColor == PaintbrushColor.Yellow) {
            startPosition = yellowStartPlayerPosition.position;
            newPlayerPosition = yellowEndPlayerPosition;
        }
        else {
            startPosition = blueStartPlayerPosition.position;
            newPlayerPosition = blueEndPlayerPosition;
        }

        while (progress < 1.0f)
        {
            progress = (Time.time - startTime) / playerMoveDuration;
            player.transform.position = Vector3.Lerp(startPosition, newPlayerPosition.position, progress);
            yield return null;
        }

        player.transform.position = newPlayerPosition.position;

        // Camera Move to show key going into gate
        

        EventBus.Publish<TriggerPlayerCanMove>(new TriggerPlayerCanMove(true));
        player.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
