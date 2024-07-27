using System.Reflection;
using System.Xml.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BDR.BestDeal.XMLLogisticsCompany.Schema;

public class XmlSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var typeInfo = context.Type;

        // Set root element name from XmlRootAttribute if present
        var xmlRootAttribute = typeInfo.GetCustomAttribute<XmlRootAttribute>();
        if (xmlRootAttribute != null)
            schema.Xml = new OpenApiXml
            {
                Name = xmlRootAttribute.ElementName
            };

        // Apply XML element names to properties and handle arrays
        foreach (var propertyInfo in typeInfo.GetProperties())
        {
            ApplyXmlElementAttribute(schema, propertyInfo);
            ApplyXmlArrayAttributes(schema, propertyInfo);
        }
    }

    private void ApplyXmlElementAttribute(OpenApiSchema schema, PropertyInfo propertyInfo)
    {
        var xmlElementAttribute = propertyInfo.GetCustomAttribute<XmlElementAttribute>();
        if (xmlElementAttribute != null && schema.Properties.ContainsKey(propertyInfo.Name))
            schema.Properties[propertyInfo.Name].Xml = new OpenApiXml
            {
                Name = xmlElementAttribute.ElementName
            };
    }

    private void ApplyXmlArrayAttributes(OpenApiSchema schema, PropertyInfo propertyInfo)
    {
        var xmlArrayAttribute = propertyInfo.GetCustomAttribute<XmlArrayAttribute>();
        var xmlArrayItemAttribute = propertyInfo.GetCustomAttribute<XmlArrayItemAttribute>();
        if (xmlArrayAttribute != null && xmlArrayItemAttribute != null &&
            schema.Properties.ContainsKey(propertyInfo.Name))
        {
            schema.Properties[propertyInfo.Name].Type = "array";
            schema.Properties[propertyInfo.Name].Xml = new OpenApiXml
            {
                Name = xmlArrayAttribute.ElementName,
                Wrapped = true
            };
            schema.Properties[propertyInfo.Name].Items = new OpenApiSchema
            {
                Xml = new OpenApiXml
                {
                    Name = xmlArrayItemAttribute.ElementName
                },
                Type = "integer" // Explicitly setting type to integer for items if it's a list of integers
            };
        }
    }
}