using System;
using Entity;
using System.Web;

namespace MvcUtilitys
{
    //is the user authorize or not login
    public enum UserAuth { noLogin, no, yes }

    //session extensions
    public static class SessionExtensions
    {
        //return the user authorize state 
        public static UserAuth VerifyPremission(this HttpSessionStateBase session, Premission premission)
        {
            if (session["user"] != null)
            {
                return ((UserInfo)session["user"]).Premission >= premission ?
                    UserAuth.yes : UserAuth.no;
            }
            return UserAuth.noLogin;
        }

        //verify in the user have premission to get to the content
        public static bool IsPremited(this HttpSessionStateBase session, Premission premission)
        {
            return session.VerifyPremission(premission) == UserAuth.yes;
        }

        //get the user id
        public static string GetUserID(this HttpSessionStateBase session)
        {
            if (session["user"] != null)
                return ((UserInfo)session["user"]).ID;
            return string.Empty;
        }

        /// <summary>
        /// Add User Info To The Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="info"></param>
        public static void SetUserData(this HttpSessionStateBase session, UserInfo info)
        {
            session["user"] = info;
        }

        /// <summary>
        /// Removes User Info From The Session
        /// </summary>
        /// <param name="session"></param>
        public static void LogOutUser(this HttpSessionStateBase session)
        {
            session.Remove("user");
        }

        /// <summary>
        /// Add New Order Object To The Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="registrationPlate"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool SetOrder(this HttpSessionStateBase session, string registrationPlate, DateTime start, DateTime end)
        {
            using (CarsData db = new CarsData())
            {
                bool isValid = db.IsDateValid(start, end, registrationPlate);
                string userId = session.GetUserID();
                if (isValid && userId != string.Empty)
                {
                    session["order"] = new Order()
                    {
                        startdate = start,
                        enddate = end,
                        carid = registrationPlate,
                        userid = userId
                    };
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Verify The Payment And Add The Order To The DataBase
        /// </summary>
        /// <param name="session"></param>
        /// <param name="creditCard"></param>
        /// <returns></returns>
        public static bool SetOrderToDatabase(this HttpSessionStateBase session, int creditCard)
        {
            if (creditCard != null && session["order"] != null) // check credit card
            {
                using (OrdersData db = new OrdersData())
                {
                    Order order = (Order)session["order"];
                    db.Add(order);
                    session.Remove("order");
                    return true;
                }
            }
            return false;
        }
    }
}