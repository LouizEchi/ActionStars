using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		public InputField inputString;
		public Text textView;
		public Button compileButton;
		private bool buttonIsPressed;
		private List<string> compiledCode;
		private int originalCount;
		private List<float> timers;
		private bool cleared = true;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			compiledCode = new List<string> ();
			originalCount = 0;
			timers = new List<float> ();
         }


        private void Update()
        {

			if (!m_Jump) {
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}
//			if (compiledCode.Count > 0 && originalCount !) {
//				textView.text = "";
//				compiledCode.ForEach (text => { 
//					textView.text += text + "\n";
//				});
//			} else {
//
//
//			}



		}

		public void toggleCompile() {
			if (cleared == false) {
				var count = 0;
				var runOnce = false;
				compiledCode.ForEach (text => { 
					var parsedInput = parseFunctionString (text);
					if (parsedInput != null) {
						runOnce = false;
						while (timers[count] > 0) {
							if (!runOnce) {
								var parameters = getFunctionParameters (parsedInput [1]);
								m_Character.InvokeAsFunctionCall (parsedInput [0], parameters);
							}

							runOnce = true;
							timers [count] = timers [count] - 0.1f;
						}
					}
					count++;
				});
				textView.text = "";
				compiledCode.Clear ();
				cleared = true;
			}

		}

		IEnumerator runCodes() {
			yield return new WaitForSeconds(5);
		}

		public void storeCompile() {
			var parsedInput = parseFunctionString (inputString.text);
			if (parsedInput != null) {
				compiledCode.Add (inputString.text);
				timers.Add(20f);
				textView.text += inputString.text + "\n";
				inputString.text = "";
				cleared = false;
			}
		}

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            //m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }

		public string removeWhitespaces(string textContent) {
			char[] charsToTrim = { ' ', '\t' };
			return textContent.Trim (charsToTrim);
		}
		
		public object[] getFunctionParameters(string textContent) {
			bool isParsed;
			var count = 0;
			int result;
			float resultf;
			bool resultb;
			var parameters = textContent.Split (',');
			object[] returnValue = new object[parameters.Length];
			foreach (string param in parameters) {
				isParsed = false;
				
				if (int.TryParse(param, out result) && (!param.Contains("f") || !param.Contains("."))) {
					returnValue[count] = int.Parse(param);
					isParsed = true;
				}

				if (bool.TryParse(param, out resultb) && !isParsed) {
					returnValue[count] = bool.Parse(param);
					isParsed = true;
				}

				if (param.Contains("f") || param.Contains(".")) {
					//param = param.Replace('f', '');
					if (float.TryParse(param, out resultf) && !isParsed) {
						returnValue[count] = float.Parse(param);
						isParsed = true;
					}
				}
				
				//if string
				if (!isParsed) {
					returnValue[count] = param;
				}

				count++;
			}
			return returnValue;
		}
		
		public string[] parseFunctionString(string textContent) {
			var mainString = removeWhitespaces (textContent);
			char [] functionDelimeters = new char[2];
			functionDelimeters [0] = '(';
			functionDelimeters [1] = ')';
			var parameters = mainString.Split (functionDelimeters);

			if (parameters.Length > 2 && parameters[2] == ";") {
				return parameters;
			} else {
				return null;
			}
		}
    }
}