﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class EnvironmentVariables {
    
    private EnvironmentVariablesEnvironmentVariable[] environmentVariableField;
    
    private string clsidField;
    
    private bool disabledField;
    
    private bool disabledFieldSpecified;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("EnvironmentVariable")]
    public EnvironmentVariablesEnvironmentVariable[] EnvironmentVariable {
        get {
            return this.environmentVariableField;
        }
        set {
            this.environmentVariableField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string clsid {
        get {
            return this.clsidField;
        }
        set {
            this.clsidField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool disabled {
        get {
            return this.disabledField;
        }
        set {
            this.disabledField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool disabledSpecified {
        get {
            return this.disabledFieldSpecified;
        }
        set {
            this.disabledFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class EnvironmentVariablesEnvironmentVariable {
    
    private EnvironmentVariablesEnvironmentVariableProperties propertiesField;
    
    private string clsidField;
    
    private string nameField;
    
    private byte imageField;
    
    private string changedField;
    
    private string uidField;
    
    private string descField;
    
    private bool bypassErrorsField;
    
    private bool bypassErrorsFieldSpecified;
    
    private bool userContextField;
    
    private bool userContextFieldSpecified;
    
    private bool removePolicyField;
    
    private bool removePolicyFieldSpecified;
    
    /// <remarks/>
    public EnvironmentVariablesEnvironmentVariableProperties Properties {
        get {
            return this.propertiesField;
        }
        set {
            this.propertiesField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string clsid {
        get {
            return this.clsidField;
        }
        set {
            this.clsidField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte image {
        get {
            return this.imageField;
        }
        set {
            this.imageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string changed {
        get {
            return this.changedField;
        }
        set {
            this.changedField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string uid {
        get {
            return this.uidField;
        }
        set {
            this.uidField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string desc {
        get {
            return this.descField;
        }
        set {
            this.descField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool bypassErrors {
        get {
            return this.bypassErrorsField;
        }
        set {
            this.bypassErrorsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool bypassErrorsSpecified {
        get {
            return this.bypassErrorsFieldSpecified;
        }
        set {
            this.bypassErrorsFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool userContext {
        get {
            return this.userContextField;
        }
        set {
            this.userContextField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool userContextSpecified {
        get {
            return this.userContextFieldSpecified;
        }
        set {
            this.userContextFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool removePolicy {
        get {
            return this.removePolicyField;
        }
        set {
            this.removePolicyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool removePolicySpecified {
        get {
            return this.removePolicyFieldSpecified;
        }
        set {
            this.removePolicyFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class EnvironmentVariablesEnvironmentVariableProperties {
    
    private string actionField;
    
    private string nameField;
    
    private string valueField;
    
    private bool userField;
    
    private bool userFieldSpecified;
    
    private bool partialField;
    
    private bool partialFieldSpecified;
    
    private bool disabledField;
    
    private bool disabledFieldSpecified;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string action {
        get {
            return this.actionField;
        }
        set {
            this.actionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool user {
        get {
            return this.userField;
        }
        set {
            this.userField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool userSpecified {
        get {
            return this.userFieldSpecified;
        }
        set {
            this.userFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool partial {
        get {
            return this.partialField;
        }
        set {
            this.partialField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool partialSpecified {
        get {
            return this.partialFieldSpecified;
        }
        set {
            this.partialFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool disabled {
        get {
            return this.disabledField;
        }
        set {
            this.disabledField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool disabledSpecified {
        get {
            return this.disabledFieldSpecified;
        }
        set {
            this.disabledFieldSpecified = value;
        }
    }
}