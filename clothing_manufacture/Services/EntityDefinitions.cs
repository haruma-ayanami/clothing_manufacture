using System.Collections.ObjectModel;

namespace clothing_manufacture.Services;

public enum FieldKind
{
    Text,
    Multiline,
    Integer,
    Decimal,
    Date,
    Checkbox,
    Choice,
    Lookup
}

public class SelectOption
{
    public object? Value { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class GridColumnDefinition
{
    public string Header { get; set; } = string.Empty;
    public string BindingPath { get; set; } = string.Empty;
    public double Width { get; set; } = 160;
    public double MinWidth { get; set; } = 100;
}

public class FieldDefinition
{
    public string PropertyName { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public FieldKind Kind { get; set; } = FieldKind.Text;
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
    public string? HelpText { get; set; }
    public object? DefaultValue { get; set; }
    public List<SelectOption> StaticOptions { get; set; } = new();
    public Type? LookupEntityType { get; set; }
    public string? LookupDisplayProperty { get; set; }
    public List<string> LookupIncludePaths { get; set; } = new();
}

public class EntityDescriptor
{
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Type EntityType { get; set; } = typeof(object);
    public string KeyPropertyName { get; set; } = "Id";
    public string DisplayProperty { get; set; } = "Id";
    public List<string> IncludePaths { get; set; } = new();
    public List<GridColumnDefinition> Columns { get; set; } = new();
    public List<FieldDefinition> Fields { get; set; } = new();
    public Func<IReadOnlyDictionary<string, object?>, string?>? CustomValidate { get; set; }
}
