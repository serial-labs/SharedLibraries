﻿using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SerialLabs.Identity.CloudStorage
{
    /// <summary>
    /// A table storage implementation of <see cref="IUser"/>
    /// </summary>
    public class IdentityUser : TableEntity, IUser
    {
        // Keeping it simple and mapping IUser.Id to IUser.UserName.
        // This allows easy, efficient lookup by setting this to be the rowkey in table storage
        public string Id { get { return UserName; } }

        /// <summary>
        /// Username is rowkey
        /// </summary>
        public string UserName
        {
            get
            {
                return RowKey;
            }
            set
            {
                RowKey = value;
            }
        }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        #region Logins
        private IList<IdentityUserLogin> _logins;
        /// <summary>
        /// Serialization pivot for <see cref="IList{IdentityUserLogin}"/> (logins)
        /// </summary>
        public string SerializedLogins
        {
            get
            {
                if (_logins == null || _logins.Count == 0)
                    return null;
                return JsonConvert.SerializeObject(Logins);
            }
            set
            {
                _logins = JsonConvert.DeserializeObject<IList<IdentityUserLogin>>(value) ?? new List<IdentityUserLogin>();
            }
        }
        /// <summary>
        /// User Logins
        /// </summary>
        [IgnoreProperty]
        public IList<IdentityUserLogin> Logins
        {
            get
            {
                _logins = _logins ?? new List<IdentityUserLogin>();
                return _logins;
            }
            set
            {
                _logins = value;
            }
        }
        #endregion

        #region Roles
        private IList<string> _roles;
        /// <summary>
        /// Serialization pivot for <see cref="IList{string}"/> (roles) 
        /// </summary>
        public string SerializedRoles
        {
            get
            {
                if (_roles == null || _roles.Count == 0)
                    return null;
                return JsonConvert.SerializeObject(Roles);
            }
            set
            {
                _roles = JsonConvert.DeserializeObject<IList<string>>(value) ?? new List<string>();
            }
        }

        [IgnoreProperty]
        public IList<string> Roles
        {
            get
            {
                _roles = _roles ?? new List<string>();
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }
        #endregion

        #region Claims
        private IList<IdentityUserClaim> _claims;
        /// <summary>
        /// Serialization pivot for <see cref="IList{IdentityUserClaim}"/> (claims)
        /// </summary>
        public string SerializedClaims
        {
            get
            {
                if (_claims == null || _claims.Count == 0)
                    return null;
                return JsonConvert.SerializeObject(Claims);
            }
            set
            {
                _claims = JsonConvert.DeserializeObject<IList<IdentityUserClaim>>(value) ?? new List<IdentityUserClaim>();
            }
        }
        /// <summary>
        /// User Claims
        /// </summary>
        [IgnoreProperty]
        public IList<IdentityUserClaim> Claims
        {
            get
            {
                _claims = _claims ?? new List<IdentityUserClaim>();
                return _claims;
            }
            set
            {
                _claims = value;
            }
        }
        #endregion
    }
}