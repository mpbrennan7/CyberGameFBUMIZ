using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
//using System.Data.SqlClient;

public class InputScript : MonoBehaviour
{
    
	public string uname;
	public string password;
	public GameObject inputBox;
	public GameObject textDisplay;
	public bool unameMode = true;
	
	// Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "What is your name?";
    }

    
	public void connectToDatabase(){
		string connetionString;
		SqlConnection cnn;
		connetionString = @"Data Source=localhost;Initial Catalog=fccgame;User ID=root;Password=";
		cnn = new SqlConnection(connetionString);
		return cnn;
		
		
		
		
	}
	
	
	public void storeNameAndPassword(){
		
		if(unameMode){
			uname = inputBox.GetComponent<Text>().text;
			textDisplay.GetComponent<Text>().text = "Welcome " + uname + " to the game.\nNow you need a strong password\nSOME PASSWORD GUIDELINES HERE"; 
			
			unameMode=false;
		}
		else{
			password = inputBox.GetComponent<Text>().text;
			string pwordStrengthText = checkStrength(password);
			textDisplay.GetComponent<Text>().text = pwordStrengthText;
			/*
			if(pwordStrengthText.equals("Great password!")){
				SqlConnection cnn = connectToDatabase();
				cnn.Open();
				SqlCommand command;
				SqlDataAdapter adapter = new SqlDataAdapter();
				String sql = "Insert into fccgame (uname, pword) values("+uname+","+pword+")";
				command = new SqlCommand(sql,cnn);
				adapter.InsertCommand = new SqlCommand(sql,cnn);
				adapter.InsertCommand.ExecuteNonQuery();
				command.Dispose();
				cnn.Close();
				
			}*/
		}
		
		
		//do anti sql injection stuff
		//write to db
	}
	
	
	public string checkStrength(string password){
		
	
		string result = "";
		int[] passwordStrengthScore = PasswordAdvisor.CheckStrength(password);
		
		bool strongEnough = false;
		for(int i =0; i<passwordStrengthScore.Length;i++){
			if(passwordStrengthScore[i] != 1){
				strongEnough = true;
			}
		}
		if(strongEnough){
			return "Great password!";
		}
		
		
		if(passwordStrengthScore[0] != 1){
			result += "Password cannot be blank\n";
		}	
		if(passwordStrengthScore[1] !=1){
			result +="Password must be longer than 8 characters\n";	
		}
		if(passwordStrengthScore[2] !=1){
			result +="Password must contain digits\n";	
		}
		if(passwordStrengthScore[3] !=1){
			result +="Password must contain uppercase and lowercase letters\n";	
		}
		if(passwordStrengthScore[3] !=1){
			result +="Password must contain special character(s)\n";	
		}
		
		return result;
	}
	
	
}


public class PasswordAdvisor
  {
    public static int[] CheckStrength(string password)
    {
      
	  int[] scoreArray = {1,1,0,0,0};
	  

	
	
	//blank
      if (password.Length < 1){
		scoreArray[0] = 0;
	  }
	  //length cant be < 8
      if (password.Length < 8){
        scoreArray[1] = 0;
	  }
	//digits
      //if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success){
		Debug.Log("Here " + Regex.Match(password, @"\d+").Success);
		if (Regex.IsMatch(password, @"\d")){
			
			scoreArray[2] = 1;
	  }
	//upper and lower case
      if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success &&
        Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success){
			
        scoreArray[3] = 1;
		}
		//special characters
      if (Regex.Match(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success){
        scoreArray[4] = 1;
	  }
      return scoreArray;
    }
  }