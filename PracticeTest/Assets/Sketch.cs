using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour
{
    public GameObject myPrefab;
    public string _WebsiteURL = "http://320-labtest2.azurewebsites.net/tables/Mountain?zumo-api-version=2.0.0";

    void Start()
    {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        http://320-labtest2.azurewebsites.net/tables/Mountain?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Mountain[] mountainsArray = JsonReader.Deserialize<Mountain[]>(jsonResponse);

        //instantiate two variables with corresponding variable names and assigned values
        int i = 0;
        int length = mountainsArray.Length;

        //loop through the array of objects and access each object individually + instantiate each one with specified paramters
        foreach (Mountain mountain in mountainsArray)
        {
            float perc = i / (float)length;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            //pull position data from array

            //float x = 1.8f + sin * totalDistance;
            float x = mountainsArray[i].X;
            //float y = 5.0f;
            float y = mountainsArray[i].Y;
            //float z = 0.0f;
            float z = mountainsArray[i].Z;

            //set posiiton, rotation, text and size based on data in array

            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            //newCube.GetComponent<CubeScript>().SetSize(.45f * (1.0f - perc));         <-- Manual Size Setting
            newCube.GetComponent<CubeScript>().SetSize(mountainsArray[i].Size * 0.2f);
            //newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;       <-- Manual rotation speed
            newCube.GetComponent<CubeScript>().rotateSpeed = 0;
            newCube.GetComponentInChildren<TextMesh>().text = mountainsArray[i].MountainName;
            i++;



            //----------------------
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
