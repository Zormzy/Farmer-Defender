using UnityEngine;

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
    private bool putTurret;

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
                preview.gameObject.transform.position = hit.point;

                if(putTurret && turret != null) 
                {
                    if (Economie.Instance.BuySomething(turret.cost))
                    {
                        GameObject go = Instantiate<GameObject>(turret.turret, hit.point, new Quaternion(0, 0, 0, 0), ParentTurret.transform);
                        go.GetComponentInChildren<TurretController>().info = turret;
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
