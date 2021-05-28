using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Procgen : MonoBehaviour
{
   public static Procgen instance = null;



    public int width = 256;
    public int height = 256;

    public float scale;

    private float heightScale;

    private float offestX;
    private float offsetY;

    public int perlinGridStepSizeX;
    public int perlinGridStepSizeY;


    public bool visualizeObjects = false;
    public GameObject[] Prefabs;
    public GameObject Prefab;
    public GameObject streetVert;
    public GameObject streetHor;
    public GameObject streetCross;
    public GameObject lamp;
    public GameObject carspawner;
    public GameObject playerSpawner;
    public GameObject objects;

    public int GlobX; //For gif generation version
    public int GlobY;

    public RawImage visulizationUI;
    private Texture2D noiseTexture;

    public int[,] citygrid;

    [SerializeField]
    private int numOfStreets, numOfBuildingsBet;

    // Start is called before the first frame update
    void Start()
    {

        citygrid = new int[perlinGridStepSizeX, perlinGridStepSizeY];

        
        offestX = Random.Range(0, 99999);
        offsetY = Random.Range(0, 99999);

        heightScale = Random.Range(2, 6);

        GenerateTexture();

        numOfStreets = GetFactor();
        numOfBuildingsBet = GetBuildingBetween();

        citygrid[numOfBuildingsBet, 1] += -8;
        GenerateStreetsY();
        GenerateStreetsX();
        VisualizePrefabs();

    }

    // Update is called once per frame
    void Update()
    {
        //StepVisualizePrefabs(GlobX); //Gif generation version
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    public void Generate()
    {
        GenerateTexture();
        if (visualizeObjects)
        {
            
            VisualizePrefabs();
        }

    }


    void GenerateTexture()
    {
        noiseTexture = new Texture2D(width, height);

        //generate noisemap
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color sample = CalculateColour(x, y);
                noiseTexture.SetPixel(x, y, sample);
            }
        }

        noiseTexture.Apply();
        visulizationUI.texture = noiseTexture;


    }


    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offestX;
        float yCoord = (float)y / height * scale + offsetY;


        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(sample, sample, sample);
    }


    void VisualizePrefabs()
    {
        GameObject objectParent = new GameObject("ObjectParent");
        objectParent.transform.SetParent(this.transform);

        for (int x = 0; x < perlinGridStepSizeX -1; x++)
        {
            for (int y = 0; y < perlinGridStepSizeY -1; y++)
            {

                //ExecuteAfterTime();
                //System.Threading.Thread.Sleep(3000);
                int ranObjects = Random.Range(0,20);

                // if (citygrid[x,] )
                if (citygrid[x, y] == -1)
                {
                    //spawn street here
                    GameObject spawnH = Instantiate(streetHor, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnH.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 0.74f, 0.6f, y + 1f) + transform.position, lamp.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //if (count == 3)
                    //{
                    //    GameObject spawnS = Instantiate(lamp, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, lamp.transform.rotation);
                    //    spawnS.transform.SetParent(objectParent.transform);
                    //    count = 0;
                    //}
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -2)
                {
                    //spawn street here
                    GameObject spawnV = Instantiate(streetVert, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation);
                    spawnV.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 1f, 0.6f, y + 1.3f) + transform.position, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -3)
                {
                    //spawn street here
                    GameObject spawnC = Instantiate(streetCross, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetCross.transform.rotation);
                    spawnC.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -6)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -7)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -11)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -12)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -9)
                {
                    GameObject spawnV = Instantiate(playerSpawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, playerSpawner.transform.rotation);
                    spawnV.transform.SetParent(objectParent.transform);
                }
                //else if (citygrid[x, y] == -13)
                //{
                //    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                //    spawnS.transform.SetParent(objectParent.transform);
                //}
                else
                {
                    Color check = CalculateColour(x, y);

                    //if (check.g < 0.4f)
                    //{
                    //    int ranSelect = Random.Range(0, Prefabs.Length);

                    //    GameObject spawn = Instantiate(Prefab, new Vector3(x, SampleStepped(x,y) * heightScale, y) + transform.position, transform.rotation);
                    //    int i = Random.Range(0, 20);

                    //    spawn.transform.SetParent(objectParent.transform);
                    //    if (i == 1) { GameObject spawn2 = Instantiate(Prefabs[ranSelect], (new Vector3(x, Random.Range(1,3), y) - new Vector3(0,0.5f,0)) + transform.position, transform.rotation); spawn2.transform.SetParent(objectParent.transform); }
                    //}

                    int ranSelect = Random.Range(0, Prefabs.Length);
                    float sample = SampleStepped(x, y);

                    GameObject spawn = Instantiate(Prefab, new Vector3(x + 1f, sample * heightScale - 6f, y + 1f) + transform.position, transform.rotation);
                    spawn.transform.SetParent(objectParent.transform);
                    int i = Random.Range(0, 40);

                    //if (i == 1)
                    //{
                    //    GameObject spawn2 = Instantiate(Prefabs[ranSelect], (new Vector3(x, (sample * heightScale) + Random.Range(1, 3), y) - new Vector3(0, 0.5f, 0)) + transform.position, transform.rotation);
                    //    spawn2.transform.SetParent(objectParent.transform);
                    //}



                    //Instantiate(spawn);
                }
                
                
            }
            
        }

        objectParent.transform.position = new Vector3(-perlinGridStepSizeX * .5f, -1 * .5f, -perlinGridStepSizeY * .5f);
    }


    public void StepVisualizePrefabs(int x)
    {
        GameObject objectParent = new GameObject("ObjectParent");
        objectParent.transform.SetParent(this.transform);
        for (int y = 0; y < perlinGridStepSizeY - 1; y++)
        {

            if (x <= perlinGridStepSizeX - 1 && y <= perlinGridStepSizeY - 1)
            {

                //ExecuteAfterTime();
                //System.Threading.Thread.Sleep(3000);
                int ranObjects = Random.Range(0, 20);

                // if (citygrid[x,] )
                if (citygrid[x, y] == -1)
                {
                    //spawn street here
                    GameObject spawnH = Instantiate(streetHor, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnH.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 0.74f, 0.6f, y + 1f) + transform.position, lamp.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //if (count == 3)
                    //{
                    //    GameObject spawnS = Instantiate(lamp, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, lamp.transform.rotation);
                    //    spawnS.transform.SetParent(objectParent.transform);
                    //    count = 0;
                    //}
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -2)
                {
                    //spawn street here
                    GameObject spawnV = Instantiate(streetVert, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation);
                    spawnV.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 1f, 0.6f, y + 1.3f) + transform.position, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -3)
                {
                    //spawn street here
                    GameObject spawnC = Instantiate(streetCross, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetCross.transform.rotation);
                    spawnC.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1)
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                    //Debug.Log("pp");
                }
                else if (citygrid[x, y] == -6)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -7)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -11)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -12)
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                //else if (citygrid[x,y] == -9)
                //{
                //    GameObject spawnV = Instantiate(playerSpawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, playerSpawner.transform.rotation);
                //    spawnV.transform.SetParent(objectParent.transform);
                //}
                //else if (citygrid[x, y] == -13)
                //{
                //    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                //    spawnS.transform.SetParent(objectParent.transform);
                //}
                else
                {
                    Color check = CalculateColour(x, y);

                    //if (check.g < 0.4f)
                    //{
                    //    int ranSelect = Random.Range(0, Prefabs.Length);

                    //    GameObject spawn = Instantiate(Prefab, new Vector3(x, SampleStepped(x,y) * heightScale, y) + transform.position, transform.rotation);
                    //    int i = Random.Range(0, 20);

                    //    spawn.transform.SetParent(objectParent.transform);
                    //    if (i == 1) { GameObject spawn2 = Instantiate(Prefabs[ranSelect], (new Vector3(x, Random.Range(1,3), y) - new Vector3(0,0.5f,0)) + transform.position, transform.rotation); spawn2.transform.SetParent(objectParent.transform); }
                    //}

                    int ranSelect = Random.Range(0, Prefabs.Length);
                    float sample = SampleStepped(x, y);

                    GameObject spawn = Instantiate(Prefab, new Vector3(x + 1f, sample * heightScale - 6f, y + 1f) + transform.position, transform.rotation);
                    spawn.transform.SetParent(objectParent.transform);
                    int i = Random.Range(0, 40);

                    //if (i == 1)
                    //{
                    //    GameObject spawn2 = Instantiate(Prefabs[ranSelect], (new Vector3(x, (sample * heightScale) + Random.Range(1, 3), y) - new Vector3(0, 0.5f, 0)) + transform.position, transform.rotation);
                    //    spawn2.transform.SetParent(objectParent.transform);
                    //}



                    //Instantiate(spawn);
                }


            }
        
            int gfunk = 0;
            float Newtime = 3.00f;
            for (float i = Newtime; i >= 0; i -= Time.deltaTime) { gfunk++; Debug.Log(gfunk); }
        }

        objectParent.transform.position = new Vector3(-perlinGridStepSizeX * .5f, -1 * .5f, -perlinGridStepSizeY * .5f);
        GlobX = x + 1;
        
    }


    void GenerateStreetsY()
    {
        int r = numOfBuildingsBet;
        //int y = 0;
        for (int x = 0; x < numOfStreets; x++)
        {
            citygrid[r, 0] += -5;

            for (int y = 0; y < perlinGridStepSizeY; y++)
            {
                citygrid[r, y] += -1;


                //Debug.Log(citygrid[r, y]);
                // r += 3;

                //if ( x == 3)
                //{
                //    break;
                //}
            }

            citygrid[r, perlinGridStepSizeY - 2] += -10;
            r += numOfBuildingsBet;

            //while ( y < perlinGridStepSizeY)
            //{
            //    citygrid[r, y] = -1;

            //    y++;
            //}


        }

    }


    void GenerateStreetsX()
    {
        int r = numOfBuildingsBet;
        //int y = 0;
        for (int y = 0; y < numOfStreets; y++)
        {
            citygrid[0, r] += -5;

            for (int x = 0; x < perlinGridStepSizeX; x++)
            {
                citygrid[x, r] += -2;

                //Debug.Log(citygrid[x, r]);
                // r += 3;

                //if ( x == 3)
                //{
                //    break;
                //}
            }
            citygrid[perlinGridStepSizeX -2, r] += -10;
            r += numOfBuildingsBet;

            

            //while ( y < perlinGridStepSizeY)
            //{
            //    citygrid[r, y] = -1;

            //    y++;
            //}


        }

    }


    public float SampleStepped(int x, int y)
    {
        int gridStepSizeX = Mathf.FloorToInt(width / perlinGridStepSizeX);
        int gridStepSizeY = Mathf.FloorToInt(height / perlinGridStepSizeY);

        float sampleFloat = noiseTexture.GetPixel((Mathf.FloorToInt(x * gridStepSizeX)), (Mathf.FloorToInt(y * gridStepSizeY))).grayscale;
        return sampleFloat;
    }


    public int GetFactor()
    {

        List<int> factors = new List<int>();

        for (int i = 1; i < (perlinGridStepSizeX - 1); i++)
        {
            if ((perlinGridStepSizeX - 1) % i == 0)
            {
                factors.Add(i);
            }
        }

        int ranSelect = Random.Range(1, factors.Capacity -1);

        int chosenFac = factors[ranSelect];

        Debug.Log(factors);

        return chosenFac;

    }


    public int GetBuildingBetween()
    {
        int tempBuild = (perlinGridStepSizeX - 1) / numOfStreets;

        return tempBuild;
    }
}
