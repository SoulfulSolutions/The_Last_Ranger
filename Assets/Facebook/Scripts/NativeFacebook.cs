// Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
//
// You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
// copy, modify, and distribute this software in source code or binary form for use
// in connection with the web services and APIs provided by Facebook.
//
// As with any software that integrates with the Facebook platform, your use of
// this software is subject to the Facebook Developer Principles and Policies
// [http://developers.facebook.com/policy/]. This copyright notice shall be
// included in all copies or substantial portions of the software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections.Generic;

namespace Facebook
{
    /// <summary>
    /// Classes defined on the mobile sdks
    /// </summary>
    internal abstract class NativeFacebook : AbstractFacebook
    {
        private const string CallbackIdKey = "callback_id";
        private NativeDialogModes.ShareDialogMode shareDialogMode =
            NativeDialogModes.ShareDialogMode.AUTOMATIC;

        // Delegates 
        protected FacebookDelegate deepLinkDelegate;
        protected InitDelegate onInitComplete;

        /// <summary>
        /// Gets or sets the dialog mode.
        /// </summary>
        /// <value>The dialog mode use for sharing, login, and other dialogs.</value>
        public NativeDialogModes.ShareDialogMode ShareDialogMode
        {
            get {
                return shareDialogMode;
            }

            set {
                shareDialogMode = value;
                this.setShareDialogMode(shareDialogMode);
            }
        }

        protected void OnLoginComplete(string message)
        {
            var parameters = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            
            if (parameters.ContainsKey("user_id"))
            {
                this.isLoggedIn = true;
                this.userId = (string)parameters ["user_id"];
                this.accessToken = (string)parameters ["access_token"];
                this.accessTokenExpiresAt = FromTimestamp(int.Parse((string)parameters ["expiration_timestamp"]));
            }
            
            OnAuthResponse(new FBResult(message));
        }

        protected void OnGetDeepLinkComplete(string message)
        {
            var rawResult = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            if (this.deepLinkDelegate != null)
            {
                object deepLink = "";
                rawResult.TryGetValue("deep_link", out deepLink);
                deepLinkDelegate(new FBResult(deepLink.ToString()));
            }
        }

        protected void OnGroupCreateComplete(string message)
        {
            var result = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            var callbackId = (string)result [CallbackIdKey];
            result.Remove(CallbackIdKey);
            OnFacebookResponse(callbackId, new FBResult(MiniJSON.Json.Serialize(result)));
        }

        protected void OnGroupJoinComplete(string message)
        {
            var result = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            var callbackId = (string)result [CallbackIdKey];
            result.Remove(CallbackIdKey);
            OnFacebookResponse(callbackId, new FBResult(MiniJSON.Json.Serialize(result)));
        }

        protected void OnLogoutComplete(string message)
        {
            this.isLoggedIn = false;
            this.userId = "";
            this.accessToken = "";
        }

        protected void OnAppRequestsComplete(string message)
        {
            var rawResult = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            if (rawResult.ContainsKey(CallbackIdKey))
            {
                var result = new Dictionary<string, object>();
                var callbackId = (string)rawResult [CallbackIdKey];
                rawResult.Remove(CallbackIdKey);
                if (rawResult.Count > 0)
                {
                    List<string> to = new List<string>(rawResult.Count - 1);
                    foreach (string key in rawResult.Keys)
                    {
                        if (!key.StartsWith("to"))
                        {
                            result [key] = rawResult [key];
                            continue;
                        }
                        to.Add((string)rawResult [key]);
                    }
                    result.Add("to", to);
                    rawResult.Clear();
                    OnFacebookResponse(callbackId, new FBResult(MiniJSON.Json.Serialize(result)));
                } else
                {
                    //if we make it here java returned a callback message with only an id
                    //this isnt supposed to happen
                    OnFacebookResponse(callbackId, new FBResult(MiniJSON.Json.Serialize(result), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
                }
            }
        }

        protected void OnInitComplete(string message)
        {
            this.isInitialized = true;
            OnLoginComplete(message);
            if (this.onInitComplete != null)
            {
                this.onInitComplete();
            }
        }

        protected void OnAccessTokenRefresh(string message)
        {
            var parameters = (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
            if (parameters.ContainsKey("access_token"))
            {
                accessToken = (string)parameters ["access_token"];
                accessTokenExpiresAt = FromTimestamp(int.Parse((string)parameters ["expiration_timestamp"]));
            }
        }

        protected void OnShareLinkComplete(string message)
        {
            IDictionary<string, object> rawResult = NativeFacebook.DeserializeMessage(message);
            string errorMessage;
            string callbackId;
            if (NativeFacebook.TryGetError(rawResult, out errorMessage))
            {
                if (NativeFacebook.TryGetCallbackId(rawResult, out callbackId))
                {
                    OnFacebookResponse(callbackId, 
                                       new FBResult(
                        NativeFacebook.SerializeDictionary(rawResult), 
                        errorMessage));
                }
                return;
            }

            if (NativeFacebook.TryGetCallbackId(rawResult, out callbackId))
            {
                var result = new Dictionary<string, object>();
                rawResult.Remove(CallbackIdKey);
                if (rawResult.Count > 0)
                {
                    foreach (string key in rawResult.Keys)
                    {
                        result [key] = rawResult [key];
                    }
                    rawResult.Clear();
                    OnFacebookResponse(
                        callbackId, 
                        new FBResult(
                            MiniJSON.Json.Serialize(result)));
                } else
                {
                    //if we make it here java returned a callback message with only a callback id
                    //this isnt supposed to happen
                    OnFacebookResponse(
                        callbackId, 
                        new FBResult(
                            NativeFacebook.SerializeDictionary(result), 
                            "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
                }
            }
        }

        private static IDictionary<string, object> DeserializeMessage(string message)
        {
            return (Dictionary<string, object>)MiniJSON.Json.Deserialize(message);
        }

        private static string SerializeDictionary(IDictionary<string, object> dict)
        {
            return MiniJSON.Json.Serialize(dict);
        }

        private static bool TryGetCallbackId(IDictionary<string, object> result, out string callbackId)
        {
            object callback;
            callbackId = null;
            if (result.TryGetValue("callback_id", out callback))
            {
                callbackId = callback as string;
                return true;
            }
            
            return false;
        }

        private static bool TryGetError(IDictionary<string, object> result, out string errorMessage)
        {
            object error;
            errorMessage = null;
            if (result.TryGetValue("error", out error))
            {
                errorMessage = error as string;
                return true;
            }

            return false;
        }

        private static DateTime FromTimestamp(int timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
        }

        protected static Dictionary<string, string> ToStringDict(Dictionary<string, object> dict)
        {
            if (dict == null)
            {
                return null;
            }
            var newDict = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> kvp in dict)
            {
                newDict [kvp.Key] = kvp.Value.ToString();
            }
            return newDict;
        }

        protected abstract void setShareDialogMode(NativeDialogModes.ShareDialogMode mode);
    }
}
