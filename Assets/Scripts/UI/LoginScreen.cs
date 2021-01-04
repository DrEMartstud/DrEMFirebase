using System.Collections;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Networking;

namespace UI {

    public class LoginScreen : MonoBehaviour {
        
        [SerializeField]
		private InputField _loginInputField;

		[SerializeField]
		private InputField _passwordInputField;

		[SerializeField]
		private Button _loginButton;

		[SerializeField]
		private Button _registerButton;
		
		[SerializeField]
		private ScriptableStringValue _parsedLogin;
        
		[SerializeField]
		private ScriptableStringValue _parsedPassword;
		
		[SerializeField]
		private ScriptableStringValue _errorMessageStringValue;
		
		[SerializeField]
		private Text _errorMessageLabel;
		//
		// [SerializeField] 
		// private string _url;

		[SerializeField] 
		private EventDispatcher _loginEventDispatcher;
		
		[SerializeField] 
		private EventDispatcher _goToregistrationEventDispatcher;
		
		[SerializeField] 
		private EventListener _unsuccessfulLogInEventListener;
		
		// private WWWForm _form;

		private void Start() {
			_loginButton.onClick.AddListener(OnLoginButtonClick);
			_registerButton.onClick.AddListener(OnRegisterButtonClick);
		}

		private void OnEnable() {
			_unsuccessfulLogInEventListener.OnEventHappened += UnsuccessfulLogin;
			ResetScreen();
		}

		private void OnDisable() {
			_unsuccessfulLogInEventListener.OnEventHappened -= UnsuccessfulLogin;
		}

		private void ResetScreen() {
			_parsedPassword.value = "";
			_parsedLogin.value = "";
			_loginButton.interactable = true;
			_registerButton.interactable = true;
			_errorMessageLabel.text = "";
			_loginInputField.text = "";
			_passwordInputField.text = "";
		}
		
		private void OnLoginButtonClick() {
			_errorMessageLabel.text = "";
			_loginButton.interactable = false;
			_parsedLogin.value = _loginInputField.text.ToLower();
			_parsedPassword.value = _passwordInputField.text;
			_loginEventDispatcher.Dispatch();
			// StartCoroutine(Login());
		}

		private void OnRegisterButtonClick() {
			_goToregistrationEventDispatcher.Dispatch();
		}

		private void UnsuccessfulLogin() {
			_errorMessageLabel.text = _errorMessageStringValue.value;
			_loginButton.interactable = true;
		}
		
		// private IEnumerator Login() {
		// 	_errorMessageLabel.text = "";
		// 	_form = new WWWForm();
		// 	_form.AddField("username", _loginInputField.text);
		// 	_form.AddField("password", _passwordInputField.text);
		// 	
		// 	WWW request = new WWW(_url, _form);
		// 	
		// 	yield return request;
		// 	Debug.Log(request.error);
		// 	
		//
		// 	if (request.error != null) {
		// 		_errorMessageLabel.text = "404 not found!";
		// 	}
		// 	else {
		// 		if (request.isDone) {
		// 			if (request.text.Contains("error")) {
		// 				_errorMessageLabel.text = "Invalid username or password!";
		// 			}
		// 			else {
		// 				_loginEventDispatcher.Dispatch();
		// 				_errorMessageLabel.text = _loginInputField.text;
		// 			}
		// 		}
		// 	}
		// 	Debug.Log(request.text);
		// 	_loginButton.interactable = true;
		// 	request.Dispose();
		// }
    }
}