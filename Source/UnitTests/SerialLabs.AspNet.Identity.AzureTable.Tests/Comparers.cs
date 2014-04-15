using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.UnitTestHelpers;
using System.Collections.Generic;

namespace SerialLabs.AspNet.Identity.AzureTable.Tests
{
    class Comparers
    {
        public static void AreEquals(IdentityUser expected, IdentityUser actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);
            if (expected == null) return;

            Assert.AreEqual(expected.ETag, actual.ETag);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.PartitionKey, actual.PartitionKey);
            Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
            Assert.AreEqual(expected.RowKey, actual.RowKey);
            Assert.AreEqual(expected.SecurityStamp, actual.SecurityStamp);
            Assert.AreEqual(expected.SerializedClaims, actual.SerializedClaims);
            Assert.AreEqual(expected.SerializedLogins, actual.SerializedLogins);
            Assert.AreEqual(expected.SerializedRoles, actual.SerializedRoles);
            CommonComparers.AreSimilar(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.UserName, actual.UserName);
            CommonComparers.AreCollectionEquals(expected.Logins, actual.Logins, AreEquals);
            CommonComparers.AreCollectionEquals(expected.Roles, actual.Roles, Assert.AreEqual);
            CommonComparers.AreCollectionEquals(expected.Claims, actual.Claims, AreEquals);
        }

        public static void AreEquals(IdentityUserLogin expected, IdentityUserLogin actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);
            if (expected == null) return;

            Assert.AreEqual(expected.ETag, actual.ETag);
            Assert.AreEqual(expected.PartitionKey, actual.PartitionKey);
            Assert.AreEqual(expected.RowKey, actual.RowKey);
            CommonComparers.AreSimilar(expected.Timestamp, actual.Timestamp);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.LoginProvider, actual.LoginProvider);
            Assert.AreEqual(expected.ProviderKey, actual.ProviderKey);
        }

        public static void AreEquals(IList<IdentityUserLogin> expected, IList<IdentityUserLogin> actual)
        {
            CommonComparers.AreCollectionEquals(expected, actual, AreEquals);
        }

        public static void AreEquals(UserLoginInfo expected, UserLoginInfo actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);
            if (expected == null) return;
            Assert.AreEqual(expected.LoginProvider, actual.LoginProvider);
            Assert.AreEqual(expected.ProviderKey, actual.ProviderKey);
        }
        public static void AreEquals(IList<UserLoginInfo> expected, IList<UserLoginInfo> actual)
        {
            CommonComparers.AreCollectionEquals(expected, actual, AreEquals);
        }

        public static void AreEquals(IdentityUserClaim expected, IdentityUserClaim actual)
        {
            CommonComparers.CheckNullReferences(expected, actual);

            Assert.AreEqual(expected.ClaimType, actual.ClaimType);
            Assert.AreEqual(expected.ClaimValue, actual.ClaimValue);
        }

        public static void AreEquals(IList<IdentityUserClaim> expected, IList<IdentityUserClaim> actual)
        {
            CommonComparers.AreCollectionEquals(expected, actual, AreEquals);
        }
    }
}
