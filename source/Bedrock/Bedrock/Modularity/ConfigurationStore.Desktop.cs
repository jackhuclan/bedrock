// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Configuration;
using Bedrock.Modularity;

namespace Bedrock.Modularity
{
    /// <summary>
    /// Defines a store for the module metadata.
    /// </summary>
    public class ConfigurationStore : IConfigurationStore
    {
        /// <summary>
        /// Gets the module configuration data.
        /// </summary>
        /// <returns>A <see cref="ModulesConfigurationSection"/> instance.</returns>
        public ModulesConfigurationSection RetrieveModuleConfigurationSection()
        {
            return ConfigurationManager.GetSection("modules") as ModulesConfigurationSection;
        }
    }
}