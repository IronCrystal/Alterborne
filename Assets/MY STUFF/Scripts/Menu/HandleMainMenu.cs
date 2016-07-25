using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleMainMenu : MonoBehaviour {
    public string singlePlayerSceneName;

    public InputField inputLoginUsername;
    public InputField inputLoginPassword;

    public InputField inputCreateEmail;
    public InputField inputCreateUsername;
    public InputField inputCreatePassword;
    public InputField inputCreatePasswordAgain;

    private List<GameObject> LoginUIObjects;
    private List<GameObject> CreateAccountUIObjects;
    private List<GameObject> MainMenuUIObjects;

    // Use this for initialization
    void Start () {
        LoginUIObjects = new List<GameObject>();
        CreateAccountUIObjects = new List<GameObject>();
        MainMenuUIObjects = new List<GameObject>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("LoginUI")) {
            LoginUIObjects.Add(go);
            go.SetActive(false);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CreateAccountUI")) {
            CreateAccountUIObjects.Add(go);
            go.SetActive(false);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MainMenuUI")) {
            MainMenuUIObjects.Add(go);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenLoginUI() {
        foreach (GameObject go in LoginUIObjects) {
            go.SetActive(true);
        }

        foreach (GameObject go in MainMenuUIObjects) {
            go.SetActive(false);
        }

        foreach (GameObject go in CreateAccountUIObjects) {
            go.SetActive(false);
        }
    }

    //Called when clicking Login within LoginUI
    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        // check for errors
        if (www.error == null) {
            Debug.Log(www.text);
        } else {
            Debug.Log(www.error);
        }
    }

    public void Login() {
        string username = inputLoginUsername.text;
        string password = inputLoginPassword.text;
        string url = "localhost/login.php";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }

    //Called when clickign CreateAccount within LoginUI
    public void CreateAccount() {
        foreach (GameObject go in LoginUIObjects) {
            go.SetActive(false);
        }

        foreach (GameObject go in MainMenuUIObjects) {
            go.SetActive(false);
        }

        foreach (GameObject go in CreateAccountUIObjects) {
            go.SetActive(true);
        }
    }

    //Called when clicking Back within LoginUI
    public void BackLogin() {
        foreach (GameObject go in LoginUIObjects) {
            go.SetActive(false);
        }

        foreach (GameObject go in MainMenuUIObjects) {
            go.SetActive(true);
        }

        foreach (GameObject go in CreateAccountUIObjects) {
            go.SetActive(false);
        }
    }
    
    public void CreateNewAccount() {
        string email = inputCreateEmail.text;
        string username = inputCreateUsername.text;
        string password = inputCreatePassword.text;
        string passwordAgain = inputCreatePasswordAgain.text;

        if (password != passwordAgain) {
            Debug.Log("Passwords are not the same!");
            return;
        }
        string url = "localhost/createaccount.php";

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }

    //Called when clicking Back within CreateAccountUI
    public void BackCreate() {
        foreach (GameObject go in LoginUIObjects) {
            go.SetActive(true);
        }

        foreach (GameObject go in MainMenuUIObjects) {
            go.SetActive(false);
        }

        foreach (GameObject go in CreateAccountUIObjects) {
            go.SetActive(false);
        }
    }

    public void SinglePlayer() {
        SceneManager.LoadScene(singlePlayerSceneName);
    }
}
