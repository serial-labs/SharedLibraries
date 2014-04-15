using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;
using SerialLabs.Data.AzureTable;
using SerialLabs.Data.AzureTable.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SerialLabs.AspNet.Identity.AzureTable
{

    public class UserStore<TUser> :
        IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserLoginStore<TUser>,
        IUserRoleStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IUserClaimStore<TUser>,
        IUserEmailStore<TUser>
        //IUserTwoFactorStore<TUser, TKey>
        where TUser : IdentityUser, new()
    {
        private bool _disposed = false;
        private readonly TableStorageWriter _userTableWriter;
        private readonly TableStorageReader _userTableReader;
        private readonly TableStorageWriter _loginTableWriter;
        private readonly TableStorageReader _loginTableReader;
        private readonly string _storageConnectionString;
        private readonly IPartitionKeyResolver<string> _partitionKeyResolver;

        public UserStore(string storageConnectionString)
            : this(storageConnectionString, new UserPartitionKeyResolver())
        { }
        public UserStore(string storageConnectionString, IPartitionKeyResolver<string> partitionKeyResolver)
        {
            Guard.ArgumentNotNull(storageConnectionString, "storageConnectionString");
            Guard.ArgumentNotNull(partitionKeyResolver, "partitionKeyResolver");

            _storageConnectionString = storageConnectionString;
            _partitionKeyResolver = partitionKeyResolver;

            _userTableWriter = new TableStorageWriter("Users", storageConnectionString);
            _userTableReader = new TableStorageReader("Users", storageConnectionString);

            _loginTableWriter = new TableStorageWriter("Logins", storageConnectionString);
            _loginTableReader = new TableStorageReader("Logins", storageConnectionString);
        }


        #region IUserStore
        public async Task CreateAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.PartitionKey = _partitionKeyResolver.Resolve(user.Id);

            _userTableWriter.Insert(user);
            await _userTableWriter.ExecuteAsync();
        }

        public async Task UpdateAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            var partitionKey = _partitionKeyResolver.Resolve(user.Id);
            user.PartitionKey = partitionKey;
            await UpdateUser(user);
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            Guard.ArgumentNotNullOrWhiteSpace(userId, "userId");

            var partitionKey = _partitionKeyResolver.Resolve(userId);
            var result = await _userTableReader.ExecuteAsync<TUser>(new EntryForPartitionAndKey<TUser>(partitionKey, userId));
            if (result == null)
                return null;
            return result.FirstOrDefault();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            Guard.ArgumentNotNullOrWhiteSpace(userName, "userName");

            return FindByIdAsync(userName);
        }

        public async Task DeleteAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            user.ETag = "*";
            _userTableWriter.Delete(user);
            await _userTableWriter.ExecuteAsync();
        }

        #endregion

        #region IUserLoginStore
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            user.Logins.Add(new IdentityUserLogin(user.Id, login));
            await UpdateUser(user);

            _loginTableWriter.Insert(new IdentityUserLogin(user.Id, login));
            await _loginTableWriter.ExecuteAsync();
        }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            Guard.ArgumentNotNull(login, "login");

            var lie = new IdentityUserLogin("", login);
            var result = await _loginTableReader.ExecuteAsync(
                new EntryForPartitionAndKey<IdentityUserLogin>(lie.PartitionKey, lie.RowKey));
            if (result == null || result.Count == 0)
                return null;
            return await FindByIdAsync(result.FirstOrDefault().UserId);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<IList<UserLoginInfo>>(user.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(login, "login");

            IdentityUserLogin identityLogin = user.Logins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            if (identityLogin != null)
            {
                user.Logins.Remove(identityLogin);
                await UpdateUser(user);

                _loginTableWriter.Delete(new IdentityUserLogin(user.Id, login) { ETag = "*" });
                await _loginTableWriter.ExecuteAsync();
            }
        }
        #endregion

        #region IUserRoleStore
        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            if (!user.Roles.Contains(roleName))
            {
                user.Roles.Add(roleName);
                await UpdateUser(user);
            }
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult(user.Roles);
        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            return Task.FromResult(user.Roles.Contains(roleName));
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNullOrWhiteSpace(roleName, "roleName");

            if (user.Roles.Remove(roleName))
            {
                await UpdateUser(user);
            }
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(!String.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            Guard.ArgumentNotNull(user, "user");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            Guard.ArgumentNotNull(user, "user");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            user.Claims.Add(new IdentityUserClaim(claim));
            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<IList<Claim>>(user.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(claim, "claim");

            IdentityUserClaim userClaim = user.Claims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            if (userClaim != null)
            {
                user.Claims.Remove(userClaim);
            }

            return Task.FromResult(0);
        }
        #endregion

        #region IUserEmailStore
        public Task<TUser> FindByEmailAsync(string email)
        {
            Guard.ArgumentNotNull(email, "email");
            var partitionKey = _partitionKeyResolver.Resolve(email);
            var operation = TableOperation.Retrieve<TUser>(partitionKey, email);
            //Not Completed yet//
            return null;
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");
            return Task.FromResult(user.IsVerified);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            Guard.ArgumentNotNull(user, "user");
            Guard.ArgumentNotNull(email, "email");
            user.Email = email;

            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            Guard.ArgumentNotNull(user, "user");
            user.IsVerified = confirmed;
            return Task.FromResult(0);

        }

        #endregion

        #region TwoFactorEnabled

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            Guard.ArgumentNotNull(user, "user");

            return Task.FromResult<bool>(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            Guard.ArgumentNotNull(user, "user");
            user.TwoFactorEnabled = enabled;
            return Task.FromResult<int>(0);
        }
        #endregion

        private async Task UpdateUser(TUser user)
        {
            _userTableWriter.Replace(user);
            await _userTableWriter.ExecuteAsync();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Free managed
            }
            _disposed = true;
        }
        #endregion
    }
}