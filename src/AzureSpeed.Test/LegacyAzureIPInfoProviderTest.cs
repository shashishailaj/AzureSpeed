﻿using System.IO;
using System.Reflection;
using AzureSpeed.ApiService.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureSpeed.Test
{
    [TestClass]
    public class LegacyAzureIPInfoProviderTest
    {
        private readonly LegacyAzureIPInfoProvider legacyAzureIpInfoProvider;

        public LegacyAzureIPInfoProviderTest()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.legacyAzureIpInfoProvider = new LegacyAzureIPInfoProvider(path);
        }

        [TestMethod]
        public void CanGetRegionNameByIp()
        {
            // ip of azure.com
            string ipOfAzureCom = "168.62.225.23";
            var eastAsiaRegion = this.legacyAzureIpInfoProvider.GetRegionInfo(ipOfAzureCom);
            Assert.AreEqual("Azure", eastAsiaRegion.Cloud);
            Assert.AreEqual("North Central US", eastAsiaRegion.Region);
            Assert.AreEqual(ipOfAzureCom, eastAsiaRegion.IPAddress);

            // ip of azure.cn
            string ipOfAzureCN = "42.159.5.43";
            var eastChinaRegion = this.legacyAzureIpInfoProvider.GetRegionInfo(ipOfAzureCN);
            Assert.AreEqual("Azure", eastAsiaRegion.Cloud);
            Assert.AreEqual("China North", eastChinaRegion.Region);
            Assert.AreEqual(ipOfAzureCN, eastChinaRegion.IPAddress);

            string ipOfAzureSpeedCom = "104.45.231.79";
            var azureSpeedRegion = this.legacyAzureIpInfoProvider.GetRegionInfo("www.azurespeed.com");
            Assert.AreEqual("Azure", eastAsiaRegion.Cloud);
            Assert.AreEqual("West US", azureSpeedRegion.Region);
            Assert.AreEqual(ipOfAzureSpeedCom, azureSpeedRegion.IPAddress);

            var azureSpeedRegion1 = this.legacyAzureIpInfoProvider.GetRegionInfo("https://www.azurespeed.com/");
            Assert.AreEqual("Azure", eastAsiaRegion.Cloud);
            Assert.AreEqual("West US", azureSpeedRegion1.Region);
            Assert.AreEqual(ipOfAzureSpeedCom, azureSpeedRegion1.IPAddress);

            var noRegion = this.legacyAzureIpInfoProvider.GetRegionInfo("1.1.1.1");
            Assert.IsTrue(string.IsNullOrEmpty(noRegion.Region));

            // TODO
            // var invalidHostRegion = this.legacyAzureIpInfoProvider.GetRegionInfo("88888888888888888888.888");
            // Assert.IsTrue(string.IsNullOrEmpty(invalidHostRegion.Region));
        }
    }
}