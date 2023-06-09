﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemaxWebsite
{
    public partial class agentLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckUserConnected();
            }
        }

        //Checks if user is connected in Session. if it is the case display the user's name
        protected void CheckUserConnected()
        {
            string connectedUserValid = (Session["connectedUser"] != null && !string.IsNullOrEmpty(Session["connectedUser"].ToString()))
                ? Session["connectedUserName"].ToString()
                : "sign in";
            connectedUser.Controls.Add(new LiteralControl(connectedUserValid));
        }

        protected void BTN_Login_Click(object sender, EventArgs e)
        {
            string email = TXT_Email.Text;
            string password = TXT_Password.Text;

            // Call the login method from Datasource to check the user credentials
            bool loginSuccessful = Datasource.AgentLogin(email, password);

            if (loginSuccessful)
            {
                // User login successful, perform further actions or redirect to another page
                // and initialise connectedUser on session
                string firstName = Datasource.GetAgentNameByEmail(email)["FirstName"].ToString();
                string lastName = Datasource.GetAgentNameByEmail(email)["LastName"].ToString();

                Session["connectedUser"] = Datasource.GetAgentNameByEmail(email);
                Session["connectedUserName"] = firstName + " " + lastName;
                Response.Redirect("index.aspx");
            }
            else
            {
                // User login failed, display an error message or take appropriate action
                // For example, displaying an error message on the page:
                ErrorLabel.Text = "Invalid email or password. Please try again.";
            }
        }
    }
}