#C# Coding Guidelines

## Definitions
* [Camel case](http://en.wikipedia.org/wiki/CamelCase) is a casing convention where the first letter is lower-case, words are not separated by any character but have their first letter capitalized. Example: *thisIsCamelCased*.
* [Pascal case](http://c2.com/cgi/wiki?PascalCase) is a casing convention where the first letter of each word is capitalized, and no separating character is included between words. Example: *ThisIsPascalCased*.

##C# coding conventions

The serial-labs team uses [Allman bracing style](http://en.wikipedia.org/wiki/Indent_style#Allman_style). We are using the C# coding conventions described in this document: [C# Coding Guidelines](http://blogs.msdn.com/b/brada/archive/2005/01/26/361363.aspx) with the following exceptions:

* Private fields are prefixed with an underscore and camel-cased.
* Each file should not start with a copyright notice. The ones at the root of the source tree will suffice.
* using statements are on top of a file (outside of namespace {...})
* Use var only if you have an anonymous type or you can clearly tell what the type is from the right hand side of the expression (see examples below).

Examples:

``` csharp
// This is ok
var tuple = new { Name = "John", Age = 50 }; 

// This is ok
var stream = new MemoryStream();

// This is ok
var product = (Product)GetProduct();

// This is NOT ok
var values = GetProducts();
```

Here is some sample code that follows these conventions.

``` csharp
using System;
namespace NuGet
{
    public class ClassName
    {
        private List<SomeType> _privateMember;

        public List<SomeType> SomeProperty
        {
            get
            {
                return _privateMember;
            }
        }

        public string SomeAutoProperty { get; set; }

        public string SomeMethod(bool someCondition)
        {
            if (someCondition)
            {
                DoSomething(someArgument);
            }
            else
            {
                return someArray[10];
            }

            switch (status)
            {
                case Status.Foo:
                    return "Foo";

                case Status.Bar:
                    return "Bar";

                default:
                    return "Bar";
            }
            return String.Empty;
        }
    }
}
```