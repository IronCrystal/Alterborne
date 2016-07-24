using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerifyLogin : MonoBehaviour {

    public InputField inputUsername;
    public InputField inputPassword;

    void Start() {
        
    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

         // check for errors
        if (www.error == null) {
            Debug.Log(www.data);
        } else {
            Debug.Log(www.error);
        }
    }

    public void Login() {
        string username = inputUsername.text;
        string password = inputPassword.text;
        string url = "localhost/test.php";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }
}