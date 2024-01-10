using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlaceTurret : MonoBehaviour
{
    public static PlaceTurret Instance;
    public Turrets_Info turret;
    private Camera _camera;
    private Transform _transform;
    [SerializeField] private Vector2 _mousePosition;
    public GameObject preview;
    public GameObject TurretPrefab;
    public GameObject ParentTurret;
    public GameObject EnnemieSpawn;
    public GameObject Objective;
    private bool putTurret;

    private GameObject lastTurret;

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        _camera = Camera.main;
        _transform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MakePreview();
        Debug.Log(IsAlreadyAPath());
        if (!IsAlreadyAPath())
        {
            Destroy(lastTurret);
        }
    }

    public void MakePreview()
    {
        Ray ray = _camera.ScreenPointToRay(_mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 2000);

        if (Physics.Raycast(ray, out RaycastHit hit, 2000))
        {
            if(hit.collider.CompareTag("Floor") && turret != null && Economie.Instance.CanBuy(turret.cost))
            {
                if(!preview.activeSelf)
                    preview.SetActive(true);
                preview.gameObject.transform.position = hit.point + new Vector3(0,2,0);

                if(putTurret && turret != null) 
                {
                    if (Economie.Instance.BuySomething(turret.cost))
                    {
                        GameObject go = Instantiate<GameObject>(turret.turret, hit.point, new Quaternion(0, 0, 0, 0), ParentTurret.transform);
                        go.GetComponentInChildren<TurretController>().info = turret;
                        lastTurret = go;
                    }

                    putTurret = false;
                    turret = null;
                }
            }
            else
            {
                preview.SetActive(false);
            }




        }
        else
        {
            preview.SetActive(false);
        }

    }

    public bool IsAlreadyAPath()
    {
        //NavMeshPath path = new();
        //EnnemieSpawn.GetComponent<NavMeshAgent>().CalculatePath(Objective.transform.position, path);
        //return EnnemieSpawn.GetComponent<NavMeshAgent>().CalculatePath(Objective.transform.position,path);
        //NavMeshPath path = new NavMeshPath();
        //if (NavMesh.CalculatePath(EnnemieSpawn.transform.position, Objective.transform.position, NavMesh.AllAreas, path))

        NavMesh.SamplePosition(EnnemieSpawn.transform.position, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
        NavMesh.SamplePosition(Objective.transform.position, out NavMeshHit hitB, 10f, NavMesh.AllAreas);

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas , path))
        //if(EnnemieSpawn.GetComponent<NavMeshAgent>().CalculatePath(hitB.position, path))
        {
            
            bool isvalid = true;
            if (path.status != NavMeshPathStatus.PathComplete)
                isvalid = false;
            return isvalid;
        }
        return false;
    }

    public void SelectTurret(TurretsButton tutu)
    {
        turret = tutu.GetTurretInfo;
    }

    public void SetMousePosition(Vector2 mousePos)
    {
        _mousePosition = mousePos;
    }

    public void SetPutTurret(bool put)
    {
        putTurret = put;
    }

}
