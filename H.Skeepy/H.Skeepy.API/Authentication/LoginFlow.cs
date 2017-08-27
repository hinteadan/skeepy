﻿using H.Skeepy.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H.Skeepy.API.Authentication
{
    public class LoginFlow
    {
        private readonly ICanStoreSkeepy<Token> tokenStore;
        private readonly ICanAuthenticate<Credentials> credentialsAuthenticator;

        public LoginFlow(ICanAuthenticate<Credentials> credentialsAuthenticator, ICanStoreSkeepy<Token> tokenStore)
        {
            this.credentialsAuthenticator = credentialsAuthenticator ?? throw new InvalidOperationException($"Must provide a {nameof(credentialsAuthenticator)}");
            this.tokenStore = tokenStore ?? throw new InvalidOperationException($"Must provide a {nameof(tokenStore)}");
        }

        public Task<AuthenticationResult> Login(Credentials credentials)
        {
            return credentialsAuthenticator
                .Authenticate(credentials)
                .ContinueWith(t =>
                {
                    if (!t.Result.IsSuccessful)
                    {
                        return t.Result;
                    }

                    tokenStore.Put(t.Result.Token).Wait();

                    return t.Result;
                });
        }
    }
}
