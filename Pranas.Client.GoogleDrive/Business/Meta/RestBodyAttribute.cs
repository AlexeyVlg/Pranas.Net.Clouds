using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Meta
{
    /// <summary>
    /// Specifies an attribute to describe a property as a REST body.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RestBodyAttribute : Attribute
    {
    }
}
