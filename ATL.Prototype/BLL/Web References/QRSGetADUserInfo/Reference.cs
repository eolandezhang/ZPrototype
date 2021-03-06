﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18213
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18213 版自动生成。
// 
#pragma warning disable 1591

namespace BLL.QRSGetADUserInfo {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetUserMailOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserInfoByConditonOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAssetInfoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::BLL.Properties.Settings.Default.BLL_QRSGetADUserInfo_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetUserMailCompletedEventHandler GetUserMailCompleted;
        
        /// <remarks/>
        public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;
        
        /// <remarks/>
        public event GetUserInfoByConditonCompletedEventHandler GetUserInfoByConditonCompleted;
        
        /// <remarks/>
        public event GetAssetInfoCompletedEventHandler GetAssetInfoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserMail", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetUserMail(string username) {
            object[] results = this.Invoke("GetUserMail", new object[] {
                        username});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserMailAsync(string username) {
            this.GetUserMailAsync(username, null);
        }
        
        /// <remarks/>
        public void GetUserMailAsync(string username, object userState) {
            if ((this.GetUserMailOperationCompleted == null)) {
                this.GetUserMailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserMailOperationCompleted);
            }
            this.InvokeAsync("GetUserMail", new object[] {
                        username}, this.GetUserMailOperationCompleted, userState);
        }
        
        private void OnGetUserMailOperationCompleted(object arg) {
            if ((this.GetUserMailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserMailCompleted(this, new GetUserMailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetUserInfo(string staffid) {
            object[] results = this.Invoke("GetUserInfo", new object[] {
                        staffid});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserInfoAsync(string staffid) {
            this.GetUserInfoAsync(staffid, null);
        }
        
        /// <remarks/>
        public void GetUserInfoAsync(string staffid, object userState) {
            if ((this.GetUserInfoOperationCompleted == null)) {
                this.GetUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
            }
            this.InvokeAsync("GetUserInfo", new object[] {
                        staffid}, this.GetUserInfoOperationCompleted, userState);
        }
        
        private void OnGetUserInfoOperationCompleted(object arg) {
            if ((this.GetUserInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUserInfoByConditon", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserInfo GetUserInfoByConditon(string message, string type) {
            object[] results = this.Invoke("GetUserInfoByConditon", new object[] {
                        message,
                        type});
            return ((UserInfo)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserInfoByConditonAsync(string message, string type) {
            this.GetUserInfoByConditonAsync(message, type, null);
        }
        
        /// <remarks/>
        public void GetUserInfoByConditonAsync(string message, string type, object userState) {
            if ((this.GetUserInfoByConditonOperationCompleted == null)) {
                this.GetUserInfoByConditonOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoByConditonOperationCompleted);
            }
            this.InvokeAsync("GetUserInfoByConditon", new object[] {
                        message,
                        type}, this.GetUserInfoByConditonOperationCompleted, userState);
        }
        
        private void OnGetUserInfoByConditonOperationCompleted(object arg) {
            if ((this.GetUserInfoByConditonCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserInfoByConditonCompleted(this, new GetUserInfoByConditonCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAssetInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAssetInfo(string asset_no) {
            object[] results = this.Invoke("GetAssetInfo", new object[] {
                        asset_no});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAssetInfoAsync(string asset_no) {
            this.GetAssetInfoAsync(asset_no, null);
        }
        
        /// <remarks/>
        public void GetAssetInfoAsync(string asset_no, object userState) {
            if ((this.GetAssetInfoOperationCompleted == null)) {
                this.GetAssetInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAssetInfoOperationCompleted);
            }
            this.InvokeAsync("GetAssetInfo", new object[] {
                        asset_no}, this.GetAssetInfoOperationCompleted, userState);
        }
        
        private void OnGetAssetInfoOperationCompleted(object arg) {
            if ((this.GetAssetInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAssetInfoCompleted(this, new GetAssetInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18213")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class UserInfo {
        
        private string cnNameField;
        
        private string userNameField;
        
        private string mailField;
        
        private string staffNoField;
        
        private string managerNameField;
        
        private string managerMailField;
        
        private string managerNoField;
        
        private string deptField;
        
        private string titleField;
        
        private string telField;
        
        /// <remarks/>
        public string CnName {
            get {
                return this.cnNameField;
            }
            set {
                this.cnNameField = value;
            }
        }
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string Mail {
            get {
                return this.mailField;
            }
            set {
                this.mailField = value;
            }
        }
        
        /// <remarks/>
        public string StaffNo {
            get {
                return this.staffNoField;
            }
            set {
                this.staffNoField = value;
            }
        }
        
        /// <remarks/>
        public string ManagerName {
            get {
                return this.managerNameField;
            }
            set {
                this.managerNameField = value;
            }
        }
        
        /// <remarks/>
        public string ManagerMail {
            get {
                return this.managerMailField;
            }
            set {
                this.managerMailField = value;
            }
        }
        
        /// <remarks/>
        public string ManagerNo {
            get {
                return this.managerNoField;
            }
            set {
                this.managerNoField = value;
            }
        }
        
        /// <remarks/>
        public string Dept {
            get {
                return this.deptField;
            }
            set {
                this.deptField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Tel {
            get {
                return this.telField;
            }
            set {
                this.telField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    public delegate void GetUserMailCompletedEventHandler(object sender, GetUserMailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserMailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserMailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    public delegate void GetUserInfoCompletedEventHandler(object sender, GetUserInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    public delegate void GetUserInfoByConditonCompletedEventHandler(object sender, GetUserInfoByConditonCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserInfoByConditonCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserInfoByConditonCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public UserInfo Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((UserInfo)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    public delegate void GetAssetInfoCompletedEventHandler(object sender, GetAssetInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18213")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAssetInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAssetInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591