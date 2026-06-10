using System.Collections.ObjectModel;
using clothing_manufacture.Services;

namespace clothing_manufacture.ViewModels;

public class FormFieldViewModel : BaseViewModel
{
    private string _textValue = string.Empty;
    private object? _selectedValue;
    private DateTime? _dateValue;
    private bool _boolValue;

    public FieldDefinition Definition { get; }

    public FormFieldViewModel(FieldDefinition definition)
    {
        Definition = definition;
        Label = definition.IsRequired ? definition.Label + " *" : definition.Label;
        ControlKind = definition.Kind.ToString();
        HelpText = definition.HelpText;
        Options = new ObservableCollection<SelectOption>();
        ResetToDefault();
    }

    public string Label { get; }
    public string ControlKind { get; }
    public string? HelpText { get; }
    public ObservableCollection<SelectOption> Options { get; }

    public string TextValue
    {
        get => _textValue;
        set
        {
            _textValue = value;
            OnPropertyChanged();
        }
    }

    public object? SelectedValue
    {
        get => _selectedValue;
        set
        {
            _selectedValue = value;
            OnPropertyChanged();
        }
    }

    public DateTime? DateValue
    {
        get => _dateValue;
        set
        {
            _dateValue = value;
            OnPropertyChanged();
        }
    }

    public bool BoolValue
    {
        get => _boolValue;
        set
        {
            _boolValue = value;
            OnPropertyChanged();
        }
    }

    public void ResetToDefault()
    {
        TextValue = Definition.DefaultValue?.ToString() ?? string.Empty;
        SelectedValue = Definition.DefaultValue;
        DateValue = Definition.DefaultValue is DateTime defaultDate ? defaultDate : null;
        BoolValue = Definition.DefaultValue is bool boolValue && boolValue;
    }
}
