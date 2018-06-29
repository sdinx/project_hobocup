using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceanManager : MonoBehaviour {

    public Goal goal;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetButtonDown("Start")&& goal.isGameClear)
        {
            

                switch (SceneManager.GetActiveScene().name)
                {
                    case "Title":
                        {

                            SceneManager.LoadScene("Stage_01");
                            break;
                        }

                    case "Stage_01":
                        {
                            SceneManager.LoadScene("Stage_02");
                            break;

                        }
                    case "Stage_02":
                        {
                            SceneManager.LoadScene("Stage_03");
                            break;


                        }
                    case "Stage_03":
                        {
                            SceneManager.LoadScene("Stage_04");
                            break;

                        }
                    case "Stage_04":
                        {
                            SceneManager.LoadScene("Title");
                            break;


                        }
                }
            

          


        }




    }
}
