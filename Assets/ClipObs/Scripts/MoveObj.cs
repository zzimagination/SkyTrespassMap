using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MoveObj : MonoBehaviour
{
    public Camera myCamera;
    public float findPathR;
    public float moveSampling;
    public NavMeshAgent agent;
    public GameObject ground;

    CharacterController controller;
    NavMeshData NavMeshData;
    Vector3 pathPos;
    List<NavMeshBuildSource> navMeshBuildSources = new List<NavMeshBuildSource>();
    List<NavMeshBuildMarkup> marks = new List<NavMeshBuildMarkup>();

    // Start is called before the first frame update
    void Start()
    {
        InitMover();
    }


    private void Update()
    {
        Vector3 pos = transform.localPosition;
        Vector3 nextPos = new Vector3((int)(pos.x / moveSampling) * moveSampling, (int)(pos.y / moveSampling) * moveSampling, (int)(pos.z / moveSampling) * moveSampling);
        if (!nextPos.Equals(pathPos))
        {
            pathPos = nextPos;
            UpdataPath();
        }
    }

    public void InitMover()
    {
        controller = GetComponent<CharacterController>();
        NavMeshData = new NavMeshData(0);
        NavMesh.AddNavMeshData(NavMeshData);
        Vector3 pos = transform.localPosition;
        pathPos = new Vector3((int)(pos.x / moveSampling) * moveSampling, (int)(pos.y / moveSampling) * moveSampling, (int)(pos.z / moveSampling) * moveSampling);
        var asy= UpdataPath();

        StartCoroutine(OpenAgen(asy));
    }

    public AsyncOperation UpdataPath()
    {

        var cs= Physics.OverlapSphere(pathPos, findPathR, 1 << 8);
        navMeshBuildSources.Clear();
        for (int i = 0; i < cs.Length; i++)
        {
          
            NavMeshBuildSource source = new NavMeshBuildSource()
            {
                sourceObject = cs[i].GetComponent<MeshFilter>().sharedMesh,
                shape = NavMeshBuildSourceShape.Mesh,
                transform = cs[i].transform.localToWorldMatrix,

            };
            navMeshBuildSources.Add(source);
        }

        Bounds bounds = new Bounds(pathPos, new Vector3(findPathR, findPathR, findPathR));

        NavMeshBuildSettings setting = NavMesh.GetSettingsByIndex(0);

       return NavMeshBuilder.UpdateNavMeshDataAsync(NavMeshData, setting, navMeshBuildSources, bounds);

    }

    public void OnClickMove()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (Physics.Raycast(myCamera.ScreenPointToRay(mousePos), out RaycastHit hitInfo, 100, 1 << 8))
        {
            Vector3 pos = hitInfo.point;
            agent.destination = pos;
        }
    }

    IEnumerator OpenAgen(AsyncOperation asy)
    {
        yield return asy;
        agent.enabled = true;
    }

}
