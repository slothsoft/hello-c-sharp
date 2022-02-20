using HelloCSharp.Api.Models;
using NUnit.Framework;

namespace HelloCSharp.Persistence.Tests.TestData;

public interface ITestData<TIdentifiable, TSave>
    where TIdentifiable : Identifiable
{
    /// <summary>
    /// Create a random object. All fields (but the ID) should be unique for each call, if at all possible.
    /// </summary>
    TSave CreateRandomObject();

    /// <summary>
    /// Create an example object. This object has to be created in the database on default.
    /// </summary>
    TIdentifiable GetExampleObject();

    /// <summary>
    /// Compares two instances of the identifiable using <see cref="Assert"/>.
    /// </summary>
    void AssertAreEqual(TIdentifiable expected, TIdentifiable actual);
    
    /// <summary>
    /// Compares two instances of the similar types using <see cref="Assert"/>.
    /// </summary>
    void AssertAreEqual(TSave expected, TIdentifiable actual);
}