using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{

    bool bLoginUIShow = false;
    bool bShowLoginFailedPopup = false;
    string strReasonOfLoginFailed;
    GameObject gInputID, gInputPW, gBTNsign, gTXTerr;
    GameObject gLoginForm;

    InputField inputID, inputPW;

    GameObject gBTNshowLoginUI;
    GameObject txtLoginErrorMsg;

    //public Rect windowRect = new Rect(50, 80, 300, 500);
    //void OnGUI()
    //{
    //    if (bShowLoginFailedPopup)
    //        windowRect = GUI.Window(0, windowRect, DoMyWindow, "Login Failed");
    //}
    //void DoMyWindow(int windowID)
    //{
    //    GUILayout.Label(strReasonOfLoginFailed);
    //    if (GUILayout.Button("Okay"))
    //        bShowLoginFailedPopup = false;

    //}


    public void ShowSignInUI()
    {
        bLoginUIShow = !bLoginUIShow;

    }

    void ButtonStatus(bool _b)
    {
        inputID.enabled = _b;
        inputPW.enabled = _b;
        gBTNsign.GetComponent<Button>().enabled = _b;
    }

    void SignIn(string _id, string _pw)
    {
        strReasonOfLoginFailed = "Signing In...";

        ParseUser.LogInAsync(_id, _pw).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                // The login failed. Check the error to see why.
                foreach (var e in t.Exception.InnerExceptions)
                {
                    ParseException parseException = (ParseException)e;
                    strReasonOfLoginFailed = parseException.Message;
                }
            }
            else
            {
                // Login was successful.
                //bLoginUIShow = false;
                strReasonOfLoginFailed = "Login successful!";
            }
        });
    }

    public void ConnectToParseAndSignIn()
    {
        if (inputID.text == "")
        {
            strReasonOfLoginFailed = "Please enter your email address.";
            return;
        }
        else if (inputPW.text == "")
        {
            strReasonOfLoginFailed = "Please enter your password.";
            return;
        }

        strReasonOfLoginFailed = "Signing Up...";

        ParseUser user = new ParseUser();
        user.Password = inputPW.text;
        user.Email = inputID.text;
        user.Username = inputID.text;

        Debug.Log("Registrating user " + inputID.text + "...");

        user.SignUpAsync().ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                foreach (var e in t.Exception.InnerExceptions)
                {
                    ParseException parseException = (ParseException)e;
                    Debug.Log("Error message " + parseException.Message);
                    Debug.Log("Error code: " + parseException.Code);
                    strReasonOfLoginFailed = parseException.Message;

                    if (parseException.Code == ParseException.ErrorCode.UsernameTaken)
                        this.SignIn(inputID.text, inputPW.text);
                }
                // The login failed. Check t.Exception to see why.
                Debug.Log("Registration failed! " + t.Exception.Message);
                strReasonOfLoginFailed = "Registration failed! ";
            }
            else
            {
                // Login was successful.
                Debug.Log("Registration was successful!");
                //SignIn(inputID.text, inputPW.text);
                strReasonOfLoginFailed = "Login successful!";
            }
        });
        //var user = new ParseUser()
        //{
        //    Username = inputID.text,
        //    Password = inputPW.text,
        //    Email = inputID.text
        //};

        //Task signUpTask = user.SignUpAsync();
    }
    // Use this for initialization
    void Start()
    {
        ParseUser.EnableRevocableSessionAsync();

        strReasonOfLoginFailed = "";
        gLoginForm = GameObject.Find("LoginForm");
        gBTNshowLoginUI = GameObject.Find("BTNshowSignUI");
        gTXTerr = GameObject.Find("TXTerrorMsg");

        gInputID = GameObject.Find("EDT_ID");
        gInputPW = GameObject.Find("EDT_PW");
        gBTNsign = GameObject.Find("BTN_Login");
        inputID = gInputID.GetComponent<InputField>();
        inputPW = gInputPW.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        gLoginForm.SetActive(bLoginUIShow);

        if (strReasonOfLoginFailed != "")
            gTXTerr.GetComponent<Text>().text = strReasonOfLoginFailed;
    }


}
