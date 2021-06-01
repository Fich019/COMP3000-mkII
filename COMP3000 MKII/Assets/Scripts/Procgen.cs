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


    public GameObject Building; //Was called Prefab in GitHub image
    public GameObject streetVert;
    public GameObject streetHor;
    public GameObject streetCross;
    public GameObject lamp;
    public GameObject carspawner;
    public GameObject playerSpawner;
    public GameObject objects;


    private int GlobX; //For gif generation version
    private int GlobY;


    //public RawImage visulizationUI;
    private Texture2D noiseTexture;


    public int[,] citygrid;


    [SerializeField]
    private int numOfStreets, numOfBuildingsBet;






    // Start is called before the first frame update
    void Start()
    {

        citygrid = new int[perlinGridStepSizeX, perlinGridStepSizeY]; //Instantiate the 2D array with the grid step size

        
        offestX = Random.Range(0, 99999); //Random offest tha gets applied to the Perlin noise
        offsetY = Random.Range(0, 99999);

        heightScale = Random.Range(2, 6); //Random Height scale that gets added to the height of the buildings when they are placed

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


    //This method creates the Perlin noise texture with the help from other methods.
    //What this method does is applies the calculated colour from that method to
    //each of the pixels in the 2D texture that is created.
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
        //visulizationUI.texture = noiseTexture;


    }


    //This method takes the x and y coordinate passed from the generate
    //texture function and converts them into a float by dividing them
    //by the width of the texture times the scale or zoom of the texture
    // and adding the random offset. It does this for the y as well then
    //uses both to get a sample from the PerlinNoise function.
    //This float is then used to create a colour and that is then
    //returned and used in the GenerateTexture() function.
    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offestX;
        float yCoord = (float)y / height * scale + offsetY;


        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(sample, sample, sample);
    }



    //This is the primary method of the script. This interates through
    //the grid step size and checks each position against the 2D array.
    //Depending on what value is present differnent objects are spawned
    //and if it fails all of the checks then it has to be a building so 
    //it spawns one in.
    void VisualizePrefabs()
    {
        GameObject objectParent = new GameObject("ObjectParent");
        objectParent.transform.SetParent(this.transform);

        for (int x = 0; x < perlinGridStepSizeX -1; x++)
        {
            for (int y = 0; y < perlinGridStepSizeY -1; y++)
            {

                int ranObjects = Random.Range(0,20);


                if (citygrid[x, y] == -1) //Spawn Street Horizontal
                {
                    //spawn street here
                    GameObject spawnH = Instantiate(streetHor, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnH.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 0.74f, 0.6f, y + 1f) + transform.position, lamp.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1) //If randObjects is 1, spawn collectable item for player
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                }
                else if (citygrid[x, y] == -2) //Spawn Street Vertical
                {
                    //spawn street here
                    GameObject spawnV = Instantiate(streetVert, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation);
                    spawnV.transform.SetParent(objectParent.transform);

                    GameObject spawnS = Instantiate(lamp, new Vector3(x + 1f, 0.6f, y + 1.3f) + transform.position, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1) //If randObjects is 1, spawn collectable item for player
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                }
                else if (citygrid[x, y] == -3) //Spawn Street Crossroad
                {
                    //spawn street here
                    GameObject spawnC = Instantiate(streetCross, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetCross.transform.rotation);
                    spawnC.transform.SetParent(objectParent.transform);

                    if (ranObjects == 1) //If randObjects is 1, spawn collectable item for player
                    {
                        GameObject spawnO = Instantiate(objects, new Vector3(x + 1f, Random.Range(3, 7), y + 1f) + transform.position, objects.transform.rotation);
                        spawnO.transform.SetParent(objectParent.transform);
                    }
                }
                else if (citygrid[x, y] == -6) //Car Spawner, start of array, horizontal
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation);
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -7) //Car Spawner, start of array, vertical
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -11) //Car Spawner, end of array, horizontal
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetHor.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -12) //Car Spawner, end of array, vertical
                {
                    GameObject spawnS = Instantiate(carspawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, streetVert.transform.rotation * Quaternion.Euler(0f, 0f, 180f));
                    spawnS.transform.SetParent(objectParent.transform);
                }
                else if (citygrid[x, y] == -9) //Player Spawner
                {
                    GameObject spawnV = Instantiate(playerSpawner, new Vector3(x + 1f, 0.6f, y + 1f) + transform.position, playerSpawner.transform.rotation);
                    spawnV.transform.SetParent(objectParent.transform);
                }
                else //Spawn building in nothing else.
                {

                    float sample = SampleStepped(x, y);

                    GameObject spawn = Instantiate(Building, new Vector3(x + 1f, sample * heightScale - 6f, y + 1f) + transform.position, transform.rotation); //Applys Perlin noise to height of building as well as randomised height scale to add more variance
                    spawn.transform.SetParent(objectParent.transform);
                    int i = Random.Range(0, 40);

                }
                
                
            }
            
        }

        objectParent.transform.position = new Vector3(-perlinGridStepSizeX * .5f, -1 * .5f, -perlinGridStepSizeY * .5f);
    }


    //Used for creating gifs, ignore
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

                    //int ranSelect = Random.Range(0, Prefabs.Length);
                    float sample = SampleStepped(x, y);

                    GameObject spawn = Instantiate(Building, new Vector3(x + 1f, sample * heightScale - 6f, y + 1f) + transform.position, transform.rotation);
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


    //Used to generate streets on the Y axis. It stores
    //number of buildings between and keeps adding to it after
    //each street is placed so that the next street can be placed
    //correctly in the next position in the array. Sets the start
    //of the street as a tunnel and the end as well as defining what 
    //street it is.
    void GenerateStreetsY()
    {
        int r = numOfBuildingsBet;

        for (int x = 0; x < numOfStreets; x++)
        {
            citygrid[r, 0] += -5;

            for (int y = 0; y < perlinGridStepSizeY; y++)
            {
                citygrid[r, y] += -1;

            }

            citygrid[r, perlinGridStepSizeY - 2] += -10;
            r += numOfBuildingsBet;


        }

    }


    //Does exactly the same as the GenerateStreetsY() but
    //instead on the X axis.
    void GenerateStreetsX()
    {
        int r = numOfBuildingsBet;

        for (int y = 0; y < numOfStreets; y++)
        {
            citygrid[0, r] += -5;

            for (int x = 0; x < perlinGridStepSizeX; x++)
            {
                citygrid[x, r] += -2;

            }
            citygrid[perlinGridStepSizeX -2, r] += -10;
            r += numOfBuildingsBet;


        }

    }


    //Takes the x and y of the current position in the grid step 
    //nested loop and converts them back to integers. These are
    //then used to get the pixel at that position in the Perlin
    //noise texture and returns a float. The float is then used
    //when deciding the height of the buildings.
    public float SampleStepped(int x, int y)
    {
        int gridStepSizeX = Mathf.FloorToInt(width / perlinGridStepSizeX);
        int gridStepSizeY = Mathf.FloorToInt(height / perlinGridStepSizeY);

        float sampleFloat = noiseTexture.GetPixel((Mathf.FloorToInt(x * gridStepSizeX)), (Mathf.FloorToInt(y * gridStepSizeY))).grayscale;
        return sampleFloat;
    }


    //Creates a list to hold all of the factors of the grid 
    //step size -1. Checks while i < grid step size if the value
    //currently at i if divided by grid step size would equal 0.
    //If it does then its a factor and gets added to the list.
    //After looping through the step size, a random integer is
    //chosen between the range of 1 and the step size -1. This
    //is done to remove the factors of 1 and itself. The random
    //select is used to get a factor from the list and then its 
    //set as the number of streets at the start of the program.
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


    //Divides the grid step size by the number of streets to find
    //the correctly number of buildings to go between the streets
    //so that it equals 0 when its finished.
    public int GetBuildingBetween()
    {
        int tempBuild = (perlinGridStepSizeX - 1) / numOfStreets;

        return tempBuild;
    }
}
