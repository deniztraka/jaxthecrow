using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAnimalSpawner : MonoBehaviour
{
    public List<GameObject> AnimalList;
    private GameObject[] animalListArray;

    // Start is called before the first frame update
    void Start()
    {
        animalListArray = AnimalList.ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            GameObject.Destroy(this.gameObject);
            Spawn();
        }

    }

    void Spawn()
    {
        var index = Random.Range(0, AnimalList.Count);

        var instantiatedObj = Instantiate(animalListArray[index], new Vector3(this.gameObject.transform.position.x - 12, 5, 0), animalListArray[index].transform.rotation);
        var characterController2D = instantiatedObj.GetComponent<CharacterController2D>();
        //characterController2D.
    }
}
