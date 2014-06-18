﻿// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using System.Threading;
    using PipeConfigurators;
    using Policies;


    public static class RetryPipeConfiguratorExtensions
    {
        public static void Retry<T>(this IPipeConfigurator<T> configurator, IRetryPolicy retryPolicy)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator");

            var pipeBuilderConfigurator = new RetryPipeBuilderConfigurator<T>(retryPolicy);

            configurator.AddPipeBuilderConfigurator(pipeBuilderConfigurator);
        }

        public static void Retry<T>(this IPipeConfigurator<T> configurator, IRetryPolicy retryPolicy, CancellationToken cancellationToken)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException("configurator");
            if (retryPolicy == null)
                throw new ArgumentNullException("retryPolicy");

            retryPolicy = new CancelRetryPolicy(retryPolicy, cancellationToken);

            var pipeBuilderConfigurator = new RetryPipeBuilderConfigurator<T>(retryPolicy);

            configurator.AddPipeBuilderConfigurator(pipeBuilderConfigurator);
        }
    }
}