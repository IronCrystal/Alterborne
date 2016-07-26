using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandleMainMenu : MonoBehaviour {

    public InputField inputLoginUsername;
    public InputField inputLoginPassword;

    public InputField inputCreateEmail;
    public InputField inputCreateUsername;
    public InputField inputCreatePassword;
    public InputField inputCreatePasswordAgain;

    public GameObject LoginUIObjects;
    public GameObject CreateAccountUIObjects;
    public GameObject MainMenuUIObjects;

    // Use this for initialization
    void Start () {
        LoginUIObjects.SetActive(false);
        CreateAccountUIObjects.SetActive(false);
        MainMenuUIObjects.SetActive(true);
    }

    public void OpenLoginUI() {
        LoginUIObjects.SetActive(true);
        CreateAccountUIObjects.SetActive(false);
        MainMenuUIObjects.SetActive(false);
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
        LoginUIObjects.SetActive(false);
        CreateAccountUIObjects.SetActive(true);
        MainMenuUIObjects.SetActive(false);
    }

    //Called when clicking Back within LoginUI
    public void BackLogin() {
        LoginUIObjects.SetActive(false);
        CreateAccountUIObjects.SetActive(false);
        MainMenuUIObjects.SetActive(true);      
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
        LoginUIObjects.SetActive(true);
        CreateAccountUIObjects.SetActive(false);
        MainMenuUIObjects.SetActive(false);        
    }

    public void SinglePlayer() {
        SceneManager.LoadScene(1);
    }
}
