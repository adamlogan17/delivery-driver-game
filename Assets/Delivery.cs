using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;

    int currScore = 0;

    [SerializeField] Color32 noPackageColour = new Color32(1, 1, 1, 1);

    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject package = GameObject.FindGameObjectsWithTag("Package")[0]; // gets first object with the tag Package (there should only be 1)
        GameObject customer = GameObject.FindGameObjectsWithTag("Customer")[0]; // gets first object with the tag Customer (there should only be 1)
        customer.GetComponent<SpriteRenderer>().color = new Color32(1, 1, 1, 0);
        putFrontHouse(package);
        putFrontHouse(customer);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Ouch!");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var collisionObject = other.GetComponent<SpriteRenderer>();

        if(other.tag == "Package" && !hasPackage) {
            Debug.Log("Package Picked Up!");
            hasPackage = true;

            GameObject customer = GameObject.FindGameObjectsWithTag("Customer")[0]; // gets first object with the tag Customer (there should only be 1)
            customer.GetComponent<SpriteRenderer>().color = collisionObject.color;

            GameObject scoreBg = GameObject.FindGameObjectsWithTag("BgScore")[0];
            scoreBg.GetComponent<UnityEngine.UI.Image>().color = collisionObject.color;

            collisionObject.color = new Color32(1, 1, 1, 0);

            putFrontHouse(other.gameObject);
            putFrontHouse(customer);
        }

        if(other.tag == "Customer" && hasPackage) {
            Debug.Log("Delivered Package!");
            hasPackage = false;

            GameObject package = GameObject.FindGameObjectsWithTag("Package")[0]; // gets first object with the tag Package (there should only be 1)

            GameObject scoreObj = GameObject.FindGameObjectsWithTag("ScoreCounter")[0];
            currScore++;
            scoreObj.GetComponent<UnityEngine.UI.Text>().text = currScore.ToString();

            package.GetComponent<SpriteRenderer>().color = new Color32((byte) Random.Range(0, 255), (byte) Random.Range(0, 255), (byte) Random.Range(0, 255), 255);
            collisionObject.color = new Color32(1, 1, 1, 0);
            spriteRenderer.color = noPackageColour;
        }
    }

    private void putFrontHouse(GameObject objectToMove) {
        GameObject[] houseXMinus = GameObject.FindGameObjectsWithTag("HouseXMinus");
        GameObject[] houseYMinus = GameObject.FindGameObjectsWithTag("HouseYMinus");
        GameObject[] houseXPlus = GameObject.FindGameObjectsWithTag("HouseXPlus");
        GameObject[] houseYPlus = GameObject.FindGameObjectsWithTag("HouseYPlus");

        GameObject house = null;
        var xOrY =  Random.Range(1, 4);
        var rnd = 0;
        switch(xOrY) {
            case 1:
                rnd = Random.Range(0,houseXPlus.Length);
                house = houseXPlus[rnd];
                break;
            case 2:
                rnd = Random.Range(0,houseXMinus.Length);
                house = houseXMinus[rnd];
                break;
            case 3:
                rnd = Random.Range(0,houseYPlus.Length);
                house = houseYPlus[rnd];
                break;
            case 4:
                rnd = Random.Range(0,houseYMinus.Length);
                house = houseYMinus[rnd];
                break;
        }
        
        float houseXval = house.transform.position.x;
        float houseYval = house.transform.position.y;
        float houseZval = house.transform.position.z;

        switch(xOrY) {
            case 1:
                objectToMove.transform.position = new Vector3(houseXval + 7, houseYval, houseZval); // x, y, x
                break;
            case 2:
                objectToMove.transform.position = new Vector3(houseXval - 7, houseYval, houseZval);
                break;
            case 3:
                objectToMove.transform.position = new Vector3(houseXval, houseYval + 7, houseZval);
                break;
            case 4:
                objectToMove.transform.position = new Vector3(houseXval, houseYval - 7, houseZval);
                break;
        }    
    }

    
}
