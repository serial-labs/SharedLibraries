using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SerialLabs
{
    /// <summary>
    /// Provides a standard base class for facilitating comparison of objects
    /// <remarks>http://www.sharparchitecture.net/</remarks>
    /// </summary>
    [SerializableAttribute]
    [DataContractAttribute(Name = "BaseObject")]
    public abstract class BaseObject : ICloneable
    {
        #region Members
        /// <summary>
        /// To help ensure hashcode uniqueness, a carefully selected random number multiplier 
        /// is used within the calculation.  Goodrich and Tamassia's Data Structures and
        /// Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
        /// of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        /// for more information.
        /// </summary>
        private const int HashMultiplier = 31;

        /// <summary>
        /// This static member caches the domain signature properties to avoid looking them up for 
        /// each instance of the same type.
        /// 
        /// A description of the very slick ThreadStatic attribute may be found at 
        /// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2005/08/18/132026.aspx
        /// </summary>
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<PropertyInfo>> signaturePropertiesDictionary;
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns true if given object is equals to current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseObject;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            return compareTo != null && this.GetType().Equals(compareTo.GetTypeUnproxied()) &&
                   this.HasSameObjectSignatureAs(compareTo);
        }
        /// <summary>
        /// This is used to provide the hashcode identifier of an object using the signature 
        /// properties of the object; although it's necessary for NHibernate's use, this can 
        /// also be useful for business logic purposes and has been included in this base 
        /// class, accordingly.  Since it is recommended that GetHashCode change infrequently, 
        /// if at all, in an object's lifetime, it's important that properties are carefully
        /// selected which truly represent the signature of an object.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var signatureProperties = this.GetSignatureProperties();

                // It's possible for two objects to return the same hash code based on 
                // identically valued properties, even if they're of two different types, 
                // so we include the object's type in the hash calculation
                var hashCode = this.GetType().GetHashCode();

                hashCode = signatureProperties.Select(property => property.GetValue(this, null))
                                              .Where(value => value != null)
                                              .Aggregate(hashCode, (current, value) => (current * HashMultiplier) ^ value.GetHashCode());

                if (signatureProperties.Any())
                {
                    return hashCode;
                }

                // If no properties were flagged as being part of the signature of the object,
                // then simply return the hashcode of the base object as the hashcode.
                return base.GetHashCode();
            }
        }
        /// <summary>
        /// Gets signature properties
        /// </summary>
        public virtual IEnumerable<PropertyInfo> GetSignatureProperties()
        {
            IEnumerable<PropertyInfo> properties;

            // Init the signaturePropertiesDictionary here due to reasons described at 
            // http://drdobbs.com/windowsWebLog/chris.taylor/archive/2005/08/18/132026.aspx
            if (signaturePropertiesDictionary == null)
            {
                signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
            }

            if (signaturePropertiesDictionary.TryGetValue(this.GetType(), out properties))
            {
                return properties;
            }

            return signaturePropertiesDictionary[this.GetType()] = this.GetTypeSpecificSignatureProperties();
        }

        /// <summary>
        /// You may override this method to provide your own comparison routine.
        /// </summary>
        public virtual bool HasSameObjectSignatureAs(BaseObject compareTo)
        {
            var signatureProperties = this.GetSignatureProperties();

            if ((from property in signatureProperties
                 let valueOfThisObject = property.GetValue(this, null)
                 let valueToCompareTo = property.GetValue(compareTo, null)
                 where valueOfThisObject != null || valueToCompareTo != null
                 where (valueOfThisObject == null ^ valueToCompareTo == null) || (!valueOfThisObject.Equals(valueToCompareTo))
                 select valueOfThisObject).Any())
            {
                return false;
            }

            // If we've gotten this far and signature properties were found, then we can
            // assume that everything matched; otherwise, if there were no signature 
            // properties, then simply return the default bahavior of Equals
            return signatureProperties.Any() || base.Equals(compareTo);
        }

        /// <summary>
        /// Create a shallow copy of the current object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Enforces the template method pattern to have child objects determine which specific 
        /// properties should and should not be included in the object signature comparison. Note
        /// that the the BaseObject already takes care of performance caching, so this method 
        /// shouldn't worry about caching...just return the goods man!
        /// </summary>
        protected abstract IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties();

        /// <summary>
        /// This wrapper burrows into the proxied object to get its actual type.
        /// </summary>
        /// <returns></returns>
        protected virtual Type GetTypeUnproxied()
        {
            return this.GetType();
        }
        #endregion
    }
}
