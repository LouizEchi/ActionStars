using System.Collections;
using System;

public class SyntaxChecker {
	public string removeWhitespaces(string textContent) {
		char[] charsToTrim = { ' ', '\t' };
		return textContent.Trim (charsToTrim);
	}
	
	public object[] getFunctionParameters(string textContent) {
		var count = 0;
		int result;
		float resultf;
		bool isParsed;
		var splitedString =  parseFunctionString(textContent);
		var parameters = splitedString[1].Split (',');
		object[] returnValue = new object[parameters.Length];
		foreach (string param in parameters) {
			isParsed = false;

			if (int.TryParse(param, out result)) {
				returnValue[count] = int.Parse(param);
				isParsed = true;
			}

			if (float.TryParse(param, out resultf) && !isParsed) {
				returnValue[count] = int.Parse(param);
				isParsed = true;
			}

			//if string
			if (!isParsed) {
				returnValue[count] = int.Parse(param);
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
		return mainString.Split(functionDelimeters);
	}

}