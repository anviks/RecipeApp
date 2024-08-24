﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipeApp.Resources.Errors {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ModelBindingErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ModelBindingErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RecipeApp.Resources.Errors.ModelBindingErrors", typeof(ModelBindingErrors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The attempted value &apos;{1}&apos; is not valid for &apos;{0}&apos;..
        /// </summary>
        public static string AttemptedValueIsInvalid {
            get {
                return ResourceManager.GetString("AttemptedValueIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A value for the &apos;{0}&apos; property was not provided..
        /// </summary>
        public static string MissingBindRequiredValue {
            get {
                return ResourceManager.GetString("MissingBindRequiredValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A value is required..
        /// </summary>
        public static string MissingKeyOrValue {
            get {
                return ResourceManager.GetString("MissingKeyOrValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A non-empty request body is required..
        /// </summary>
        public static string MissingRequestBodyRequiredValue {
            get {
                return ResourceManager.GetString("MissingRequestBodyRequiredValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The attempted value &apos;{0}&apos; is not valid..
        /// </summary>
        public static string NonPropertyAttemptedValueIsInvalid {
            get {
                return ResourceManager.GetString("NonPropertyAttemptedValueIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The supplied value is invalid..
        /// </summary>
        public static string NonPropertyUnknownValueIsInvalid {
            get {
                return ResourceManager.GetString("NonPropertyUnknownValueIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value must be a number..
        /// </summary>
        public static string NonPropertyValueMustBeANumber {
            get {
                return ResourceManager.GetString("NonPropertyValueMustBeANumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; is invalid..
        /// </summary>
        public static string UnknownValueIsInvalid {
            get {
                return ResourceManager.GetString("UnknownValueIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; is invalid..
        /// </summary>
        public static string ValueIsInvalid {
            get {
                return ResourceManager.GetString("ValueIsInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; must be a number..
        /// </summary>
        public static string ValueMustBeANumber {
            get {
                return ResourceManager.GetString("ValueMustBeANumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; must not be null..
        /// </summary>
        public static string ValueMustNotBeNull {
            get {
                return ResourceManager.GetString("ValueMustNotBeNull", resourceCulture);
            }
        }
    }
}