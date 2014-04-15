using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialLabs.AspNet.Identity.AzureTable.Tests
{
    [TestClass]
    public class UserStoreTest
    {
        public const string StorageConnectionString = "UseDevelopmentStorage=true";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreateAsync_WithNullUser_ShouldFail()
        {
            UserStore<IdentityUser> store = GetUserStore();
            await store.CreateAsync(null);
        }

        [TestMethod]
        public async Task CreateAsync_WithSuccess()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser user = CreateRandomUser();
            await store.CreateAsync(user);
        }

        [TestMethod]
        public async Task FindByIdAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser actual = await store.FindByIdAsync(expected.Id);

            Comparers.AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task FindByNameAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser actual = await store.FindByNameAsync(expected.UserName);

            Comparers.AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task DeleteAsync_WithSuccess()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser expected = CreateRandomUser();
            await store.CreateAsync(expected);
            await store.DeleteAsync(expected);
            // Check if user is deleted from store
            IdentityUser actual = await store.FindByIdAsync(expected.Id);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task UpdateAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();

            UserStore<IdentityUser> store = GetUserStore();

            IdentityUserClaim newClaim = CreateRandomUserClaim();
            user.Claims.Add(newClaim);
            await store.UpdateAsync(user);

            IdentityUser actual = await store.FindByIdAsync(user.Id);
            IdentityUserClaim updatedClaim = actual.Claims.FirstOrDefault(x => x.ClaimType == newClaim.ClaimType);
            Assert.IsNotNull(updatedClaim);
            Comparers.AreEquals(newClaim, updatedClaim);
        }

        [TestMethod]
        public async Task AddLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);
        }

        [TestMethod]
        public async Task FindAsync_WithSuccess()
        {
            IdentityUser expected = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(expected, newLoginInfo);

            IdentityUser actual = await store.FindAsync(newLoginInfo);

            Assert.IsNotNull(actual);
            Comparers.AreEquals(expected, actual);
        }

        [TestMethod]
        public async Task GetLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);

            IList<UserLoginInfo> logins = await store.GetLoginsAsync(user);
            Assert.IsNotNull(logins);
            Assert.IsNotNull(logins.FirstOrDefault(x => x.LoginProvider == newLoginInfo.LoginProvider && x.ProviderKey == newLoginInfo.ProviderKey));
        }

        [TestMethod]
        public async Task RemoveLoginAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            UserLoginInfo newLoginInfo = CreateRandomUserLoginInfo();
            await store.AddLoginAsync(user, newLoginInfo);
            await store.RemoveLoginAsync(user, newLoginInfo);

            IdentityUser actual = await store.FindAsync(newLoginInfo);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task AddToRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            await store.AddToRoleAsync(user, Guid.NewGuid().ToString());
        }

        [TestMethod]
        public async Task GetRolesAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            await store.AddToRoleAsync(user, Guid.NewGuid().ToString());

            IList<string> roles = await store.GetRolesAsync(user);
            CollectionAssert.AreEqual(user.Roles.ToArray(), roles.ToArray());
        }

        [TestMethod]
        public async Task IsInRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string newRoleName = Guid.NewGuid().ToString();
            await store.AddToRoleAsync(user, newRoleName);

            bool isInRole = await store.IsInRoleAsync(user, newRoleName);
            Assert.IsTrue(isInRole);
        }

        [TestMethod]
        public async Task RemoveFromRoleAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string newRoleName = Guid.NewGuid().ToString();
            await store.AddToRoleAsync(user, newRoleName);
            await store.RemoveFromRoleAsync(user, newRoleName);

            IdentityUser actual = await store.FindByNameAsync(user.UserName);
            Assert.IsFalse(user.Roles.Contains(newRoleName));
        }

        [TestMethod]
        public async Task GetPasswordHashAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            string passwordHash = await store.GetPasswordHashAsync(user);
            Assert.AreEqual(user.PasswordHash, passwordHash);
        }

        [TestMethod]
        public async Task HasPasswordAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            bool hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsFalse(hasPasswordHash);
            user.PasswordHash = CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString());
            hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsTrue(hasPasswordHash);
        }

        [TestMethod]
        public async Task SetPasswordHashAsync_WithSuccess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();

            bool hasPasswordHash = await store.HasPasswordAsync(user);
            Assert.IsFalse(hasPasswordHash);

            string passwordHash = CryptographyHelper.ComputeSHA256Hash(Guid.NewGuid().ToString());
            await store.SetPasswordHashAsync(user, passwordHash);
        }

        [TestMethod]
        public async Task SetEmail_WithSucess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();
            string expected = "unitTest@email.com";
            await store.SetEmailAsync(user, expected);
            Assert.IsNotNull(user.Email);
            Assert.AreEqual(expected, user.Email);

        }

        [TestMethod]
        public async Task GetEmail_WithSucess()
        {

            IdentityUser user = await CreateAndPersistUserWithEmailAsync("unitTest@email.com");
            UserStore<IdentityUser> store = GetUserStore();
            string expected = "unitTest@email.com";
            string existing = await store.GetEmailAsync(user);
            Assert.AreEqual(existing, expected);
        }

        [TestMethod]
        public async Task SetConfirmed_WithSucess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();
            await store.SetEmailConfirmedAsync(user, true);
            Assert.IsTrue(user.IsVerified);
        }

        [TestMethod]
        public async Task GetConfirmed_WithSucess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();
            user.IsVerified = true;
            Assert.IsTrue(await store.GetEmailConfirmedAsync(user));
        }

        [TestMethod]
        public async Task SetTowFactorEnabled_WithSucess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();
            await store.SetTwoFactorEnabledAsync(user, true);
            Assert.IsTrue(user.TwoFactorEnabled);
        }

        [TestMethod]
        public async Task GetTowFactorEnabled_WithSucess()
        {
            IdentityUser user = await CreateAndPersistRandomUserAsync();
            UserStore<IdentityUser> store = GetUserStore();
            user.TwoFactorEnabled = true;
            Assert.IsTrue(await store.GetTwoFactorEnabledAsync(user));
        }


        #region Utilities
        static CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.DevelopmentStorageAccount;
        }
        static UserPartitionKeyResolver GetPartitionKeyResolver()
        {
            return new UserPartitionKeyResolver(Guid.Empty.ToString());
        }
        static UserStore<IdentityUser> GetUserStore()
        {
            return new UserStore<IdentityUser>("UseDevelopmentStorage=true", GetPartitionKeyResolver());
        }
        static IdentityUser CreateRandomUser()
        {
            string userName = Guid.NewGuid().ToString();

            return new IdentityUser
            {
                UserName = userName,
                Claims = new List<IdentityUserClaim> { CreateRandomUserClaim() },
                Logins = new List<IdentityUserLogin> { 
                    new IdentityUserLogin(userName, CreateRandomUserLoginInfo())
                },
                Roles = new List<string> { "Administrator", "User" }
            };
        }
        static UserLoginInfo CreateRandomUserLoginInfo()
        {
            return new UserLoginInfo(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
        static IdentityUserClaim CreateRandomUserClaim()
        {
            return new IdentityUserClaim(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }
        static async Task<IdentityUser> CreateAndPersistRandomUserAsync()
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser user = CreateRandomUser();
            await store.CreateAsync(user);
            return user;
        }
        static async Task<IdentityUser> CreateAndPersistUserWithEmailAsync(String email)
        {
            UserStore<IdentityUser> store = GetUserStore();
            IdentityUser user = CreateRandomUser();
            user.Email = email;
            await store.CreateAsync(user);
            return user;
        }


        //static void AreSimilar(DateTimeOffset expected, DateTimeOffset actual)
        //{
        //    if (expected == DateTimeOffset.MinValue && actual != DateTimeOffset.MinValue)
        //        Assert.Fail("Expected is not set while actual has a value");
        //    if (expected != DateTimeOffset.MinValue && actual == DateTimeOffset.MinValue)
        //        Assert.Fail("Expected has a value while actual is not set");
        //    string dateFormat = "yyyy/MM/dd HH:mm:ss";
        //    Assert.AreEqual(expected.ToString(dateFormat), actual.ToString(dateFormat));
        //}
        //static void AreCollectionEquals<T>(IEnumerable<T> expected, IEnumerable<T> actual, Action<T, T> comparer)
        //{
        //    CheckNullReferences(expected, actual);
        //    if (expected == null)
        //        return;
        //    Assert.AreEqual(expected.Count(), actual.Count());
        //    for (int i = 0; i < expected.Count(); i++)
        //    {
        //        comparer(expected.ElementAt(i), actual.ElementAt(i));
        //    }
        //}

        //static void CheckNullReferences<T>(T expected, T actual) where T : class
        //{
        //    if (expected == null && actual != null)
        //        Assert.Fail("Expected is null, actual is not null");
        //    if (expected != null && actual == null)
        //        Assert.Fail("Expected is not null, actual is null");
        //}
        #endregion
    }
}
