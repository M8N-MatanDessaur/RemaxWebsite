﻿using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RemaxWebsite
{
    public partial class message : System.Web.UI.Page
    {
        static int messageID;
        static int otherUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    DataRow connectedUser = (DataRow)Session["connectedUser"];
                    int userID = Convert.ToInt32(connectedUser["_id"]);
                    bool isAgent = Datasource.AgentExists(connectedUser["Email"].ToString());
                    messageID = Convert.ToInt32(Request.QueryString["id"]);
                    otherUser = Convert.ToInt32(Request.QueryString["contact"]);

                    DataTable messages = Datasource.GetAllMessages(userID, otherUser);

                    foreach (DataRow row in messages.Rows)
                    {
                        HtmlGenericControl messageContent = new HtmlGenericControl("p");
                        messageContent.InnerText = row["Content"].ToString();

                        bool messageIsAgent = (row["isAgent"].ToString() == "True")?true:false;

                        if (isAgent && messageIsAgent)
                        {
                            messageContent.Attributes.Add("class", "right");
                        }
                        else if (!isAgent && !messageIsAgent)
                        {
                            messageContent.Attributes.Add("class", "right");
                        }
                        else
                        {
                            messageContent.Attributes.Add("class", "left");
                        }

                        messages_view.Controls.Add(messageContent);
                    }
                }
            }
        }

        protected void btn_send_Click(object sender, EventArgs e)
        {
            string message = txt_messageToSend.Text;

            // Get the current user's ID
            DataRow connectedUser = (DataRow)Session["connectedUser"];
            int currentUserID = Convert.ToInt32(connectedUser["_id"]);

            // Determine the recipient's ID (client or agent)
            bool isAgent = Datasource.AgentExists(connectedUser["Email"].ToString());
 
            // Send the message
            Datasource.AddMessage(currentUserID, otherUser, message, isAgent);

            // Refresh the page to display the updated messages
            Response.Redirect($"message.aspx?id={Request.QueryString["id"]}&contact={otherUser}");
        }
    }
}
