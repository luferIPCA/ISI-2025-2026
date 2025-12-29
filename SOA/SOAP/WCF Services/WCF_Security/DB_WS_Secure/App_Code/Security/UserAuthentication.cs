/*
 * Auxiliar class for security
 * lufer
 */
using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;  //to get UserNamePasswordValidator


    public class UserAuthentication : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {

            //or just
            // validate arguments
            //if (string.IsNullOrEmpty(userName))
            //    throw new ArgumentNullException("userName");
            //if (string.IsNullOrEmpty(password))
            //    throw new ArgumentNullException("password");
            try
            {
                if (userName == "test" && password == "test123")
                {
                    Console.WriteLine("Authentic User");
                }
            }
            catch (Exception ex)
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
