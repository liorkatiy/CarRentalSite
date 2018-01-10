using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum Premission { User, Employee, Manger, Karpas }
    public enum Gender { Male, Female, Other }
    /// <summary>
    /// contains the user id and hes premission
    /// </summary>
    public class UserInfo
    {
        public Premission Premission { get; internal set; }
        public string ID { get; internal set; }
    }

    public class UsersData : DataBase<YoRentEntities, User>
    {
        /// <summary>
        /// get all the user Active Order search by ID Card ||Full Name || User Name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idCard"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public T[] GetActiveOrders<T>(string search, Func<Order, T> select)
        {
            User u = db.Users.FirstOrDefault(_user =>
            _user.idcard == search ||
            _user.fullname == search ||
            _user.username == search);
            if (u != null)
            {
                return u.Orders
                    .Where(order => order.carid != null && order.returndate == null)
                    .Select(select).ToArray();
            }
            return new T[0];
        }

        /// <summary>
        /// get all the user order get the user by the user id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userid"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public T[] GetOrdersByID<T>(string userid, Func<Order, T> select)
        {
            User user = db.Users.FirstOrDefault(_user => _user.id == userid);
            if (user != null)
            {
                return user.Orders
                    .Where(order=>order.carid!=null)
                    .Select(select).ToArray();
            }
            return new T[0];
        }

        /// <summary>
        /// check if the username is in the database
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Contains(string userName)
        {
            return db.Users.Any(user => user.username == userName);
        }

        /// <summary>
        /// check if the idcard already in the database
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public bool ContainsByIdCard(string IdCard)
        {
            return db.Users.Any(user => user.idcard == IdCard);
        }
        /// <summary>
        /// add the user to the database
        /// will turn the password into hash and salt
        /// default permission to 0
        /// and random guid
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Register(User user)
        {
            var passwordAndHash = PasswordHash.Create(user.password);
            user.password = passwordAndHash.Hash;
            user.salt = passwordAndHash.Salt;
            user.permission = 0;
            user.id = Guid.NewGuid().ToString();
            return Add(user);
        }

        /// <summary>
        /// check if the username annd password match
        /// outs the info if match found
        /// </summary>
        /// <param name="password"></param>
        /// <param name="username"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool VerifyUser(string password, string username, out UserInfo info)
        {
            info = null;
            var user = db.Users.FirstOrDefault(_user => _user.username == username);
            if (user != null && Verify(user, password))
            {
                info = new UserInfo() { Premission = (Premission)user.permission, ID = user.id };
                return true;
            }
            return false;
        }

        bool Verify(User user, string password)
        {
            return PasswordHash.Verify(password, user.salt, user.password);
        }

        //what to update in the user
        protected override bool Update(User user, User updatedUser)
        {
            if (updatedUser.password != null)
            {
                var hashPass = PasswordHash.Create(updatedUser.password);
                user.password = hashPass.Hash;
                user.salt = hashPass.Salt;
            }

            bool userNameInUse = db.Users.Any(_user =>
            (updatedUser.username == _user.username ||
            updatedUser.idcard == _user.idcard) && 
            _user.id != user.id);

            if (userNameInUse)
                return false;
            user.username = updatedUser.username;
            user.fullname = updatedUser.fullname;
            user.idcard = updatedUser.idcard;
            user.birthdate = updatedUser.birthdate;
            user.email = updatedUser.email;
            user.permission = updatedUser.permission;       
            user.sex = updatedUser.sex;
            return true;
        }

        //search filter
        protected override Expression<Func<User, bool>> MySearch(string[] search)
        {
            return user =>
            search.Any(word =>
            user.email.Contains(word) ||
            user.fullname.Contains(word) ||
            user.idcard.Contains(word) ||
            user.username.Contains(word));
        }

        //function to be called before deleting
        protected override void Delete(User user)
        {
            foreach (var order in user.Orders)
            {
                order.userid = null;
            }
        }
    }
}
