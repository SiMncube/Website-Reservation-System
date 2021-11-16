﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M4_major_project
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private string firstName = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
        private bool userExist()
        {
            FullDataSet fullDs = new FullDataSet();
            FullDataSetTableAdapters.CustomerTableAdapter taCustomer = new FullDataSetTableAdapters.CustomerTableAdapter();
            taCustomer.Fill(fullDs.Customer);
            string rand = randomOTP();
            string temp = "";
            for (int i = 0; i < rand.Length; i++)
            {
                if (rand[i] != ' ')
                    temp += rand[i];
            }
            CurrentReset.setOtpString(temp);
            for (int i = 0; i < fullDs.Customer.Rows.Count; i++)
            {
                if (fullDs.Customer[i].emailID.Equals(emailTextbox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    CurrentReset.setEmailID(fullDs.Customer[i].emailID);
                    firstName = fullDs.Customer[i].name;
                    Email.sendEmail(CurrentReset.getEmailID(),"Reset password OTP confirmation",htmlOTP(rand));
                    return true;
                }
            }
            return false;
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (userExist())
                Response.Redirect("~/ChangePassword");
            else
                Response.Write("<script language='javascript'>window.alert('You have entered an invalid email address');window.location='ForgotPassword.aspx';</script>");
        }
        private string htmlOTP(string temp)
        {
            string body = @"<html>
                           <body>";
            body += "<p>Dear " + firstName + ",</p><p>There was a request to change your password!!</p><p>If you did not make this request then please ignore this email</p>";
            body += "<p> Otherwise, please use the One Time Pin(OTP) below to change your password<br>";
            body += "Here's your One Time Pin(OTP) : <strong>" + temp + "</strong></p>";
            body += "<p>This is an autogenerated email, for enquiries<br>call: +27 64 090 3388<br>Or email: sonya@TheCottageBnB.co.za</p>";
            body += " </body></html>";
            return body;
        }
        public string randomOTP()
        {
            Random r = new Random();
            int randNum = r.Next(1000000);
            string temp = randNum.ToString("D6");
            string random = null;
            for (int i = 0; i < 6; i++)
                random += temp[i] + " ";
            return random.Substring(0, 11);
        }
    }
}