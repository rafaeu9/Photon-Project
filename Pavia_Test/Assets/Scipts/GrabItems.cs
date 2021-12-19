using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum GrabedState
{
    Attracting,
    Connected,
    Null
}

public class GrabItems : MonoBehaviour
{
    public int GrabbingDistance = 0;
    public float AttractingSpeed = 0.5f;    
    public float GrabingAdjustDistance = 0.02f;


    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject UI;

    private PhotonView photonview;

    [SerializeField]
    private GameObject GrabbedItem;

    [SerializeField]
    private GrabedState grabedState = GrabedState.Null;

    

    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        UI = GameObject.Find("Canvas").transform.Find("InGame").Find("GrabTxt").gameObject;

        photonview = GetComponentInParent<PhotonView>();

        


    }

    // Update is called once per frame
    void Update()
    {
        if (photonview.IsMine)
        {
            ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2,0));            

            switch (grabedState)
            {
                case GrabedState.Attracting:

                    GrabbedItem.transform.position = Vector3.Lerp(GrabbedItem.transform.position, transform.position, AttractingSpeed);

                    if(Vector3.Distance(transform.position, GrabbedItem.transform.position) <= GrabingAdjustDistance)
                    {
                        GrabbedItem.transform.parent = transform;
                                                    
                        grabedState = GrabedState.Connected;
                    }

                    break;
                case GrabedState.Connected:

                    GrabbedItem.transform.position = Vector3.Lerp(GrabbedItem.transform.position, transform.position, AttractingSpeed);

                    if (Vector3.Distance(transform.position, GrabbedItem.transform.position) <= GrabingAdjustDistance)
                    {
                        GrabbedItem.transform.localPosition = Vector3.zero;
                    }

                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        GrabbedItem.transform.parent = null;
                        GrabbedItem.GetComponent<Rigidbody>().useGravity = true;
                        GrabbedItem = null;
                        grabedState = GrabedState.Null;
                    }

                    break;

                case GrabedState.Null:

                    if (Physics.Raycast(ray, out hit) && hit.transform.tag == "throwable")
                    {
                        //Transform objectHit = hit.transform;
                        // Do something with the object that was hit by the raycast.


                        if (Vector3.Distance(transform.position, hit.transform.position) <= GrabbingDistance)
                        {
                            UI.SetActive(true);

                            if (Mouse.current.rightButton.wasPressedThisFrame)
                            {
                                grabedState = GrabedState.Attracting;

                                GrabbedItem = hit.transform.gameObject;

                                GrabbedItem.GetComponent<Rigidbody>().useGravity = false;
                            }
                        }
                    }
                    else
                    UI.SetActive(false);

                    break;
                default:       
                    break;
 
            }
        }
    }
}
