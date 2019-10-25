using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerate : MonoBehaviour
{
    public GameObject plane;
    public Transform cube;

    Vector3 centerPos;
    Vector3 leftPos;
    Vector3 rightPos;
    Vector3 upPos;
    Vector3 downPos;
    Vector3 leftUpPos;
    Vector3 leftDownPos;
    Vector3 rightUpPos;
    Vector3 rightDownPos;

    GameObject centerPlane;
    GameObject leftPlane;
    GameObject rightPlane;
    GameObject upPlane;
    GameObject downPlane;
    GameObject leftUpPlane;
    GameObject leftDownPlane;
    GameObject rightUpPlane;
    GameObject rightDownPlane;

    List<MapTile> mapTiles = new List<MapTile>();
    // Start is called before the first frame update
    void Start()
    {
        GeneratePlaneAroundCube();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cubePos = cube.localPosition;
        if (cubePos.x - centerPos.x > 5 || cubePos.x - centerPos.x < -5 || cubePos.z - centerPos.z > 5 || cubePos.z - centerPos.z < -5)
            GeneratePlaneAroundCube();
    }

    void GeneratePlaneAroundCube()
    {
        Vector3 cubePos = cube.localPosition;

        int fx = Mathf.RoundToInt(cubePos.x / 10) * 10;
        int fz = Mathf.RoundToInt(cubePos.z / 10) * 10;
        centerPos = new Vector3(fx, 0, fz);
        ComparePlaneAroundCube(new Vector3(fx, 0, fz));
        ComparePlaneAroundCube(new Vector3(fx - 10, 0, fz));
        ComparePlaneAroundCube(new Vector3(fx + 10, 0, fz));
        ComparePlaneAroundCube(new Vector3(fx, 0, fz + 10));
        ComparePlaneAroundCube(new Vector3(fx, 0, fz - 10));
        ComparePlaneAroundCube(new Vector3(fx - 10, 0, fz + 10));
        ComparePlaneAroundCube(new Vector3(fx - 10, 0, fz - 10));
        ComparePlaneAroundCube(new Vector3(fx + 10, 0, fz + 10));
        ComparePlaneAroundCube(new Vector3(fx + 10, 0, fz - 10));

        for (int i = mapTiles.Count-1; i >=0; i--)
        {
            if(mapTiles[i].dirty)
            {
                mapTiles[i].dirty = false;
            }else
            {
                Destroy(mapTiles[i].plane);
                mapTiles.RemoveAt(i);
            }
        }

    }

    void ComparePlaneAroundCube(Vector3 newP)
    {
        for (int i = 0; i < mapTiles.Count; i++)
        {
            bool posE = mapTiles[i].pos.Equals(newP);
            if (mapTiles[i].pos.Equals(newP))
            {
                mapTiles[i].dirty = true;
                return;
            }
        }

        MapTile mapTile = new MapTile();
        mapTile.dirty = true;
        mapTile.pos = newP;
        mapTile.plane = Instantiate(plane, transform);
        mapTile.plane.transform.localPosition = newP;
        mapTile.plane.name = Time.time.ToString();
        mapTiles.Add(mapTile);
    }
}

class MapTile
{
    public Vector3 pos;
    public GameObject plane;
    public bool dirty;
}
